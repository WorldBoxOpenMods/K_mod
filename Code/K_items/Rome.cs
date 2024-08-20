using System;
using UnityEngine;
using ReflectionUtility;
using System.Collections.Generic;

namespace K_mod.K_items
{
    class Rome
    {
        [Obsolete]
        public static void init()
        {
            // 初始化装备
            InitializeArmors();

            // 初始化武器
            InitializeWeapons();
        }

        [Obsolete]
        private static void InitializeArmors()
        {
            // 铁铠装备
            ItemAsset armour1 = CreateAndAddArmor("armour1", "iron", 30, 10, 250, 40f, 150, 20);
            ItemAsset armour2 = CreateAndAddArmor("armour2", "iron", 40, 15, 280, 50f, 150, 30);
            ItemAsset armour3 = CreateAndAddArmor("armour3", "iron", 60, 20, 300, 60f, 200, 35);

            // 锁子甲装备
            ItemAsset chainMail1 = CreateAndAddArmor("chainMail1", "iron", 15, 5, 50, 10f, 100, 5);
            ItemAsset chainMail2 = CreateAndAddArmor("chainMail2", "iron", 15, 5, 60, 20f, 150, 0);

            // 皮甲装备
            ItemAsset leatherArmor1 = CreateAndAddArmor("leatherArmor1", "leather", 5, 5, 40, 0f, 50, 0);
            ItemAsset leatherArmor2 = CreateAndAddArmor("leatherArmor2", "leather", 10, 6, 45, 0f, 100, 0);

            // 板甲装备
            ItemAsset plateArmor1 = CreateAndAddArmor("plateArmor1", "iron", 10, 15, 280, 35f, 300, 30);
            ItemAsset plateArmor2 = CreateAndAddArmor("plateArmor2", "iron", 10, 15, 300, 50f, 300, 30);

            // 罗马头盔装备
            ItemAsset romanHelmet = CreateAndAddArmor("RomanHelmet", "iron", 5, 5, 60, 0f, 60, 10);
        }

        [Obsolete]
        private static ItemAsset CreateAndAddArmor(string id, string material, int cost1, int cost2, int value, float reduction, int health, int armor)
        {
            ItemAsset Armor = AssetManager.items.clone(id, "_equipment");
            Armor.id = id;
            AssetManager.items.add(Armor);
            Armor.materials = List.Of<string>(new string[] { material });
            Armor.setCost(cost1, material, cost2, "", 0);
            Armor.equipment_value = value;
            Armor.base_stats[S.knockback_reduction] = reduction;
            Armor.base_stats[S.health] = health;
            Armor.base_stats[S.armor] = armor;
            Armor.equipmentType = EquipmentType.Armor;
            Armor.name_class = "item_class_armor";
            Armor.name_templates = List.Of<string>(new string[] { "armor_name" });
            addItemSprite(Armor.id, Armor.materials[0]);
            return Armor;
        }

