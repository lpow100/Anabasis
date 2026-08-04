#!/usr/bin/env python3
"""
weapon_scanner.py

Scans a folder of tModLoader ModItem .cs files (like Anabasis's weapon files)
and generates a markdown table of their stats.

Usage:
    python weapon_scanner.py <path_to_weapons_folder> [-o output.md]

Example:
    python weapon_scanner.py ./Content/Items/Weapons -o WeaponStats.md

Notes:
- Parses fields set on `Item.` inside SetDefaults() (damage, knockBack, useTime,
  rare, value, DamageType, shoot, consumable, autoReuse, maxStack, etc.)
- Parses ingredients/tiles from AddRecipes() -> CreateRecipe(...).AddIngredient(...)
- Falls back to "-" for any field it can't find, so a weapon missing a field
  (e.g. a melee weapon with no `shoot`) still shows up in the table cleanly.
- This is regex-based, not a real C# parser, so it expects reasonably
  conventional formatting (one field assignment per line). If your codebase
  uses helper methods instead of inline Item.x = y, those won't be picked up
  automatically -- see the "CUSTOM_FIELD_PATTERNS" section below to extend it.
"""

import re
import sys
import argparse
from pathlib import Path

# ---- Field patterns -------------------------------------------------------
# Each entry: display_name -> regex capturing the value assigned to Item.<field>
FIELD_PATTERNS = {
    "Damage":       r"Item\.damage\s*=\s*([^;]+);",
    "Knockback":    r"Item\.knockBack\s*=\s*([^;]+);",
    "Use Time":     r"Item\.useTime\s*=\s*([^;]+);",
    "Use Animation":r"Item\.useAnimation\s*=\s*([^;]+);",
    "Use Style":    r"Item\.useStyle\s*=\s*([^;]+);",
    "Rarity":       r"Item\.rare\s*=\s*([^;]+);",
    "Value":        r"Item\.value\s*=\s*([^;]+);",
    "Max Stack":    r"Item\.maxStack\s*=\s*([^;]+);",
    "Damage Class": r"Item\.DamageType\s*=\s*(?:ModContent\.GetInstance<)?([^;>()]+)",
    "Shoot Speed":  r"Item\.shootSpeed\s*=\s*([^;]+);",
    "Projectile":   r"Item\.shoot\s*=\s*(?:ModContent\.ProjectileType<)?([^;>()]+)",
    "Consumable":   r"Item\.consumable\s*=\s*([^;]+);",
    "Auto Reuse":   r"Item\.autoReuse\s*=\s*([^;]+);",
    "Crit Bonus":   r"Item\.crit\s*=\s*([^;]+);",
    "Width":        r"Item\.width\s*=\s*([^;]+);",
    "Height":       r"Item\.height\s*=\s*([^;]+);",
    "Mana":         r"Item\.mana\s*=\s*([^;]+);",
}

# Columns to actually show in the table, in order. Add/remove freely.
DISPLAY_COLUMNS = [
    "Damage", "Damage Class", "Knockback", "Use Time",
    "Rarity", "Value", "Consumable", "Auto Reuse",
    "Projectile", "Shoot Speed"
]

CLASS_NAME_PATTERN = r"public\s+class\s+(\w+)\s*:\s*ModItem"
SET_DEFAULTS_PATTERN = r"public\s+override\s+void\s+SetDefaults\s*\(\)\s*\{(.*?)\n\s*\}"
ADD_RECIPES_PATTERN = r"public\s+override\s+void\s+AddRecipes\s*\(\)\s*\{(.*?)\n\s*\}"

CREATE_RECIPE_AMOUNT = r"CreateRecipe\s*\(\s*(\d+)?\s*\)"
ADD_INGREDIENT_TYPED = r"AddIngredient<(\w+)>\s*\(\s*(\d+)?\s*\)"          # .AddIngredient<Fulgurite>()
ADD_INGREDIENT_ITEMID = r"AddIngredient\s*\(\s*ItemID\.(\w+)\s*(?:,\s*(\d+))?\s*\)"  # .AddIngredient(ItemID.X, 5)
ADD_TILE = r"AddTile\s*\(\s*TileID\.(\w+)\s*\)"


