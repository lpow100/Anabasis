using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Anabasis.Content.Bosses.PhobosAndDeimos
{
    public class Phobos : BaseMartianMoon
    {
        protected override int PartnerType => ModContent.NPCType<Deimos>();
        protected override bool IsCharger => true;
    }
}
