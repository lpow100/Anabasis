using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons.Alchemist
{
    public class BottleCannon : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 3);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.damage = 71;
            Item.knockBack = 7f;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Item.shootSpeed = 13.75f;
            Item.shoot = ModContent.ProjectileType<GlassShards>();
        }
    }
}
