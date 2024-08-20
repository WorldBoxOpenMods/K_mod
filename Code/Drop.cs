using System.Collections.Generic;

namespace K_mod
{
    class K_Drop
    {
        public static void init()
        {
            DropAsset item = new()
            {
                id = "item_rain",
                path_texture = "drops/drop_item_rain",
                random_frame = true,
                default_scale = 0.1f,
                sound_drop = "event:/SFX/DROPS/DropRainGamma",
                action_landed = new DropsAction((pTile, pDropID) => action_item_rain(pTile, pDropID, 0))
            };
            AssetManager.drops.add(item);





            // DropAsset gamma2 = new DropAsset();
            // gamma2.id = "gamma_rain3";
            // gamma2.path_texture = "drops/drop_gamma_rain";
            // gamma2.random_frame = true;
            // gamma2.default_scale = 0.1f;
            // gamma2.sound_drop = "event:/SFX/DROPS/DropRainGamma";
            // gamma2.action_landed = new DropsAction((pTile, pDropID) => action2_gamma_rain(pTile, pDropID, 1));
            // AssetManager.drops.add(gamma2);

            // DropAsset gamma3 = new DropAsset();
            // gamma3.id = "gamma_rain4";
            // gamma3.path_texture = "drops/drop_gamma_rain";
            // gamma3.random_frame = true;
            // gamma3.default_scale = 0.1f;
            // gamma3.sound_drop = "event:/SFX/DROPS/DropRainGamma";
            // gamma3.action_landed = new DropsAction((pTile, pDropID) => action_gamma_rain3(pTile, pDropID, 0));
            // AssetManager.drops.add(gamma3);




            // DropAsset gamma4 = new DropAsset();
            // gamma4.id = "gamma_rain5";
            // gamma4.path_texture = "drops/drop_gamma_rain";
            // gamma4.random_frame = true;
            // gamma4.default_scale = 0.1f;
            // gamma4.sound_drop = "event:/SFX/DROPS/DropRainGamma";
            // gamma4.action_landed = new DropsAction((pTile, pDropID) => action_gamma_rain3(pTile, pDropID, 1));
            // AssetManager.drops.add(gamma4);





            // DropAsset gamma5 = new DropAsset();
            // gamma5.id = "gamma_rain6";
            // gamma5.path_texture = "drops/drop_gamma_rain";
            // gamma5.random_frame = true;
            // gamma5.default_scale = 0.1f;
            // gamma5.sound_drop = "event:/SFX/DROPS/DropRainGamma";
            // gamma5.action_landed = new DropsAction((pTile, pDropID) => action_gamma_rain4(pTile, pDropID, 0));
            // AssetManager.drops.add(gamma5);





            // DropAsset gamma6 = new DropAsset();
            // gamma6.id = "gamma_rain7";
            // gamma6.path_texture = "drops/drop_gamma_rain";
            // gamma6.random_frame = true;
            // gamma6.default_scale = 0.1f;
            // gamma6.sound_drop = "event:/SFX/DROPS/DropRainGamma";
            // gamma6.action_landed = new DropsAction((pTile, pDropID) => action_gamma_rain4(pTile, pDropID, 1));
            // AssetManager.drops.add(gamma6);

        }
        public static void action_item_rain(WorldTile pTile = null, string pDropID = null, int p = 0)
        {
            List<string> trait_editor_gamma = PlayerConfig.instance.data.trait_editor_gamma;
            if (p == 0) { removeTraitRain(pTile, trait_editor_gamma); }
            else
            {
                useTraitRain(pTile, trait_editor_gamma);
            }
        }

        // public static void action_gamma_rain3(WorldTile pTile = null, string pDropID = null, int p = 0)
        // {
        //   var Units = World.world.units._container.getSimpleList();
        //   List<string> trait_editor_gamma = PlayerConfig.instance.data.trait_editor_gamma;
        //   foreach(var unit in Units)
        //   {
        // 	if (unit.city != null && pTile.zone.city != null && unit.city == pTile.zone.city)
        // 	{
        // 	  if (p == 0){useTraitRain2(unit, trait_editor_gamma);}
        //       else{removeTraitRain2(unit, trait_editor_gamma);}
        // 	}
        //   }
        // }

