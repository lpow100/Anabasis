using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Anabasis.Common.DamageClasses;

namespace Anabasis.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class WellWornHeadgear : ModItem
    {
        public static readonly int AdditiveGenericDamageBonus = 10;

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat

            SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(AdditiveGenericDamageBonus);
        }

        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 16; // Height of the item
            Item.value = Item.sellPrice(silver: 15); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 5; // The amount of defense the item will give when equipped
        }

        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<RaggedStorageCloth>() && legs.type == ModContent.ItemType<RaggedWornLeggings>();

        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value; // This is the setbonus tooltip: "Increases dealt damage by 20%"
            player.GetDamage<AlchemistDamageClass>() += AdditiveGenericDamageBonus / 100f; // Increase dealt damage for all weapon classes by 20%
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<AlchemistDamageClass>() += 0.10f;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 18)
                .AddIngredient(ItemID.Leather, 2)
                .AddIngredient(ItemID.IronBar, 2)
                .AddTile(TileID.WorkBenches)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.Silk, 18)
                .AddIngredient(ItemID.Leather, 1)
                .AddIngredient(ItemID.LeadBar, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
