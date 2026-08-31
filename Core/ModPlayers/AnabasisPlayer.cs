using Anabasis.Core.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Anabasis.Core.ModPlayers
{
    public class AnabasisPlayer : ModPlayer
    {
        public int dashDuration;
        public float currentDashSpeed;
        public AnabasisDashManager.DashType currentDashType;
        public int currentDashDamage;
        public HashSet<int> damagedDuringDash;

        public override void PreUpdateMovement()
        {
            AnabasisDashManager.UpdateDashes(this.Player);
            if (dashDuration > 0)
            {
                dashDuration--;
            }
            if (dashDuration == 0)
                Player.dashType = 0;
        }
    }
}
