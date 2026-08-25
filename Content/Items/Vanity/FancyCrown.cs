using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Vanity
{
    // This tells tModLoader to look for a texture called MinionBossMask_Head, which is the texture on the player
    // and then registers this item to be accepted in head equip slots
    [AutoloadEquip(EquipType.Head)]
    public class FancyCrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
            ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;

            // Common values for every boss mask
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
            Item.maxStack = 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 3)
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.Ruby, 2)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 3)
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.Ruby, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}