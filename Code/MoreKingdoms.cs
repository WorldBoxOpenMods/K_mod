namespace K_mod
{
    internal class MoreKingdoms
    {

        internal void init()
        {



            var kingdomAsset = AssetManager.kingdoms.get("human");
            kingdomAsset.addFriendlyTag("Rome");
            kingdomAsset.addFriendlyTag("Arab");
            // kingdomAsset.addFriendlyTag("Russia");


            var kingdomNomadsAsset = AssetManager.kingdoms.get("nomads_human");
            kingdomNomadsAsset.addFriendlyTag("Rome");
            kingdomNomadsAsset.addFriendlyTag("Arab");
            // kingdomNomadsAsset.addFriendlyTag("Russia");

            var kingdomAsset2 = AssetManager.kingdoms.get("orc");
            kingdomAsset2.addFriendlyTag("tame");

            var kingdomNomadsAsset2 = AssetManager.kingdoms.get("nomads_orc");
            kingdomNomadsAsset2.addFriendlyTag("tame");


            AssetManager.kingdoms.add(new KingdomAsset
            {
                id = "empty",
                civ = true
            });
            AssetManager.kingdoms.add(new KingdomAsset
            {
                id = "nomads_empty",
                nomads = true,
                mobs = true
            });
            #region Rome
            //主要国家
            KingdomAsset addKingdom7 = AssetManager.kingdoms.clone("Rome", "empty");
            addKingdom7.addTag("civ");
            addKingdom7.addTag("Rome");
            addKingdom7.addFriendlyTag("human");
            addKingdom7.addFriendlyTag("Arab");
            addKingdom7.addFriendlyTag("Rome");
            addKingdom7.addFriendlyTag("tame");
            addKingdom7.addFriendlyTag("neutral");
            addKingdom7.addFriendlyTag("good");
            addKingdom7.addEnemyTag("bandits");
            newHiddenKingdom(addKingdom7);
            //临时用的国家
            KingdomAsset addKingdom8 = AssetManager.kingdoms.clone("nomads_Rome", "nomads_empty");
            addKingdom8.addTag("civ");
            addKingdom8.addTag("Rome");
            addKingdom8.addFriendlyTag("Rome");
            addKingdom8.addFriendlyTag("Arab");
            addKingdom8.addFriendlyTag("human");
            addKingdom8.addFriendlyTag("tame");
            addKingdom8.addFriendlyTag("neutral");
            addKingdom8.addFriendlyTag("good");
            addKingdom8.addEnemyTag("bandits");
            newHiddenKingdom(addKingdom8);
            #endregion
            #region Arab
            //主要国家
            KingdomAsset addKingdom9 = AssetManager.kingdoms.clone("Arab", "empty");
            addKingdom9.addTag("civ");
            addKingdom9.addTag("Arab");
            addKingdom9.addFriendlyTag("Arab");
            addKingdom9.addFriendlyTag("human");
            addKingdom9.addFriendlyTag("tame");
            addKingdom9.addFriendlyTag("Rome");
            addKingdom9.addFriendlyTag("neutral");
            addKingdom9.addFriendlyTag("good");
            addKingdom9.addEnemyTag("bandits");
            newHiddenKingdom(addKingdom9);
            //临时用的国家
            KingdomAsset addKingdom10 = AssetManager.kingdoms.clone("nomads_Arab", "nomads_empty");
            addKingdom10.addTag("civ");
            addKingdom10.addTag("Arab");
            addKingdom10.addFriendlyTag("Arab");
            addKingdom10.addFriendlyTag("Rome");
            addKingdom10.addFriendlyTag("tame");
            addKingdom10.addFriendlyTag("human");
            addKingdom10.addFriendlyTag("neutral");
            addKingdom10.addFriendlyTag("good");
            addKingdom10.addEnemyTag("bandits");
            newHiddenKingdom(addKingdom10);
            #endregion
            #region Russia
            //主要国家
            KingdomAsset addKingdom11 = AssetManager.kingdoms.clone("Russia", "empty");
            addKingdom11.addTag("civ");
            addKingdom11.addTag("Russia");
            addKingdom11.addFriendlyTag("Russia");
            addKingdom11.addFriendlyTag("human");
            addKingdom11.addFriendlyTag("Rome");
            addKingdom11.addFriendlyTag("neutral");
            addKingdom11.addFriendlyTag("good");
            addKingdom11.addEnemyTag("bandits");
            newHiddenKingdom(addKingdom11);
            //临时用的国家
            KingdomAsset addKingdom12 = AssetManager.kingdoms.clone("nomads_Russia", "nomads_empty");
            addKingdom12.addTag("civ");
            addKingdom12.addTag("Russia");
            addKingdom12.addFriendlyTag("Russia");
            addKingdom12.addFriendlyTag("human");
            addKingdom12.addFriendlyTag("neutral");
            addKingdom12.addFriendlyTag("good");
            addKingdom12.addEnemyTag("bandits");
            newHiddenKingdom(addKingdom12);
            #endregion
            KingdomAsset addKingdom13 = (new KingdomAsset
            {
                id = "tame",
                mobs = true,
                count_as_danger = false
            });
            addKingdom13.addTag("tame");
            addKingdom13.addTag("good");
            addKingdom13.addFriendlyTag("Rome");
            addKingdom13.addFriendlyTag("Arab");
            addKingdom13.addFriendlyTag("orc");
            addKingdom13.addFriendlyTag("elf");
            addKingdom13.addFriendlyTag("dwarf");
            addKingdom13.addFriendlyTag("human");
            addKingdom13.addFriendlyTag(SK.good);
            addKingdom13.addFriendlyTag(SK.neutral);
            addKingdom13.addFriendlyTag(SK.nature_creature);
            addKingdom13.addFriendlyTag(SK.living_houses);
            addKingdom13.addFriendlyTag(SK.snowman);
            addKingdom13.addFriendlyTag(SK.civ);
            newHiddenKingdom(addKingdom13);


            //initColors();
            //BannerGenerator.loadBanners($"{Main.mainPath}/EmbededResources/banners");
        }
        private Kingdom newHiddenKingdom(KingdomAsset pAsset)
        {
            Kingdom kingdom = World.world.kingdoms.newObject(pAsset.id);
            kingdom.asset = pAsset;
            kingdom.createHidden();
            // kingdom.id = pAsset.id;
            kingdom.data.name = pAsset.id;
            KingdomManager kingdomManager = MapBox.instance.kingdoms;
            kingdomManager.setupKingdom(kingdom, false);
            return kingdom;
        }





    }

}
