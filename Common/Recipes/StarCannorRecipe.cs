using Anabasis.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Common.Recipes
{
    public class StarCannonRecipe : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ItemID.StarCannon))
                {
                    recipe.AddIngredient<Fulgurite>(5);
                }
            }
        }
    }
}