using Anabasis.Common.DamageClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Projectiles
{
    // This example is similar to the Wooden Arrow projectile
    public class CoralAmalgamationProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Shuriken);
            AIType = ProjectileID.Shuriken;
            Projectile.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.penetrate = 2;
        }
    }
}
