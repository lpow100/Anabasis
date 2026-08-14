using Anabasis.Common.DamageClasses;
using Anabasis.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Equipment
{
    public class AlchemistEmblem : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;

            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(gold: 2);

            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.WarriorEmblem;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<AlchemistDamageClass>() *= 1.15f;
        }
    }
}
