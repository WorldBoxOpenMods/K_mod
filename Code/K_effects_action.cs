using System;
using UnityEngine;
using System.Collections.Generic;
using ReflectionUtility;
using K_mod.Utils;

namespace K_mod
{
    class K_effects_action
    {

        public static bool charge_1(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor actor = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;

            if (actor == null || !actor.isAlive())
            {
                return false;
            }

            if (actor.tileTarget != null && actor.currentTile != null)
            {
                if (Toolbox.DistTile(actor.tileTarget, actor.currentTile) < 2f)
                {
                    actor.removeStatusEffect("charge");
                    return false;
                }

                // 根据距离影响周围敌人
                List<BaseSimObject> nearbyObjects = Main.getObjectsInChunks(pTile, 2, MapObjectType.Actor);
                foreach (BaseSimObject obj in nearbyObjects)
                {
                    if (obj is Actor nearbyActor)
                    {
                        if (ShouldApplyImpact(actor, nearbyActor))
                        {
                            ApplyImpact(actor, nearbyActor);
                        }
                    }
                }
            }
            else
            {
                actor.removeStatusEffect("charge");
            }

            return true;
        }

        public static bool ShouldApplyImpact(Actor source, Actor target)
        {
            return target.data != null && target.data.alive&&target.kingdom!=source.kingdom
                && source.kingdom.isEnemy(target.kingdom)
                && !target.asset.flying && !target.asset.hovering && !target.isFlying();
        }

        private static void ApplyImpact(Actor source, Actor target)
        {
            int damage = CalculateImpactDamage(source.stats[S.damage], source.stats[S.armor]);
            target.getHit(damage, true, AttackType.Weapon, source, true);

            float angle = Toolbox.getAngle(target.transform.position.x, target.transform.position.y, source.transform.position.x, source.transform.position.y);
            if(source.race.id!="dwarf")
            target.addForce(-Mathf.Cos(angle) * 0.45f, -Mathf.Sin(angle) * 0.45f, 0.5f);
            else target.addForce(-Mathf.Cos(angle) * 0.6f, -Mathf.Sin(angle) * 0.6f, 0.5f);
        }

        private static int CalculateImpactDamage(float damage, float armor)
        {
            // 根据具体需求计算伤害，可以把魔法数字提取为常量或者参数化
            return (int)(damage * 0.4 + (int)(armor * 0.2));
        }


        public static bool Cavalry(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (a != null && a.isAlive() && a.attackTarget != null && a.attackTarget.isAlive())
            {
                if (a.race.id != "orc")
                { 
                    HumanCavalry(pTarget, pTile); 
                }
                else
                {
                    CruelCavalry(pTarget, pTile);
                }
            }
            else
            {
                a.removeStatusEffect("charge");
                return false;
            }
            return true;
        }


        public static bool HumanCavalry(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (a != null && a.isAlive() && a.attackTarget != null && a.attackTarget.isAlive() && a.s_attackType == WeaponType.Melee)
            {
                float TDJL = Toolbox.DistTile(a.attackTarget.currentTile, a.currentTile);
                if (TDJL is > 4f)
                {
                    a.goTo(a.attackTarget.currentTile, true, true);
                    a.tileTarget = a.attackTarget.currentTile;
                    a.addStatusEffect("charge", 3f);
                    // a.attackTarget.getHit((int)(a.stats[S.damage] * 0.6 + (int)(a.stats[S.armor] * 0.3)), true, AttackType.Block, a, true, false);
                    if (a.animationContainer != null)
                    {
                        a.animationContainer.idle.id = 1;
                        a.animationContainer.walking.id = 1;
                        a.animationContainer.swimming.id = 1;
                    }
                }
            }
            else
            {
                a.removeStatusEffect("charge");
            }
            return true;
        }


        public static bool ArcherCavalry(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (a != null && a.isAlive() && a.attackTarget != null && a.attackTarget.isAlive() && a.s_attackType == WeaponType.Melee)
            {
                float TDJL = Toolbox.DistTile(a.attackTarget.currentTile, a.currentTile);
                if (TDJL is > 2f and < 10f)
                {
                    a.goTo(a.attackTarget.currentTile, true, true);
                    a.tileTarget = a.attackTarget.currentTile;
                    a.addStatusEffect("charge");
                    a.attackTarget.getHit((int)(a.stats[S.damage] * 0.6 + (int)(a.stats[S.armor] * 0.3)), true, AttackType.Block, a, true, false);
                    if (a.animationContainer != null)
                    {
                        a.animationContainer.idle.id = 1;
                        a.animationContainer.walking.id = 1;
                        a.animationContainer.swimming.id = 1;
                    }
                }
            }
            else
            {
                a.removeStatusEffect("charge");
            }
            return true;
        }


