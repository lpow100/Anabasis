using Anabasis.Common.DamageClasses;
using Anabasis.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Equipment.Summoner
{
    public class PetConch : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;

            Item.accessory = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 45);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<SummonDamageClass>().Flat += 10;
            player.maxMinions += 1;
        }
    }
}
