using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Anabasis.Content.Projectiles
{
    public class BlazingBottleProjectile : FlaskBottleProjectile
    {
        public override int DebuffType => BuffID.OnFire;

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.Center, 0.9f, 0.65f, 0.3f);
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            int Heat = Main.rand.Next(3, 8);

            for (int i = 0; i < Heat; i++)
            {
                Vector2 projvelocity = new(
                    (Main.rand.NextFloat() - 0.5f) * 8f,
                    (Main.rand.NextFloat() - 0.5f) * 8f
                );

                Dust dust = Dust.NewDustDirect(
                   Projectile.position,
                   Projectile.height,
                   Projectile.width,
                   DustID.Torch,
                   0,
                   0,
                   254
               );
                dust.velocity += projvelocity;
                dust.velocity *= 0.5f;
            }
        }
    }
}
