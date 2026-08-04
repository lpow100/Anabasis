using Anabasis.Common.DamageClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Projectiles
{
    // This example is similar to the Wooden Arrow projectile
    public class HoneyVialProjectile : ModProjectile
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
            int beePower = Main.rand.Next(2, 5);

            for (int i = 0; i < beePower; i++)
            {
                Vector2 projvelocity = new(
                    (Main.rand.NextFloat() - 0.5f) * 8f,
                    (Main.rand.NextFloat() - 0.5f) * 8f
                );

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.position,
                    projvelocity,
                    ProjectileID.Bee,
                    (int)Projectile.damage / 4,
                    Projectile.knockBack / 2.25f,
                    Projectile.owner
                );
            }
        }
    }
}
