using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

/*
namespace Anabasis.Common
{
    public class WallOfFleshAlchemistGear : ModNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
                // Emblem choice - guaranteed, pick one
                npcLoot.Add(ItemDropRule.OneFromOptions(1,
                    ModContent.ItemType<YourEmblemMelee>(),
                    ModContent.ItemType<YourEmblemRanged>(),
                    ModContent.ItemType<YourEmblemMagic>(),
                    ModContent.ItemType<YourEmblemSummon>()
                ));

                // Class weapon choice - guaranteed, pick one
                npcLoot.Add(ItemDropRule.OneFromOptions(1,
                    ModContent.ItemType<YourWeaponMelee>(),
                    ModContent.ItemType<YourWeaponRanged>(),
                    ModContent.ItemType<YourWeaponMagic>(),
                    ModContent.ItemType<YourWeaponSummon>()
                ));
            }
        }
    }

    public class WoFBagExtraLoot : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ItemID.WallofFleshTreasureBag)
            {
                // Remove vanilla's emblem choice rule
                itemLoot.RemoveWhere(rule =>
                    rule is ItemDropWithConditionRule == false &&
                    DropsAnyOf(rule, ItemID.WarriorEmblem, ItemID.RangerEmblem, ItemID.SorcererEmblem, ItemID.SummonerEmblem)
                );

                // Remove vanilla's class weapon choice rule
                itemLoot.RemoveWhere(rule =>
                    DropsAnyOf(rule, ItemID.BreakerBlade, ItemID.ClockworkAssaultRifle, ItemID.LaserRifle, ItemID.Firecracker)
                );

                // Re-add emblem choice with your item included (now 5 options, 20% each)
                itemLoot.Add(ItemDropRule.OneFromOptions(1,
                    ItemID.WarriorEmblem, ItemID.RangerEmblem, ItemID.SorcererEmblem, ItemID.SummonerEmblem,
                    ModContent.ItemType<YourEmblem>()
                ));

                // Re-add weapon choice with your item included (now 5 options, 20% each)
                itemLoot.Add(ItemDropRule.OneFromOptions(1,
                    ItemID.BreakerBlade, ItemID.ClockworkAssaultRifle, ItemID.LaserRifle, ItemID.Firecracker,
                    ModContent.ItemType<YourWeapon>()
                ));
            }
        }

        private bool DropsAnyOf(IItemDropRule rule, params int[] itemTypes)
        {
            if (rule is OneFromOptionsDropRule options)
                return options.dropIds.Any(id => itemTypes.Contains(id));
            return false;
        }
    }
}*/
