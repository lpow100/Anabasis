using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons.Alchemist
{
    public class BoneSpikes : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 50);
            Item.maxStack = Item.CommonMaxStack;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.damage = 23;
            Item.knockBack = 5f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Item.shootSpeed = 13.5f;
            Item.shoot = ModContent.ProjectileType<BoneSpikesProjectile>();
        }
    }
}
