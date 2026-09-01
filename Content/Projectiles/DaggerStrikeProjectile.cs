using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace Anabasis.Content.Projectiles
{
    public class DaggerStrikeProjectile : ModProjectile
    {
        private const int JabDuration = 14;
        private const float StartDistance = 12f;
        private const float JabDistance = 54f;

        public int Timer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        private Vector2 Direction =>
            Projectile.velocity.SafeNormalize(Vector2.UnitX);

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(18f, 18f);

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = JabDuration / 2 + 2;

            // Set this to false if the projectile uses its normal texture.
            Projectile.hide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            Timer++;

            if (Timer >= JabDuration)
            {
                Projectile.Kill();
                return;
            }

            player.heldProj = Projectile.whoAmI;

            Vector2 direction = Direction;

            // Smooth, fast forward jab.
            float progress = Timer / (float)(JabDuration - 1);
            float easedProgress = 1f - (1f - progress) * (1f - progress);

            float distance = MathHelper.Lerp(
                StartDistance,
                JabDistance,
                easedProgress
            );

            Vector2 playerCenter = player.RotatedRelativePoint(
                player.MountedCenter,
                reverseRotation: false,
                addGfxOffY: false
            );

            Projectile.Center = playerCenter + direction * distance;

            Projectile.rotation =
                direction.ToRotation() + MathHelper.PiOver2;

            Projectile.spriteDirection =
                direction.X >= 0f ? 1 : -1;

            // Fade in and fade out.
            Projectile.Opacity =
                Utils.GetLerpValue(0f, 2f, Timer, clamped: true) *
                Utils.GetLerpValue(
                    JabDuration,
                    JabDuration - 3f,
                    Timer,
                    clamped: true
                );

            SetVisualOffsets();
        }

        private void SetVisualOffsets()
        {
            const int spriteWidth = 32;
            const int spriteHeight = 32;

            DrawOriginOffsetX = 0;
            DrawOffsetX = -(spriteWidth / 2 - Projectile.width / 2);
            DrawOriginOffsetY = -(spriteHeight / 2 - Projectile.height / 2);
        }

        public override bool ShouldUpdatePosition()
        {
            // Position is controlled manually in AI().
            return false;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;

            Vector2 direction = Direction;
            Vector2 start = Projectile.Center;
            Vector2 end = start + direction * 20f;

            Utils.PlotTileLine(
                start,
                end,
                10f * Projectile.scale,
                DelegateMethods.CutTiles
            );
        }

        public override bool? Colliding(
            Rectangle projHitbox,
            Rectangle targetHitbox
        )
        {
            Vector2 direction = Direction;

            // Hit slightly in front of the dagger.
            Vector2 start = Projectile.Center;
            Vector2 end = start + direction * 22f;

            float collisionPoint = 0f;

            return Collision.CheckAABBvLineCollision(
                targetHitbox.TopLeft(),
                targetHitbox.Size(),
                start,
                end,
                10f * Projectile.scale,
                ref collisionPoint
            );
        }
    }
}
