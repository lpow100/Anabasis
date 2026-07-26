using Terraria.ModLoader;

namespace Anabasis.Common.Players
{
    public class AlchemistPlayer : ModPlayer
    {
        public float consumableSaveChance = 0f;

        public override void ResetEffects() {
            consumableSaveChance = 0f; // reset every frame; armor re-applies it below
        }
    }
}