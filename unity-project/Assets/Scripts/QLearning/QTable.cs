using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AI.Util;

namespace AI.QLearning
{
    public class QTable
    {
        #region Private Members
        private Dictionary<uint, Dictionary<IAction, double>> ValueTable;
        #endregion

        public QTable()
        {
            ValueTable = new Dictionary<uint, Dictionary<IAction, double>>();
        }

        #region Access Methods
        public double ValueFor(uint StateId, IAction a)
        {
            if (!ValueTable.ContainsKey(StateId))
                ValueTable[StateId] = new Dictionary<IAction, double>();

            if (!ValueTable[StateId].ContainsKey(a))
                ValueTable[StateId][a] = 0.0;

            return ValueTable[StateId][a];
        }

        public double ValueFor(uint StateId, List<IAction> AvailableActions)
        {
            if (!ValueTable.ContainsKey(StateId))
            {
                ValueTable[StateId] = new Dictionary<IAction, double>();
                foreach (var a in AvailableActions)
                    ValueTable[StateId][a] = 0.0;

                return 0.0;
            }

            double MaxValue = Double.MinValue;
            foreach (var a in AvailableActions)
            {
                if (!ValueTable[StateId].ContainsKey(a))
                    ValueTable[StateId][a] = 0.0f;

                double value = ValueTable[StateId][a];
                if (MaxValue < value)
                    MaxValue = value;
            }
            return MaxValue;
        }

        public IAction BestAction(uint StateId, List<IAction> AvailableActions)
        {
            if (!ValueTable.ContainsKey(StateId))
            {
                ValueTable[StateId] = new Dictionary<IAction, double>();
                foreach (var a in AvailableActions)
                    ValueTable[StateId][a] = 0.0;

                return RandomList<IAction>.RandomEntry(AvailableActions);
            }

            List<IAction> bestActions = new List<IAction>();

            double maxValue = double.MinValue;
            foreach (var a in AvailableActions)
            {
                if (!ValueTable[StateId].ContainsKey(a))
                    ValueTable[StateId][a] = 0.0f;

                double value = ValueTable[StateId][a];
                if (maxValue < value)
                {
                    maxValue = value;
                    bestActions.Clear();
                    bestActions.Add(a);
                }
                if (maxValue == value)
                    bestActions.Add(a);
            }

            return RandomList<IAction>.RandomEntry(bestActions);
        }
        #endregion

        #region Setter Methods
        public void SetValue(uint StateId, IAction a, double newValue)
        {
            if (!ValueTable.ContainsKey(StateId))
                ValueTable[StateId] = new Dictionary<IAction, double>();

            ValueTable[StateId][a] = newValue;
        }
        #endregion
    }
}
