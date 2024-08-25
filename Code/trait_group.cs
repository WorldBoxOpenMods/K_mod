using System;
using System.Collections.Generic;
using ReflectionUtility;

namespace K_mod
{
    class trait_group
    {
        public static string kmod = "kmod";

        
        public static void init()
        {
            ActorTraitGroupAsset kmod = new()
            {
                id = "kmod",
                name = "trait_group_kmod",
                color = Toolbox.makeColor("#A3AFFF", -1f)
            };
            AssetManager.trait_groups.add(kmod);
            AddTraitGroupToLocalizedLibrary("en", kmod.id, "Rome");
            AddTraitGroupToLocalizedLibrary("cz", kmod.id, "罗马");
        }

        
        private static void AddTraitGroupToLocalizedLibrary(string planguage, string id, string name)
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            if (planguage == language)
            {
                Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
                localizedText.Add("trait_group_" + id, name);
            }
        }

    }
}
