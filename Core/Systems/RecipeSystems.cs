using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Core.Systems
{
    public class RecipeSystems : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup gemGroup = new RecipeGroup(
                () => $"{Lang.misc[37]} Gem", 
                ItemID.Diamond, 
                ItemID.Amber, 
                ItemID.Ruby, 
                ItemID.Emerald, 
                ItemID.Sapphire, 
                ItemID.Topaz, 
                ItemID.Amethyst
            );
            RecipeGroup.RegisterGroup("Anabasis:AnyGem", gemGroup);
        }
    }
}
