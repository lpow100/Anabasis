using Anabasis.Content.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons.Blitz
{
    public class WoodenThorn : DaggerWeapon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.damage = 7;
            Item.knockBack = 4f;
            Item.width = 24;
            Item.height = 24;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 0, 10);

            Item.shoot = ModContent.ProjectileType<WoodenThornProjectile>();
            Item.shootSpeed = 2.1f;
        }

        protected override DashData OnDash(Player player)
        {
            return new DashData(10, 5.0f, 300);
        }
    }
}
