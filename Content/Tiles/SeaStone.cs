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
    public class SeaStone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
            Main.tileOreFinderPriority[Type] = 410; // Metal Detector value, see https://terraria.wiki.gg/wiki/Metal_Detector
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(41, 51, 102), name);

            DustType = DustID.DungeonBlue;
            HitSound = SoundID.Tink;
            MineResist = 2f;
            MinPick = 65;
        }
    }
}