def clean_value(raw: str) -> str:
    """Tidy up a captured C# expression for display."""
    raw = raw.strip()
    # Item.sellPrice(silver: 120) -> "120 silver"
    m = re.match(r"Item\.sellPrice\((.*)\)", raw)
    if m:
        parts = [p.strip() for p in m.group(1).split(",") if p.strip()]
        return ", ".join(parts)
    return raw


def parse_recipe(block: str) -> str:
    if not block:
        return "-"
    amount_match = re.search(CREATE_RECIPE_AMOUNT, block)
    amount = amount_match.group(1) if amount_match and amount_match.group(1) else "1"

    ingredients = []
    for m in re.finditer(ADD_INGREDIENT_TYPED, block):
        name, qty = m.group(1), m.group(2) or "1"
        ingredients.append(f"{qty}x {name}")
    for m in re.finditer(ADD_INGREDIENT_ITEMID, block):
        name, qty = m.group(1), m.group(2) or "1"
        ingredients.append(f"{qty}x {name}")

    tiles = [m.group(1) for m in re.finditer(ADD_TILE, block)]

    parts = []
    if ingredients:
        parts.append(", ".join(ingredients))
    if tiles:
        parts.append(f"(at {', '.join(tiles)})")

    result = " ".join(parts) if parts else "-"
    return f"Makes {amount}: {result}"


def parse_file(path: Path):
    """Return a list of dicts, one per ModItem class found in the file."""
    text = path.read_text(encoding="utf-8", errors="ignore")
    results = []

    for class_match in re.finditer(CLASS_NAME_PATTERN, text):
        class_name = class_match.group(1)
        # Grab the body of the file after this class starts, up to a reasonable
        # window -- since files here are one-class-per-file in the example,
        # we just search the whole file for SetDefaults/AddRecipes.
        set_defaults_match = re.search(SET_DEFAULTS_PATTERN, text, re.DOTALL)
        add_recipes_match = re.search(ADD_RECIPES_PATTERN, text, re.DOTALL)

        block = set_defaults_match.group(1) if set_defaults_match else ""
        recipe_block = add_recipes_match.group(1) if add_recipes_match else ""

        row = {"Name": class_name, "File": path.name}
        for field, pattern in FIELD_PATTERNS.items():
            m = re.search(pattern, block)
            row[field] = clean_value(m.group(1)) if m else "-"

        row["Recipe"] = parse_recipe(recipe_block)
        results.append(row)

    return results


def scan_folder(folder: Path):
    all_rows = []
    for cs_file in sorted(folder.rglob("*.cs")):
        rows = parse_file(cs_file)
        all_rows.extend(rows)
    return all_rows


def to_markdown(rows, include_recipe=True) -> str:
    if not rows:
        return "_No ModItem classes found._"

    columns = ["Name"] + DISPLAY_COLUMNS + (["Recipe"] if include_recipe else [])

    lines = []
    lines.append("| " + " | ".join(columns) + " |")
    lines.append("|" + "|".join(["---"] * len(columns)) + "|")
    for row in rows:
        cells = [row.get(col, "-") for col in columns]
        # escape pipes so table doesn't break
        cells = [str(c).replace("|", "\\|") for c in cells]
        lines.append("| " + " | ".join(cells) + " |")

    return "\n".join(lines)


def main():
    parser = argparse.ArgumentParser(description="Scan tModLoader weapon .cs files into a markdown table.")
    parser.add_argument("folder", help="Path to the folder containing weapon .cs files (scanned recursively)")
    parser.add_argument("-o", "--output", default="WeaponStats.md", help="Output markdown file path")
    parser.add_argument("--no-recipe", action="store_true", help="Exclude the Recipe column")
    parser.add_argument("-m","--mana", action="store_false", help="Include Mana")
    args = parser.parse_args()

    folder = Path(args.folder)
    if not folder.exists():
        print(f"Folder not found: {folder}", file=sys.stderr)
        sys.exit(1)

    rows = scan_folder(folder)
    if args.mana:
        DISPLAY_COLUMNS.append("Mana")
    md = to_markdown(rows, include_recipe=not args.no_recipe)

    out_path = Path(args.output)
    out_path.write_text(md, encoding="utf-8")
    print(f"Scanned {len(rows)} weapon(s) across {folder}.")
    print(f"Wrote markdown table to {out_path}")


if __name__ == "__main__":
    main()
