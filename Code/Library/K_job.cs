using ai.behaviours;

namespace K_mod
{

    class K_Job
    {
        public static void init()
        {




            ActorJob Strike_retreat = new()
            {
                id = "Strike retreat"//设计一个受到攻击随机移动的功能
            };
            AssetManager.job_actor.add(Strike_retreat);
            Strike_retreat.addTask("long_move");
            Strike_retreat.addTask("long_move");
            Strike_retreat.addTask("end_job");

            BehaviourTaskActor long_move = new()
            {
                id = "long_move"
            };
            AssetManager.tasks_actor.add(long_move);
            long_move.addBeh(new BehLongRandomMove());
            long_move.addBeh(new BehGoToTileTarget());

            BehaviourTaskActor Market = new()
            {
                id = "Market"
            };
            AssetManager.tasks_actor.add(Market);
            Market.addBeh(new BehCityFindBuilding("Market"));
            Market.addBeh(new BehGoToBuildingTarget(false));
            Market.addBeh(new BehStayInBuildingTarget(10f, 15f));
            Market.addBeh(new BehtBeInMarket());
            Market.addBeh(new BehExitBuilding());
        }
    }
}