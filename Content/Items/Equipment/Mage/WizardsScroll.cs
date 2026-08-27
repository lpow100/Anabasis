using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Equipment.Mage
{
    public class WizardsScroll : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 32;

            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 45);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<MagicDamageClass>().Flat += 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 15)
                .AddRecipeGroup("Anabasis:AnyGem", 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
