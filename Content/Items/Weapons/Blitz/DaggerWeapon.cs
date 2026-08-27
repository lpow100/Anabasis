using Anabasis.Content.Buffs;
using Anabasis.Core.ModPlayers;
using Anabasis.Core.Systems;
using Steamworks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons.Blitz
{
    public struct DashData
    {
        public int LengthTicks;
        public float DashSpeed;
        public int ShockedOutDuration;

        /// <summary>
        /// Data about a dash
        /// </summary>
        /// <param name="lengthTicks">Min Length in ticks</param>
        /// <param name="shockedOutDuration">Duration of cooldown in ticks</param>
        public DashData(int lengthTicks, float dashSpeed, int shockedOutDuration)
        {
            LengthTicks = lengthTicks;
            DashSpeed = dashSpeed;
            ShockedOutDuration = shockedOutDuration;
        }
    }

    /// <summary>
    /// Base class for all Blitzer daggers.
    /// Left click: Short Range Stab, properties set in SetDefaults
    /// Right click: routes to OnDash. Cooldown is entirely represented by
    /// the ShockedOut debuff - no separate cooldown field/timer anywhere.
    /// </summary>
    public abstract class DaggerWeapon : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 12;
            Item.useTime = 12;
        }


        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                DashData data = OnDash(player);
                PerformDash(player, data);

                player.AddBuff(ModContent.BuffType<ShockedDebuff>(), data.ShockedOutDuration + data.LengthTicks);

                return true;
            }

            return base.UseItem(player);
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
                Item.useStyle = ItemUseStyleID.Shoot;
            else
                Item.useStyle = ItemUseStyleID.Rapier;

            return base.CanUseItem(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        /// <summary>
        /// Every dagger overrides this to define its dash distance and how
        /// long Shocked Out lasts afterward (i.e. its effective cooldown).
        /// </summary>
        protected abstract DashData OnDash(Player player);

        protected virtual void PerformDash(Player player, DashData data)
        {
            AnabasisDashManager.DashStart(player, data.LengthTicks, data.DashSpeed);
        }
    }
}
