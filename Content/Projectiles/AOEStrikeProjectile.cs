using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Anabasis.Content.Projectiles
{
    public class AOEStrikeProjectile : ModProjectile, ILocalizedModType
    {

        public override void SetDefaults()
        {
            Projectile.width = 24 * 5;
            Projectile.height = 24 * 5;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 0;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 80;
            Projectile.timeLeft = 12;
        }

        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Main.rand.Next(-10, 10), Main.rand.Next(-10, 10));
            }
            SoundEngine.PlaySound(SoundID.Item10);
        }
    }
}
