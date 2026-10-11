using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Anabasis.Content.Tiles.Walls
{
    public class SunkenWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(0x02, 0x8b, 0xa2), name);

            DustType = DustID.DungeonBlue;
            HitSound = SoundID.Dig;
        }
    }
}