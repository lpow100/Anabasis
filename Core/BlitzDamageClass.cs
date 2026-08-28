using Terraria.ModLoader;
using Terraria.Localization;

namespace Anabasis.Core
{
    public class BlitzDamageClass : DamageClass
    {
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Generic)
                return StatInheritanceData.Full;

            return StatInheritanceData.None;
        }

        public override bool GetEffectInheritance(DamageClass damageClass) => damageClass == Generic;

        public override LocalizedText DisplayName => Language.GetOrRegister("Mods.YourModName.DamageClasses.Throwing.DisplayName", () => "Blitz Damage");
    }
}