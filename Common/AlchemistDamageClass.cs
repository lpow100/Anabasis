using Terraria.ModLoader;
using Terraria.Localization;

namespace Anabasis.Common.DamageClasses
{
    public class AlchemistDamageClass : DamageClass
    {
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Generic)
                return StatInheritanceData.Full;

            return StatInheritanceData.None;
        }

        public override bool GetEffectInheritance(DamageClass damageClass) => damageClass == Generic;

        public override LocalizedText DisplayName => Language.GetOrRegister(
            this.GetLocalizationKey("DisplayName"),
            () => "Alchemy Damage"
        );
    }
}