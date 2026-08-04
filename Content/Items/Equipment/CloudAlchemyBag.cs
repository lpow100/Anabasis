

using Anabasis.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Equipment
{
    public class CloudAlchemyBag : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;

            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 1, silver: 50);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AlchemistPlayer>().consumableSaveChance += 0.15f;
        }
    }
}
