using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons.Alchemist
{
    public class HoneyVial : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(70);
            Item.maxStack = Item.CommonMaxStack;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.damage = 23;
            Item.knockBack = 5f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Item.shootSpeed = 13f;
            Item.shoot = ModContent.ProjectileType<HoneyVialProjectile>();
        }
    }
}
