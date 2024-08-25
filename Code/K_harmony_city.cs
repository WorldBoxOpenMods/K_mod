using K_mod;
using HarmonyLib;
using System;
using UnityEngine;
using K_mod.Utils;
using ReflectionUtility;
using System.Collections.Generic;
using NCMS.Utils;
using ai.behaviours;
using ai;

namespace K_mod;
public class K_harmony_city
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(CityBehCheckLeader), "checkFindLeader")]
    public static bool checkFindLeader(City pCity)
    {
        Actor pActor = pCity.leader;
        if (pActor != null)
        {
            if (pActor.asset.id is "Ballista" or "Catapult")
            {
                pCity.removeLeader();
            }
        }
        return true;
    }
    [HarmonyPostfix]
    [HarmonyPatch(typeof(CityBehGiveInventoryItem), "tryToGiveItem")]
    public static void TryToGiveItemPostfix(City pCity, List<ItemData> pItems)
    {
        if (pCity.race == null)
            return;

        // Check toggle value and specific race IDs
        if (!PowerButtons.GetToggleValue("texture Cavalry") &&
            !IsCavalryRace(pCity.race.id))
        {
            return;
        }

        if (pCity.data.storage.get(SR.gold) < 10)
            return;

        float cavalryChance = CalculateCavalryChance(pCity);

        if (pCity.countProfession(UnitProfession.Warrior) > 0)
        {
            Actor actor = pCity.professionsDict[UnitProfession.Warrior].GetRandom<Actor>();

            if (actor == null || !actor.isAlive())
                return;

            // Check actor conditions and apply effects
            if (!actor.isKing() && !actor.isCityLeader() && !actor.hasStatus("effect_cavalry") && !actor.hasStatus("BigPig") &&
                !actor.hasStatus("rhino") && Toolbox.randomChance(cavalryChance))
            {
                ApplyActorEffect(actor, pCity.race.id, pItems, pCity);
                return;
            }
        }
    }

    private static bool IsCavalryRace(string raceId)
    {
        HashSet<string> cavalryRaces = new()
            {
                "human", "orc", "elf", "dwarf", "Arab", "Pig", "Rome", "Xia"
            };
        return cavalryRaces.Contains(raceId);
    }

    private static float CalculateCavalryChance(City pCity)
    {
        float defaultChance = 0.005f;
        float enhancedChance = 0.09f;

        Culture culture = pCity.getCulture();
        if (culture == null || !IsCavalryRace(pCity.race.id))
            return 0f;
        if (culture.hasTech("AW_hufuqishe") || pCity.race.id == "Arab" || pCity.race.id == "orc")
        {
            if (pCity.race.id == "Pig")
            { return 0.03f; }
            if (pCity.race.id == "dwarf")
            { return 0.06f; }
            return enhancedChance;
        }




        return defaultChance;
    }

    private static void ApplyActorEffect(Actor actor, string raceId, List<ItemData> pItems, City pCity)
    {
        if (actor.asset.id == "unit_Pig")
        {
            actor.addStatusEffect("BigPig");
            actor.removeStatusEffect("effect_cavalry");
            actor.data.health += 100;
        }
        else if (actor.asset.id == "unit_dwarf")
        {
            actor.addStatusEffect("rhino");
            actor.removeStatusEffect("effect_cavalry");
            actor.data.health += 250;
            City.giveItem(actor, pItems, pCity); // Assuming pItems and pCity are accessible
        }
        else
        {
            actor.addStatusEffect("effect_cavalry");
            actor.data.health += 100;
            City.giveItem(actor, pItems, pCity); // Assuming pItems and pCity are accessible
        }
    }
    [HarmonyPostfix]
    [HarmonyPatch(typeof(City), "updateAge")]

    public static void updateAgeCity(City __instance)
    {
        if (__instance == null || __instance.leader == null)
        {
            return;
        }

        __instance.gold_in_tax = __instance.getPopulationTotal(true) / 2;
        __instance.gold_out_homeless = __instance.getPopulationTotal(true) - __instance.status.housingTotal;
        if (__instance.gold_out_homeless < 0)
        {
            __instance.gold_out_homeless = 0;
        }
        __instance.gold_out_army = __instance.countProfession(UnitProfession.Warrior) / 2;
        __instance.gold_out_buildings = __instance.buildings.Count / 2;

        // Calculate cavalry expenses
        CalculateCavalryExpenses(__instance);

        // Enhance national strength effect
        if (PowerButtons.GetToggleValue("national_strength"))
        {
            EnhanceNationalStrength(__instance);
        }

        __instance.updatePopPoints();
    }

    private static void CalculateCavalryExpenses(City city)
    {
        if (city.getArmy() > 0)
        {
            float cavalryCost = 0;

            foreach (Actor actor in city.professionsDict[UnitProfession.Warrior])
            {
                if (actor.hasStatus("effect_cavalry"))
                {
                    cavalryCost += CalculateCavalryCost(actor);
                }
                else if (actor.hasStatus("rhino") || actor.hasStatus("BigPig"))
                {
                    cavalryCost += 3; //特定类型的成本
                }
            }

            city.data.storage.change("gold", -(int)cavalryCost);
            city.data.set("horse", (int)cavalryCost);
            city.gold_change += (int)cavalryCost;
        }
    }

    private static float CalculateCavalryCost(Actor actor)
    {
        float baseCost = 2; //骑兵的默认成本
        if (actor.race.id is "orc" or "Arab")
        {
            baseCost -= 1; //降低特定race的成本
        }
        return baseCost;
    }

    private static void EnhanceNationalStrength(City city)
    {
        int initialTax = city.gold_in_tax;
        if (city.leader.hasTrait("经济") && city.leader.stats[S.stewardship] >= 15)
        {
            city.gold_in_tax = (int)(city.getPopulationTotal(true) * 0.7);
        }

        int taxDifference = city.gold_in_tax - initialTax;
        int totalGoldChange = city.gold_in_tax - city.gold_out_army - city.gold_out_buildings - city.gold_out_homeless;
        if (totalGoldChange < 0)
        {
            totalGoldChange = 0;
        }

        float stewardshipModifier = CalculateStewardshipModifier(city);

        float taxIncrease = (float)(totalGoldChange * 0.4 * stewardshipModifier) + taxDifference;

        city.gold_change += (int)taxIncrease;
        city.data.storage.change("gold", (int)taxIncrease);
        city.data.set("增税收", (int)taxIncrease);
    }

    private static float CalculateStewardshipModifier(City city)
    {
        float stewardship = city.leader.stats[S.stewardship];
        float modifier = stewardship - 5;
        if (stewardship <= 10)
        {
            stewardship += 2;
            if (stewardship > 5)
            {
                modifier = 0;
            }
        }
        //限制（如有必要）
        // if (modifier > 200)
        // {
        //     modifier = 200;
        // }
        return modifier;
    }
}