        public static bool CruelCavalry(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (a != null && a.isAlive() && a.attackTarget != null && a.attackTarget.isAlive() && a.s_attackType == WeaponType.Melee)
            {
                float TDJL = Toolbox.DistTile(a.attackTarget.currentTile, a.currentTile);
                if (TDJL is > 2f and < 10f)
                {
                    a.goTo(a.attackTarget.currentTile, true, true);
                    a.tileTarget = a.attackTarget.currentTile;
                    a.addStatusEffect("charge");
                    if(!a.hasStatus("effect_roar"))
                    {
                        a.addStatusEffect("effect_roar",30f);
                    }
                    a.attackTarget.getHit((int)(a.stats[S.damage] * 0.6 + (int)(a.stats[S.armor] * 0.3)), true, AttackType.Block, a, true, false);
                    if (a.animationContainer != null)
                    {
                        a.animationContainer.idle.id = 1;
                        a.animationContainer.walking.id = 1;
                        a.animationContainer.swimming.id = 1;
                    }
                }
            }
            else
            {
                a.removeStatusEffect("charge");
            }
            return true;
        }


        public static bool DwarfCavalry(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (a != null && a.isAlive() && a.attackTarget != null && a.attackTarget.isAlive() && a.s_attackType == WeaponType.Melee)
            {
                float TDJL = Toolbox.DistTile(a.attackTarget.currentTile, a.currentTile);
                if (TDJL is > 2f and < 10f)
                {
                    a.goTo(a.attackTarget.currentTile, true, true);
                    a.tileTarget = a.attackTarget.currentTile;
                    a.addStatusEffect("charge");
                    a.attackTarget.getHit((int)(a.stats[S.damage] * 0.3 + (int)(a.stats[S.armor] * 0.6)), true, AttackType.Block, a, true, false);
                    if (a.animationContainer != null)
                    {
                        a.animationContainer.idle.id = 1;
                        a.animationContainer.walking.id = 1;
                        a.animationContainer.swimming.id = 1;
                    }
                }
            }
            else
            {
                a.removeStatusEffect("charge");
            }
            return true;
        }


        public static bool Elf_Cavalry(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (a != null && a.isAlive() && a.attackTarget != null && a.attackTarget.isAlive() && a.s_attackType == WeaponType.Melee)
            {
                float TDJL = Toolbox.DistTile(a.attackTarget.currentTile, a.currentTile);
                if (TDJL is > 2f and < 10f)
                {
                    a.goTo(a.attackTarget.currentTile, true, true);
                    a.tileTarget = a.attackTarget.currentTile;
                    a.addStatusEffect("charge");
                    a.attackTarget.getHit((int)(a.stats[S.damage] * 0.6 + (int)(a.stats[S.armor] * 0.3)), true, AttackType.Block, a, true, false);
                    if (a.animationContainer != null)
                    {
                        a.animationContainer.idle.id = 1;
                        a.animationContainer.walking.id = 1;
                        a.animationContainer.swimming.id = 1;
                    }
                }
            }
            else
            {
                a.removeStatusEffect("charge");
            }
            return true;
        }

        public static bool Pig_Cavalry(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (a != null && a.isAlive() && a.attackTarget != null && a.attackTarget.isAlive() && a.s_attackType == WeaponType.Melee)
            {
                float TDJL = Toolbox.DistTile(a.attackTarget.currentTile, a.currentTile);
                if (TDJL is > 2f and < 10f)
                {
                    a.goTo(a.attackTarget.currentTile, true, true);
                    a.tileTarget = a.attackTarget.currentTile;
                    a.addStatusEffect("charge");
                    a.attackTarget.getHit((int)(a.stats[S.damage] * 0.6 + (int)(a.stats[S.armor] * 0.3)), true, AttackType.Block, a, true, false);
                    if (a.animationContainer != null)
                    {
                        a.animationContainer.idle.id = 1;
                        a.animationContainer.walking.id = 1;
                        a.animationContainer.swimming.id = 1;
                    }
                }
            }
            else
            {
                a.removeStatusEffect("charge");
            }
            return true;
        }

    }
}