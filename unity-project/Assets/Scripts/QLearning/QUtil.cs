using System.Collections.Generic;

namespace AI {
    public interface IAction
    {
        string getName();
    }

    public interface IGameState
    {
        uint getID();
        List<IAction> getPossibleActions();
        double ExecuteAction(IAction a);

    }
}

namespace AI.Util {
    using UnityEngine;
    static class RandomList<T>
    {
        public static T RandomEntry(List<T> list)
        {
                int s = Rand.Next() % list.Count;
                return list[s];
        }
        static System.Random Rand = new System.Random();
    }
}
