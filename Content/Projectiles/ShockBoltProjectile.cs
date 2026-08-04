using Anabasis.Common.DamageClasses;
using Anabasis.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Projectiles
{
    class ShockBoltProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            AIType = ProjectileID.WoodenArrowFriendly;

            Projectile.width = 14;
            Projectile.height = 26;

            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = ModContent.GetInstance<AlchemistDamageClass>(); // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
        }

        public override void AI()
        {
            base.AI();

            Lighting.AddLight(Projectile.Center, 0.8f, 0.8f, 1.0f); // R G B values from 0 to 1f. This is the red from the Crimson Heart pet
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ShockedDebuff>(), 24);
        }
    }
}
