using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items
{
    public class Neutronium : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;

            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.buyPrice(gold: 4);
            Item.rare = ItemRarityID.Purple; // TODO: Make higher rarity
        }
    }
}
