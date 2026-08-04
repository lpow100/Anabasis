using System;
using Anabasis.Common.DamageClasses;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Anabasis.Content.Projectiles
{
    class SporeMixtureProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Shuriken);
            AIType = ProjectileID.Shuriken;
            Projectile.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Projectile.width = 28;
            Projectile.height = 28;

            Projectile.penetrate = 1;
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            if (Projectile.owner != Main.myPlayer)
                return;

            int projcount = Main.rand.Next(5, 13);

            for (int i = 0; i < projcount; i++)
            {
                int projchoice = Main.rand.Next(1, 4);
                Microsoft.Xna.Framework.Vector2 projvelocity = new(
                    Main.rand.NextFloat() * 3 - 1.5f,
                    Main.rand.NextFloat() * 3 - 1.5f
                );

                if (projchoice == 1)
                {
                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        Projectile.position,
                        projvelocity,
                        ModContent.ProjectileType<SporeCloud1>(),
                        5, 0.5f, Projectile.owner);
                }
                else if (projchoice == 2)
                {
                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        Projectile.position,
                        projvelocity,
                        ModContent.ProjectileType<SporeCloud2>(),
                        5, 0.5f, Projectile.owner);
                }
                else if (projchoice == 3)
                {
                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        Projectile.position,
                        projvelocity,
                        ModContent.ProjectileType<SporeCloud3>(),
                        5, 0.5f, Projectile.owner);
                }
            }
        }
    }

    class SporeCloud1 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ToxicCloud);
        }
    }

    class SporeCloud2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ToxicCloud2);
        }
    }
    class SporeCloud3 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ToxicCloud3);
        }
    }
}
