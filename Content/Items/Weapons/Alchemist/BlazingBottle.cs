using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons.Alchemist
{
    public class BlazingBottle : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(silver: 35);
            Item.maxStack = Item.CommonMaxStack;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.damage = 3;
            Item.knockBack = 0.5f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Item.shootSpeed = 13f;
            Item.shoot = ModContent.ProjectileType<BlazingBottleProjectile>();
        }
    }
}
