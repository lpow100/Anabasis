using Anabasis.Common.DamageClasses;
using Anabasis.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Equipment.Summoner
{
    public class ShinedStone : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 45);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<SummonDamageClass>().Flat += 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("Anabasis:AnyGem", 3)
                .AddIngredient(ItemID.StoneBlock, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
