using Terraria;
using Terraria.ModLoader;
using Anabasis.Common.DamageClasses;
using Anabasis.Common.Players;
using Terraria.ID;

namespace Anabasis.Common.GlobalItems
{
    public class MolotovCocktailAlchemistGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ItemID.MolotovCocktail || entity.type == ItemID.RottenEgg ||
                entity.type == ItemID.Flamethrower    || entity.type == ItemID.ElfMelter) {
                entity.DamageType = ModContent.GetInstance<AlchemistDamageClass>(); // Change to desired damage type
            }
            if (entity.type == ItemID.ToxicFlask)
            {
                entity.DamageType = ModContent.GetInstance<AlchemistDamageClass>();
                entity.damage = 49;
                entity.useTime = 46;
                entity.useAnimation = 46;
                entity.mana = 0;
            }
        }
    }
}