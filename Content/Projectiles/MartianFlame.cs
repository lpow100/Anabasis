using Anabasis.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.GameContent.Animations.IL_Actions.Sprites;

namespace Anabasis.Content.Projectiles
{
    public class MartianFlame: ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = ProjAIStyleID.Bounce;
            Projectile.hostile = true;
            Projectile.light = 0.8f;
            Projectile.alpha = 100;
            Projectile.penetrate = -1;
            Projectile.scale = 1.5f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Main.rand.Next(16) > 10)
            {
                target.AddBuff(ModContent.BuffType<MartianFire>(), 150 + Main.rand.Next(91));
            }
        }
    }
}
