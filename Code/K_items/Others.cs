using System;
using UnityEngine;
using ReflectionUtility;
using System.Collections.Generic;

namespace K_mod.K_items
{
    class Others
    {
        [Obsolete]
        public static void init()
        {

            ItemAsset Arabic_Scimitar = AssetManager.items.clone("Arabic_Scimitar", "sword");
            Arabic_Scimitar.id = "Arabic_Scimitar";
            AssetManager.items.add(Arabic_Scimitar);
            Arabic_Scimitar.materials = List.Of<string>(new string[] { "iron" });
            Arabic_Scimitar.equipment_value = 130;
            Arabic_Scimitar.base_stats[S.damage] = 25;
            Arabic_Scimitar.base_stats[S.attack_speed] = 25;
            Arabic_Scimitar.base_stats[S.critical_chance] = 0.2f;
            Arabic_Scimitar.equipmentType = EquipmentType.Weapon;
            Arabic_Scimitar.name_class = "item_class_weapon";
            Arabic_Scimitar.name_templates = Toolbox.splitStringIntoList(new string[] { "sword_name#30", "sword_name_king#3", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom" });
            addItemSprite(Arabic_Scimitar.id, Arabic_Scimitar.materials[0]);

            ItemAsset crossbow = AssetManager.items.clone("crossbow", "_range");
            crossbow.id = "crossbow";
            crossbow.tech_needed = "weapon_bow";
            crossbow.projectile = "bolt";
            crossbow.path_slash_animation = "effects/slashes/slash_bow";
            crossbow.materials = List.Of<string>(new string[] { "wood" });
            crossbow.equipment_value = 100;
            crossbow.base_stats[S.range] = 20f;
            crossbow.base_stats[S.critical_chance] = 0.3f;
            crossbow.base_stats[S.critical_damage_multiplier] = 0.6f;
            crossbow.base_stats[S.damage] = 20;
            crossbow.base_stats[S.attack_speed] -= 20;
            crossbow.equipmentType = EquipmentType.Weapon;
            crossbow.name_class = "item_class_weapon";
            crossbow.name_templates = Toolbox.splitStringIntoList(new string[] { "bow_name#30", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom" });
            AssetManager.items.add(crossbow);
            addItemSprite(crossbow.id, crossbow.materials[0]);
            crossbow.action_attack_target = (AttackAction)Delegate.Combine(crossbow.action_attack_target, new AttackAction(crossbowAttack));

            ItemAsset armorPiercingCrossbow = AssetManager.items.clone("armorPiercingCrossbow", "_range");
            armorPiercingCrossbow.id = "armorPiercingCrossbow";
            armorPiercingCrossbow.tech_needed = "weapon_bow";
            armorPiercingCrossbow.projectile = "bolt";
            armorPiercingCrossbow.path_slash_animation = "effects/slashes/slash_spear";
            armorPiercingCrossbow.materials = List.Of<string>(new string[] { "iron" });
            armorPiercingCrossbow.equipment_value = 130;
            armorPiercingCrossbow.base_stats[S.range] = 23f;
            armorPiercingCrossbow.base_stats[S.critical_chance] = 0.45f;
            armorPiercingCrossbow.base_stats[S.critical_damage_multiplier] = 0.7f;
            armorPiercingCrossbow.base_stats[S.damage] = 25;
            armorPiercingCrossbow.base_stats[S.attack_speed] -= 25;
            armorPiercingCrossbow.equipmentType = EquipmentType.Weapon;
            armorPiercingCrossbow.name_class = "item_class_weapon";
            armorPiercingCrossbow.name_templates = Toolbox.splitStringIntoList(new string[] { "bow_name#30", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom" });
            AssetManager.items.add(armorPiercingCrossbow);
            addItemSprite(armorPiercingCrossbow.id, armorPiercingCrossbow.materials[0]);
            armorPiercingCrossbow.action_attack_target = (AttackAction)Delegate.Combine(armorPiercingCrossbow.action_attack_target, new AttackAction(armorPiercingCrossbowAttack));


            ItemAsset CrutchGun = AssetManager.items.clone("CrutchGun", "_range");
            CrutchGun.id = "CrutchGun";
            CrutchGun.projectile = "gun";
            CrutchGun.path_slash_animation = "effects/slashes/slash_bow";
            CrutchGun.materials = List.Of<string>(new string[] { "iron" });
            CrutchGun.setCost(80, "iron", 12, "", 0);
            CrutchGun.equipment_value = 200;
            CrutchGun.base_stats[S.range] = 6f;
            CrutchGun.base_stats[S.damage] = 40;
            CrutchGun.base_stats[S.attack_speed] = 50;
            CrutchGun.equipmentType = EquipmentType.Weapon;
            CrutchGun.name_class = "item_class_weapon";
            CrutchGun.name_templates = Toolbox.splitStringIntoList(new string[] { "bow_name#30", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom" });
            AssetManager.items.add(CrutchGun);
            addItemSprite(CrutchGun.id, CrutchGun.materials[0]);
            CrutchGun.action_attack_target = (AttackAction)Delegate.Combine(CrutchGun.action_attack_target, new AttackAction(Continuous_firing));
            CrutchGun.action_attack_target = (AttackAction)Delegate.Combine(CrutchGun.action_attack_target, new AttackAction(burn));

            ItemAsset YongleHandGun = AssetManager.items.clone("YongleHandGun", "_range");
            YongleHandGun.id = "YongleHandGun";
            YongleHandGun.projectile = "gun";
            YongleHandGun.path_slash_animation = "effects/slashes/slash_punch";
            YongleHandGun.materials = List.Of<string>(new string[] { "copper" });
            YongleHandGun.equipment_value = 70;
            YongleHandGun.setCost(20, "copper", 4, "", 0);
            YongleHandGun.base_stats[S.range] = 3f;
            YongleHandGun.base_stats[S.damage] = 5;
            YongleHandGun.base_stats[S.attack_speed] = -40;
            YongleHandGun.base_stats[S.projectiles] = 3;
            YongleHandGun.equipmentType = EquipmentType.Weapon;
            YongleHandGun.name_class = "item_class_weapon";
            YongleHandGun.name_templates = Toolbox.splitStringIntoList(new string[] { "bow_name#30", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom" });
            AssetManager.items.add(YongleHandGun);
            addItemSprite(YongleHandGun.id, YongleHandGun.materials[0]);
            YongleHandGun.action_attack_target = (AttackAction)Delegate.Combine(YongleHandGun.action_attack_target, new AttackAction(burn));


            ItemAsset ThreeEyeBlunderbuss = AssetManager.items.clone("ThreeEyeBlunderbuss", "_range");
            ThreeEyeBlunderbuss.id = "ThreeEyeBlunderbuss";
            ThreeEyeBlunderbuss.projectile = "gun";
            ThreeEyeBlunderbuss.path_slash_animation = "effects/slashes/slash_punch";
            ThreeEyeBlunderbuss.materials = List.Of<string>(new string[] { "iron" });
            ThreeEyeBlunderbuss.setCost(60, "iron", 10, "", 0);
            ThreeEyeBlunderbuss.equipment_value = 160;
            ThreeEyeBlunderbuss.base_stats[S.range] = 4f;
            ThreeEyeBlunderbuss.base_stats[S.damage] = 10;
            ThreeEyeBlunderbuss.base_stats[S.attack_speed] = -60;
            ThreeEyeBlunderbuss.base_stats[S.projectiles] = 3;
            ThreeEyeBlunderbuss.equipmentType = EquipmentType.Weapon;
            ThreeEyeBlunderbuss.name_class = "item_class_weapon";
            ThreeEyeBlunderbuss.name_templates = Toolbox.splitStringIntoList(new string[] { "bow_name#30", "weapon_name_city", "weapon_name_kingdom", "weapon_name_culture", "weapon_name_enemy_king", "weapon_name_enemy_kingdom" });
            AssetManager.items.add(ThreeEyeBlunderbuss);
            addItemSprite(ThreeEyeBlunderbuss.id, ThreeEyeBlunderbuss.materials[0]);
            ThreeEyeBlunderbuss.action_attack_target = (AttackAction)Delegate.Combine(ThreeEyeBlunderbuss.action_attack_target, new AttackAction(burn));

            //攻击方式
            ItemAsset Ballista_Arrows = AssetManager.items.clone("Ballista_Arrows", "_range");
            Ballista_Arrows.id = "Ballista_Arrows";
            Ballista_Arrows.projectile = "Ballista_Arrow";
            Ballista_Arrows.base_stats[S.targets] = 1;
            Ballista_Arrows.base_stats[S.range] = 30;
            Ballista_Arrows.base_stats[S.attack_speed] = -50f;
            Ballista_Arrows.base_stats[S.damage] = 200f;
            Ballista_Arrows.path_slash_animation = "effects/slashes/slash_punch";
            Ballista_Arrows.action_attack_target = (AttackAction)Delegate.Combine(Ballista_Arrows.action_attack_target, new AttackAction(armorPiercingCrossbowAttack));

            ItemAsset stones = AssetManager.items.clone("stones", "_range");
            stones.id = "stones";
            stones.projectile = "stone";
            stones.base_stats[S.targets] = 1;
            stones.base_stats[S.range] = 30;
            stones.base_stats[S.damage] = 40f;
            stones.base_stats[S.attack_speed] = -80f;
            stones.path_slash_animation = "effects/slashes/slash_punch";
            // stones.action_attack_target = (AttackAction)Delegate.Combine(stones.action_attack_target, new AttackAction(stoenEffect));



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