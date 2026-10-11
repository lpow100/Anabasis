using Anabasis.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Anabasis.Core.ModPlayers
{
    public class DashPlayer : ModPlayer
    {
        public int dashDuration;
        public bool IsDashing
        {
            get => dashDuration > 0;
        }
        public float currentDashSpeed;
        public AnabasisDashManager.DashType currentDashType;
        public int currentDashDamage;
        public HashSet<int> damagedDuringDash = [];
        public bool pounceImpactTriggered;
        public int dashCooldown;

        public override void PostUpdate()
        {
            AnabasisDashManager.UpdateDashes(Player);
        }
    }
}
