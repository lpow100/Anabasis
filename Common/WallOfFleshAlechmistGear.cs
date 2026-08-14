using Anabasis.Content.Items.Equipment;
using Anabasis.Content.Items.Weapons.Alchemist;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Anabasis.Common
{
    public class WoFDropsAlchemyLoot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
                // Adapted from Calamity mod's code, https://github.com/CalamityTeam/CalamityModPublic/blob/1.4.4/NPCs/CalamityGlobalNPCLoot.cs
                var wofRootRules = npcLoot.Get(false);

                
                try
                {
                    IItemDropRule notExpert = wofRootRules.Find(
                        (rule) => rule is LeadingConditionRule wofLeadingConditionRule &&
                        wofLeadingConditionRule.condition is Conditions.NotExpert
                    );
                    if (notExpert is LeadingConditionRule wofLeadingConditionRule_NotExpert)
                    {
                        wofLeadingConditionRule_NotExpert.ChainedRules.RemoveAll((chainAttempt) =>
                            chainAttempt is Chains.TryIfSucceeded c &&
                            c.RuleToChain is OneFromOptionsNotScaledWithLuckDropRule emblems &&
                            emblems.dropIds[0] == ItemID.WarriorEmblem);

                        wofLeadingConditionRule_NotExpert.OnSuccess(ItemDropRule.OneFromOptions(1,
                            ItemID.WarriorEmblem, ItemID.RangerEmblem, ItemID.SorcererEmblem, ItemID.SummonerEmblem,
                            ModContent.ItemType<AlchemistEmblem>()
                        ));
                    }
                }
                catch (ArgumentNullException) { }

                try
                {
                    IItemDropRule notExpert = wofRootRules.FindLast(
                        (rule) => rule is LeadingConditionRule wofLeadingConditionRule &&
                        wofLeadingConditionRule.condition is Conditions.NotExpert
                    );
                    if (notExpert is LeadingConditionRule wofLeadingConditionRule_NotExpert)
                    {
                        wofLeadingConditionRule_NotExpert.ChainedRules.RemoveAll((chainAttempt) =>
                            chainAttempt is Chains.TryIfSucceeded c &&
                            c.RuleToChain is OneFromOptionsNotScaledWithLuckDropRule weapons &&
                            weapons.dropIds[0] == ItemID.BreakerBlade);

                        wofLeadingConditionRule_NotExpert.OnSuccess(ItemDropRule.OneFromOptions(1,
                            ItemID.BreakerBlade, ItemID.ClockworkAssaultRifle, ItemID.LaserRifle, ItemID.FireWhip,
                            ModContent.ItemType<BottleCannon>()
                        ));
                    }
                }
                catch (ArgumentNullException) { }
            }
        }
    }

        public class WoFBagExtraLoot : GlobalItem
        {
            public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
            {
                if (item.type == ItemID.WallOfFleshBossBag)
                {
                    var itemLootRule = itemLoot.Get(false);

                    itemLootRule.RemoveAll((chainAttempt) =>
                            chainAttempt is Chains.TryIfSucceeded c &&
                            c.RuleToChain is OneFromOptionsNotScaledWithLuckDropRule emblems &&
                            emblems.dropIds[0] == ItemID.WarriorEmblem);

                    itemLootRule.RemoveAll((chainAttempt) =>
                            chainAttempt is Chains.TryIfSucceeded c &&
                            c.RuleToChain is OneFromOptionsNotScaledWithLuckDropRule emblems &&
                            emblems.dropIds[0] == ItemID.BreakerBlade);

                    itemLoot.Add(ItemDropRule.OneFromOptions(1,
                        ItemID.WarriorEmblem, ItemID.RangerEmblem, ItemID.SorcererEmblem, ItemID.SummonerEmblem,
                        ModContent.ItemType<AlchemistEmblem>()
                    ));

                    itemLoot.Add(ItemDropRule.OneFromOptions(1,
                        ItemID.BreakerBlade, ItemID.ClockworkAssaultRifle, ItemID.LaserRifle, ItemID.FireWhip,
                        ModContent.ItemType<BottleCannon>()
                    ));
                }
            }
        }
    }
