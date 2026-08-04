using Anabasis.Common.DamageClasses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Projectiles
{
    public abstract class FlaskBottleProjectile : ModProjectile
    {
        public virtual int DebuffType => 0;
        public virtual int DebuffDuration => 600;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Shuriken);
            AIType = ProjectileID.Shuriken;
            Projectile.DamageType = ModContent.GetInstance<AlchemistDamageClass>();
            Projectile.width = 26;
            Projectile.height = 32;
            Projectile.penetrate = 1;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (DebuffType > 0)
                target.AddBuff(DebuffType, DebuffDuration);
        }
    }
}
