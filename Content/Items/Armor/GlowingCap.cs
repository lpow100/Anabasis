using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Anabasis.Common.DamageClasses;

namespace Anabasis.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GlowingCap : ModItem
    {

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;

            SetBonusText = this.GetLocalization("SetBonus");
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
            return body.type == ModContent.ItemType<MushroomWear>() && legs.type == ModContent.ItemType<MushroomBottoms>();

        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;
            player.GetDamage<AlchemistDamageClass>().Flat += 3f;
            player.GetCritChance<AlchemistDamageClass>() += 0.02f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<AlchemistDamageClass>().Flat += 1f;
            player.GetCritChance<AlchemistDamageClass>() += 0.04f;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {

            CreateRecipe()
                .AddIngredient(ItemID.GlowingMushroom, 20)
                .AddIngredient(ItemID.TissueSample, 4)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.GlowingMushroom, 20)
                .AddIngredient(ItemID.ShadowScale, 4)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
