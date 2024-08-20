using System.Linq;
using System.Collections.Generic;
using K_mod.Utils;
namespace K_mod
{
    class k_update
    {
        public static void update_actions(bool paused)
        {
            List<k_action> destroyAction = new();
            for (int i = 0; i < Main.k_actions.Count; i++)
            {
                k_action action = Main.k_actions[i];
                if (action.destroy)
                {
                    destroyAction.Add(action);
                }
                else { action.update(paused); }
            }
            foreach (k_action action in destroyAction)
            {
                Actor a = action.a;
                if (Main.Actor_Action.ContainsKey(a))
                {
                    if (Main.Actor_Action[a].Any() && a.Any())
                    {
                        Main.Actor_Action[a].Remove(action);
                    }
                    else { Main.Actor_Action.Remove(a); }
                }
                Main.k_actions.Remove(action);
                if (a.Any()) { a.setStatsDirty(); }
            }
        }
    }
}