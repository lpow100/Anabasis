using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Anabasis.Common.GlobalItems
{
    public class CrimsonLeather : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ItemID.Leather)
                .AddIngredient(ItemID.Vertebrae, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
