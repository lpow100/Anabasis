using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Equipment.Ranger
{
    public class FlinxsFoot : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 20;

            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 45);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<RangedDamageClass>().Flat += 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FlinxFur, 3)
                .AddIngredient(ItemID.IceBlock, 6)
                .AddIngredient(ItemID.Chain, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
