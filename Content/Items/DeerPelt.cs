using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items
{
    public class DeerPelt : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 60;
        }

        public override void SetDefaults()
        {
            Item.width = 18; // The item texture's width
            Item.height = 22; // The item texture's height

            Item.maxStack = Item.CommonMaxStack; // The item's max stack value
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.Green;
        }
    }
}
