using Anabasis.Content.Items.Equipment;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Common.GlobalItems
{
    public class AlchemistEmblemShimmer : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ItemID.SummonerEmblem)
            {
                ItemID.Sets.ShimmerTransformToItem[entity.type] = ModContent.ItemType<AlchemistEmblem>();
            }
        }
    }
}
