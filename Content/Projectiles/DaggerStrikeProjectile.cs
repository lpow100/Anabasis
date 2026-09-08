using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace Anabasis.Content.Projectiles
{
    public class DaggerStrikeProjectile : ModProjectile
    {
        public float CollisionWidth => 10f * Projectile.scale;

        public int Parent => (int)Projectile.ai[1];

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(38f);

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;

            // The parent already uses extraUpdates. The child does not need
            // another independent movement step.
            Projectile.extraUpdates = 0;
            Projectile.timeLeft = 3600;
        }

        public override void AI()
        {
            if (!IsValidParent())
            {
                Projectile.Kill();
                return;
            }

            Projectile parent = Main.projectile[Parent];

            // Use the parent direction every frame so the strike follows it.
            Vector2 direction = parent.velocity.SafeNormalize(Vector2.UnitX);

            /*
             * The shortsword projectile is 38 pixels wide.
             * Move the dagger to the end of the shortsword:
             *
             * parent center
             * + half of parent length
             * + half of dagger length
             *
             * Increase EndOffset if you want a gap or a longer extension.
             */
            float endOffset =
                parent.width * 0.5f +
                Projectile.width * 0.5f;

            Projectile.Center = parent.Center + direction * endOffset;

            Projectile.velocity = direction;

            Projectile.spriteDirection =
                direction.X >= 0f ? 1 : -1;

            Projectile.rotation =
                direction.ToRotation() +
                MathHelper.PiOver2 -
                MathHelper.PiOver4 * (Projectile.spriteDirection - Projectile.spriteDirection);

            SetVisualOffsets();
        }

        private bool IsValidParent()
        {
            return Parent >= 0 &&
                   Parent < Main.maxProjectiles &&
                   Main.projectile[Parent].active &&
                   Main.projectile[Parent].type ==
                   ModContent.ProjectileType<AntlionDaggerProjectile>() &&
                   Main.projectile[Parent].owner == Projectile.owner;
        }

        private void SetVisualOffsets()
        {
            const int SpriteWidth = 14;
            const int SpriteHeight = 50;

            int halfSpriteWidth = SpriteWidth / 2;
            int halfSpriteHeight = SpriteHeight / 2;

            int halfProjectileWidth = Projectile.width / 2;
            int halfProjectileHeight = Projectile.height / 2;

            DrawOriginOffsetX = 0;
            DrawOffsetX = -(halfSpriteWidth - halfProjectileWidth);
            DrawOriginOffsetY = -(halfSpriteHeight - halfProjectileHeight);
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 =
                TileCuttingContext.AttackProjectile;

            Vector2 direction =
                Projectile.velocity.SafeNormalize(Vector2.UnitX);

            Vector2 start = Projectile.Center;
            Vector2 end = start + direction * Projectile.width;

            Utils.PlotTileLine(
                start,
                end,
                CollisionWidth,
                DelegateMethods.CutTiles
            );
        }

        public override bool? Colliding(
            Rectangle projHitbox,
            Rectangle targetHitbox)
        {
            Vector2 direction =
                Projectile.velocity.SafeNormalize(Vector2.UnitX);

            /*
             * This gives the dagger its own forward collision segment.
             * Since its center is already at the shortsword's end, the
             * combined weapon reaches farther forward.
             */
            Vector2 start =
                Projectile.Center - direction * (Projectile.width * 0.5f);

            Vector2 end =
                Projectile.Center + direction * (Projectile.width * 0.5f);

            float collisionPoint = 0f;

            return Collision.CheckAABBvLineCollision(
                targetHitbox.TopLeft(),
                targetHitbox.Size(),
                start,
                end,
                CollisionWidth,
                ref collisionPoint
            );
        }
    }
}
