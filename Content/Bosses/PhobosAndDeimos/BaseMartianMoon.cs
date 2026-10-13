using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Bosses.PhobosAndDeimos
{
    public abstract class BaseMartianMoon : ModNPC
    {
        protected abstract int PartnerType { get; }
        public int partner = -1;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Bleeding] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Wet] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 58;
            NPC.height = 62;
            NPC.scale = 1.75f;
            NPC.damage = 180;
            NPC.defense = 18;
            NPC.lifeMax = 355000;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.npcSlots = 5f;
            NPC.value = Item.buyPrice(gold: 5);

            NPC.BossBar = ModContent.GetInstance<MartianMoonBossBar>();
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || partner != -1)
                return;

            foreach (NPC other in Main.npc)
            {
                if (other.active &&
                    other.whoAmI != NPC.whoAmI &&
                    other.ModNPC is BaseMartianMoon otherMoon &&
                    otherMoon.partner == -1)
                {
                    partner = other.whoAmI;
                    otherMoon.partner = NPC.whoAmI;
                    return;
                }
            }

            int playerSpawned = 1;
            if (source is EntitySource_BossSpawn spawn &&
                spawn.Target is Player bossSummoner)
            {
                playerSpawned = bossSummoner.whoAmI;
            }

            NPC.SpawnOnPlayer(playerSpawned, PartnerType);
        }

        protected void MoveToward(Vector2 destination, float speed)
        {
            Vector2 desired = destination - NPC.Center;
            if (desired.Length() > speed)
                desired = Vector2.Normalize(desired) * speed;

            NPC.velocity = (NPC.velocity * 24f + desired) / 25f;
        }
    }
}
