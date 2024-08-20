using System;
using UnityEngine;
using System.Collections.Generic;
using ReflectionUtility;
using K_mod.Utils;

namespace K_mod
{
    class K_effects
    {
        [Obsolete]
        public static void init()
        {
            StatusEffect breakingArmor = new()
            {
                id = "breakingArmor",
                path_icon = "ui/Icons/effects/iconbreakingArmor",
                duration = 5f
            };
            breakingArmor.base_stats[S.armor] = -60f;
            breakingArmor.animated = false;
            AssetManager.status.add(breakingArmor);


            StatusEffect filling = new()
            {
                id = "filling",
                name = "status_title_filling",
                animated = false,
                path_icon = "ui/Icons/effects/filling",
                duration = 5f
            };
            filling.base_stats[S.speed] = -40f;
            AssetManager.status.add(filling);
            addTraitToLocalizedLibrary("cz", filling.id, "装填中...", "");
            addTraitToLocalizedLibrary("ch", filling.id, "装填中...", "");
            addTraitToLocalizedLibrary("en", filling.id, "filling...", "");

            StatusEffect effect_cavalry = new()
            {
                id = "effect_cavalry",
                name = "status_title_effect_cavalry",
                animated = false,
            };
            effect_cavalry.base_stats[S.knockback_reduction] = 0.4f;
            effect_cavalry.base_stats[S.knockback] = 0.25f;
            effect_cavalry.base_stats[S.speed] = 120f;
            effect_cavalry.base_stats[S.health] = 100;
            effect_cavalry.duration = 10000f;
            effect_cavalry.description = "status_description_effect_cavalry";
            effect_cavalry.path_icon = "ui/Icons/effects/iconcavalry";
            effect_cavalry.action = new WorldAction(K_effects_action.Cavalry);
            effect_cavalry.action_interval = 30f;
            effect_cavalry.remove_status.Add("BigPig");
            effect_cavalry.remove_status.Add("rhino");
            //effect_cavalry.special_effect_interval = 0.01f;
            AssetManager.status.add(effect_cavalry);
            addTraitToLocalizedLibrary("cz", effect_cavalry.id, "骑兵", "如果不是一名优秀的骑兵，我会是一名充满战斗热情的勇士");
            addTraitToLocalizedLibrary("ch", effect_cavalry.id, "骑兵", "如果不是一名优秀的骑兵，我会是一名充满战斗热情的勇士");
            addTraitToLocalizedLibrary("en", effect_cavalry.id, "cavalry", "cavalry");



            StatusEffect BigPig = new()
            {
                id = "BigPig",
                name = "status_title_BigPig",
                animated = false,
            };
            BigPig.base_stats[S.knockback_reduction] = 0.6f;
            BigPig.base_stats[S.knockback] = 0.4f;
            BigPig.base_stats[S.speed] = -10f;
            BigPig.base_stats[S.health] = 100;
            BigPig.base_stats[S.damage] = 20f;
            BigPig.duration = 10000f;
            BigPig.description = "status_description_BigPig";
            BigPig.path_icon = "ui/Icons/effects/iconBigPig";
            BigPig.action = new WorldAction(K_effects_action.Cavalry);
            BigPig.action_interval = 30f;
            //BigPig.special_effect_interval = 0.01f;
            AssetManager.status.add(BigPig);
            addTraitToLocalizedLibrary("cz", BigPig.id, "大肥猪", "大肥猪");
            addTraitToLocalizedLibrary("ch", BigPig.id, "大肥猪", "大肥猪");
            addTraitToLocalizedLibrary("en", BigPig.id, "Big Pig", "Big Pig");

            StatusEffect rhino = new()
            {
                id = "rhino",
                name = "status_title_rhino",
                animated = false,
            };
            rhino.base_stats[S.knockback_reduction] = 0.7f;
            rhino.base_stats[S.knockback] = 0.5f;
            rhino.base_stats[S.speed] = -20f;
            rhino.base_stats[S.attack_speed] = -20f;
            rhino.base_stats[S.health] = 250;
            rhino.base_stats[S.damage] = 25f;
            rhino.base_stats[S.armor] = 10f;
            rhino.duration = 10000f;
            rhino.description = "status_description_rhino";
            rhino.path_icon = "ui/Icons/effects/iconrhino";
            rhino.action = new WorldAction(K_effects_action.Cavalry);
            rhino.action_interval = 30f;
            //rhino.special_effect_interval = 0.01f;
            AssetManager.status.add(rhino);
            addTraitToLocalizedLibrary("cz", rhino.id, "犀牛骑士", "犀牛骑士");
            addTraitToLocalizedLibrary("ch", rhino.id, "犀牛骑士", "犀牛骑士");
            addTraitToLocalizedLibrary("en", rhino.id, "Rhinoceros Knight", "Rhinoceros Knight");

            StatusEffect charge = new()
            {
                id = "charge",
                name = "status_title_charge",
                animated = true,
                duration = 5f
            };
            charge.base_stats[S.speed] = 20f;
            charge.description = "status_description_charge";
            charge.path_icon = "ui/Icons/effects/iconcavalry";
            charge.action = new WorldAction(K_effects_action.charge_1);
            charge.animation_speed = 0.1f;
            charge.texture = "charge";
            charge.action_interval = 30f;
            // charge.remove_status.Add("BigPig");
            // charge.remove_status.Add("rhino");
            AssetManager.status.add(charge);
            addTraitToLocalizedLibrary("cz", charge.id, "冲锋", "荣耀！冲锋！");
            addTraitToLocalizedLibrary("ch", charge.id, "冲锋", "荣耀！冲锋！");
            addTraitToLocalizedLibrary("en", charge.id, "charge", "charge");


            StatusEffect ChargeCooling = new()
            {
                id = "ChargeCooling",
                name = "status_title_ChargeCooling",
                animated = false,
                duration = 10f,
                description = "status_description_ChargeCooling",
                path_icon = "ui/Icons/effects/iconcavalry"
            };
            AssetManager.status.add(ChargeCooling);
            addTraitToLocalizedLibrary("cz", ChargeCooling.id, "冲锋冷却", "冲锋冷却");
            addTraitToLocalizedLibrary("ch", ChargeCooling.id, "冲锋冷却", "冲锋冷却");
            addTraitToLocalizedLibrary("en", ChargeCooling.id, "Charge Cooling", "Charge Cooling");

            StatusEffect array = new()
            {
                id = "array",
                name = "status_title_array",
                // texture = "fx_status_slowness_t",
                duration = 99999f,
                animated = false,
                path_icon = "ui/Icons/iconSlowness",
                // array.base_stats[S.speed] = -80f;
                // array.base_stats[S.armor] = 60f;
                // array.base_stats[S.attack_speed] = -30f;
                description = "status_description_array"
            };
            array.remove_status.Add("caffeinated");
            array.remove_status.Add("charge");
            array.remove_status.Add("effect_cavalry");
            AssetManager.status.add(array);
            addTraitToLocalizedLibrary("cz", "array", "列阵", "列阵");
            addTraitToLocalizedLibrary("ch", "array", "列阵", "列阵");
            addTraitToLocalizedLibrary("en", "array", "列阵", "列阵");

        }


        [Obsolete]
        private static void addTraitToLocalizedLibrary(string pLanguage, string id, string name, string description)
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            if (language is not "en" and not "ch" and not "cz")
            {
                pLanguage = "en";
            }
            if (pLanguage == language)
            {
                Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
                localizedText.Add("status_title_" + id, name);
                localizedText.Add("status_description_" + id, description);
            }
        }
    }
}