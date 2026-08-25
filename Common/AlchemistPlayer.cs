using Terraria.ModLoader;

namespace Anabasis.Common.Players
{
    public class AlchemistPlayer : ModPlayer
    {
        public float consumableSaveChance = 0f;
        public float alchemistDebuffBonusDamage = 0;

        public override void ResetEffects()
        {
            alchemistDebuffBonusDamage = 0;
            consumableSaveChance = 0f; // reset every frame; armor re-applies it below
        }
    }
}