        [Obsolete]
        private static void InitializeWeapons()
        {

            //武器
            ItemAsset mainzGladius = AssetManager.items.clone("mainzGladius", "sword");
            mainzGladius.id = "mainzGladius";
            AssetManager.items.add(mainzGladius);
            mainzGladius.materials = List.Of<string>(new string[] { "iron" });
            mainzGladius.setCost(30, "iron", 5, "", 0);
            mainzGladius.equipment_value = 130;
            mainzGladius.base_stats[S.damage] = 25;
            mainzGladius.base_stats[S.attack_speed] = 25;
            mainzGladius.base_stats[S.critical_chance] = 0.2f;
            mainzGladius.equipmentType = EquipmentType.Weapon;
            mainzGladius.name_class = "item_class_weapon";
            mainzGladius.name_templates = Toolbox.splitStringIntoList(new string[] { "sword_name#30", "sword_name_king#3", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom" });
            addItemSprite(mainzGladius.id, mainzGladius.materials[0]);

            ItemAsset gladius = AssetManager.items.clone("gladius", "sword");
            gladius.id = "gladius";
            AssetManager.items.add(gladius);
            gladius.materials = List.Of<string>(new string[] { "iron" });
            gladius.setCost(10, "iron", 3, "", 0);
            gladius.equipment_value = 100;
            gladius.base_stats[S.damage] = 20;
            gladius.base_stats[S.range] = -1f;
            gladius.base_stats[S.attack_speed] = 40;
            gladius.base_stats[S.critical_chance] = 0.1f;
            gladius.equipmentType = EquipmentType.Weapon;
            gladius.name_class = "item_class_weapon";
            gladius.name_templates = Toolbox.splitStringIntoList(new string[] { "sword_name#30", "sword_name_king#3", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom" });
            addItemSprite(gladius.id, gladius.materials[0]);


            ItemAsset RomanJavelin = AssetManager.items.clone("RomanJavelin", "_range");
            RomanJavelin.id = "RomanJavelin";
            RomanJavelin.projectile = "javelin";
            RomanJavelin.path_slash_animation = "effects/slashes/slash_sword";
            RomanJavelin.materials = List.Of<string>(new string[] { "wood" });
            RomanJavelin.equipment_value = 100;
            RomanJavelin.base_stats[S.range] = 5f;
            RomanJavelin.base_stats[S.critical_chance] = 0.1f;
            RomanJavelin.base_stats[S.critical_damage_multiplier] = 0.2f;
            RomanJavelin.base_stats[S.damage] = 10;
            RomanJavelin.equipmentType = EquipmentType.Weapon;
            RomanJavelin.name_class = "item_class_weapon";
            RomanJavelin.name_templates = Toolbox.splitStringIntoList(new string[] { "bow_name#30", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom" });
            AssetManager.items.add(RomanJavelin);
            addItemSprite(RomanJavelin.id, RomanJavelin.materials[0]);
            RomanJavelin.action_attack_target = (AttackAction)Delegate.Combine(RomanJavelin.action_attack_target, new AttackAction(crossbowAttack));
        }

        // public static bool stoenEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null) 
        // {
        //   stone(pTile, 0.25f);
        //   return true;
        // }
        // public static void stone(WorldTile pTile, float pScale = 0.25f)
        // {
        //   BaseEffect baseEffect = EffectsLibrary.spawnAtTile("fx_lightning_big", pTile, pScale);
        //   if (baseEffect == null)
        //   {
        //     return;
        //   }
        //   int pRad = (int)(pScale * 25f);
        //   MapAction.damageWorld(pTile, pRad, AssetManager.terraform.get("lightning_power"), null);
        //   // baseEffect.sprRenderer.flipX = Toolbox.randomBool();
        //   // MapAction.checkLightningAction(pTile.pos, pRad);
        //   MapAction.checkSantaHit(pTile.pos, pRad);
        //   MapAction.checkUFOHit(pTile.pos, pRad);
        // }
        [Obsolete]
        public static bool armorPiercingCrossbowAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                if (a == null)
                {
                    return false;
                }
                if (Toolbox.randomChance(0.6f))
                {
                    a.addStatusEffect("breakingArmor", 5f);
                }
            }
            return true;
        }

        [Obsolete]
        public static bool Continuous_firing(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf != null)
            {
                Actor a = Reflection.GetField(pSelf.GetType(), pSelf, "a") as Actor;
                if (a == null)
                {
                    return false;
                }
                a.data.get("Continuous_firing", out int pResult, 0);
                pResult++;
                a.data.set("Continuous_firing", pResult);
            }
            return true;
        }

        [Obsolete]
        public static bool burn(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                if (a == null)
                {
                    return false;
                }
                if (Toolbox.randomChance(0.3f))
                {
                    a.addStatusEffect("burning", 5f);
                }
            }
            return true;
        }

        [Obsolete]
        public static bool crossbowAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                if (a == null)
                {
                    return false;
                }
                if (Toolbox.randomChance(0.3f))
                {
                    a.addStatusEffect("breakingArmor", 10f);
                }
            }
            return true;
        }

        [Obsolete]
        public static bool HeavyWeaponsAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // if (pSelf !== null)
            // {
            // Actor s = Reflection.GetField(pTarget.GetType(), pSelf, "s") as Actor;
            // s.city
            // }
            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                if (a == null)
                {
                    return false;
                }
                a.addStatusEffect("breakingArmor", 10f);
            }
            return true;
        }

        [Obsolete]
        public static void addItemSprite(string id, string material)
        {
            var dictItems = Reflection.GetField(typeof(ActorAnimationLoader), null, "dictItems") as Dictionary<string, Sprite>;
            var sprite = Resources.Load<Sprite>("ui/Icons/items/w_" + id + "_" + material);
            dictItems.Add(sprite.name, sprite);
        }
    }
}