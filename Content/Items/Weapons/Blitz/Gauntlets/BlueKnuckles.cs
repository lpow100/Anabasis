using Anabasis.Content.Buffs;
using Anabasis.Core;
using Anabasis.Core.ModPlayers;
using Anabasis.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons.Blitz.Gauntlets
{
    public class BlueKnuckles : ModItem
    {
        const int dashCooldown = 50;
        const int dashTime = 17;
        const float dashSpeed = 11.5f;
        const int dashDamage = 110;
        const int momentumCost = 27;

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 14;
            Item.useTime = dashCooldown;
            Item.damage = dashDamage;
            Item.DamageType = ModContent.GetInstance<BlitzDamageClass>();
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(silver: 180);
        }

        public override bool? UseItem(Player player)
        {
            AnabasisDashManager.DashStart(player, AnabasisDashManager.DashType.Pounce, dashTime, dashSpeed, dashCooldown, dashDamage, momentumCost);
            return base.UseItem(player);
        }
    }
}
