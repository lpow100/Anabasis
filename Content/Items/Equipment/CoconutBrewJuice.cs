

using Anabasis.Common.DamageClasses;
using Anabasis.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Equipment
{
    public class CoconutBrewJuice : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 23;

            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 50);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<AlchemistDamageClass>().Flat += 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Coconut)
                .AddIngredient(ItemID.Bottle)
                .AddTile(TileID.WorkBenches)
                .Register();

            // No luck needed but expensive
            CreateRecipe()
                .AddIngredient(ItemID.PalmWood, 150)
                .AddIngredient(ItemID.BottledWater, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
