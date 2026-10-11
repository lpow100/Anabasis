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

namespace Anabasis.Content.Tiles
{
    public class SunkenBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(0x02,0xcb,0xc2), name);

            DustType = DustID.DungeonBlue;
            HitSound = SoundID.Dig;
            MineResist = 2f;
            MinPick = 210;
        }
    }
}