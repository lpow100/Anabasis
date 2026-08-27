using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Anabasis.Content.Items.Weapons.Blitz
{
    public class WoodenThorn : DaggerWeapon
    {
        protected override DashData OnDash(Player player)
        {
            return new DashData(10, 4.0f, 50);
        }
    }
}
