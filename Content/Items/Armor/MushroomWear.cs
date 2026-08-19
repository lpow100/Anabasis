using Anabasis.Common.Players;
using Anabasis.Common.DamageClasses;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting a X_Body.png file to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Body)]
    public class MushroomWear : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 30; // Width of the item
            Item.height = 20; // Height of the item
            Item.value = Item.sellPrice(silver: 75); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 3; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<AlchemistDamageClass>().Flat += 5f;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GlowingMushroom, 30)
                .AddIngredient(ItemID.TissueSample, 6)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.GlowingMushroom, 30)
                .AddIngredient(ItemID.ShadowScale, 6)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
