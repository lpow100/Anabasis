using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Equipment.Melee
{
    public class AntlionKnuckles : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 33;
            Item.height = 24;

            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 45);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<MeleeDamageClass>().Flat += 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AntlionMandible, 3)
                .AddRecipeGroup(RecipeGroupID.IronBar, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
