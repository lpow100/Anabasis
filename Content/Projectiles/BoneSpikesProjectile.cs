using Anabasis.Common.DamageClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Projectiles
{
    // This example is similar to the Wooden Arrow projectile
    public class BoneSpikesProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Shuriken);
            AIType = ProjectileID.Shuriken;
            Projectile.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = 1;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 projvelocity = new(
                    (Main.rand.NextFloat() - 1f) * 6f + 4f,
                    (Main.rand.NextFloat() - 0.5f) * 5f
                );

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.position,
                    projvelocity,
                    ModContent.ProjectileType<BoneShard>(),
                    (int)Projectile.damage / 5,
                    Projectile.knockBack / 2.25f,
                    Projectile.owner
                );
            }
        }
    }

    public class BoneShard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.light = 0f;
            Projectile.timeLeft = 45;
        }
    }
}
