using ai.behaviours;

namespace K_mod
{
    public class BehtBeInMarket : BehaviourActionActor
    {
        //前往市场
        public override BehResult execute(Actor pActor)
        {
            if (pActor == null)
            {
                return BehResult.Stop;
            }
            if (pActor.attackTarget != null)
            {
                return BehResult.Stop;
            }
            if (pActor.city == null)
            {
                return BehResult.Stop;
            }
            if (pActor.asset.id is "Ballista" or "Catapult")
            {
                return BehResult.Stop;
            }
            if (!pActor.isKing() && !pActor.isCityLeader() && pActor.professionAsset.profession_id != UnitProfession.Warrior)
            {
                pActor.city.data.storage.change(SR.gold, 1);
                return BehResult.Continue;
            }
            return BehResult.Stop;
        }
    }
}