        // public static void action_gamma_rain4(WorldTile pTile = null, string pDropID = null, int p = 0)
        // {
        //   var Units = World.world.units._container.getSimpleList();
        //   List<string> trait_editor_gamma = PlayerConfig.instance.data.trait_editor_gamma;
        //   foreach(var unit in Units)
        //   {
        // 	if (unit.kingdom != null && pTile.zone.city != null && pTile.zone.city.kingdom != null && unit.kingdom == pTile.zone.city.kingdom)
        // 	{
        // 	  if (p == 0){useTraitRain2(unit, trait_editor_gamma);}
        //       else{removeTraitRain2(unit, trait_editor_gamma);}
        // 	}
        //   }
        // }

        public static void removeTraitRain(WorldTile pTile, List<string> pList)
        {
            if (pList.Count == 0) { return; }
            int i = 0;
            while (i < pList.Count)
            {
                string pID = pList[i];
                if (AssetManager.traits.get(pID) == null) { pList.RemoveAt(i); }
                else { i++; }
            }
            MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
            for (int j = 0; j < MapBox.instance.temp_map_objects.Count; j++)
            {
                Actor a = MapBox.instance.temp_map_objects[j].a;
                if (a.asset.can_edit_traits)
                {
                    foreach (string pTrait in pList)
                    {
                        a.removeTrait(pTrait);
                    }
                    a.startShake(0.3f, 0.1f, true, true);
                    a.startColorEffect(ActorColorEffect.White);
                }
            }
        }
        public static void useTraitRain(WorldTile pTile, List<string> pList)
        {
            if (pList.Count == 0) { return; }
            int i = 0;
            while (i < pList.Count)
            {
                string pID = pList[i];
                if (AssetManager.traits.get(pID) == null) { pList.RemoveAt(i); }
                else { i++; }
            }
            MapBox.instance.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
            for (int j = 0; j < MapBox.instance.temp_map_objects.Count; j++)
            {
                Actor a = MapBox.instance.temp_map_objects[j].a;
                if (a.asset.can_edit_traits)
                {
                    foreach (string pTrait in pList)
                    {
                        a.addTrait(pTrait, true);
                    }
                    a.startShake(0.3f, 0.1f, true, true);
                    a.startColorEffect(ActorColorEffect.White);
                }
            }
        }
        // public static void removeTraitRain2(Actor act, List<string> pList)
        // {
        //   if (pList.Count == 0){return;}
        //   int i = 0;
        //   while (i < pList.Count)
        //   {
        // 	string pID = pList[i];
        // 	if (AssetManager.traits.get(pID) == null){pList.RemoveAt(i);}
        // 	else{i++;}
        //   }
        //   if (act.asset.can_edit_traits)
        //   {
        // 	foreach (string pTrait in pList)
        // 	{
        // 	  act.removeTrait(pTrait);
        // 	}
        // 	act.startShake(0.3f, 0.1f, true, true);
        // 	act.startColorEffect(ActorColorEffect.White);
        //   }
        // }
        // public static void useTraitRain2(Actor act, List<string> pList)
        // {
        //   if (pList.Count == 0){return;}
        //   int i = 0;
        //   while (i < pList.Count)
        //   {
        // 	string pID = pList[i];
        // 	if (AssetManager.traits.get(pID) == null){pList.RemoveAt(i);}
        // 	else{i++;}
        //   }
        //   if (act.asset.can_edit_traits)
        //   {
        // 	foreach (string pTrait in pList)
        // 	{
        // 	  act.addTrait(pTrait);
        // 	}
        // 	act.startShake(0.3f, 0.1f, true, true);
        // 	act.startColorEffect(ActorColorEffect.White);
        //   }
        // }
    }
}
