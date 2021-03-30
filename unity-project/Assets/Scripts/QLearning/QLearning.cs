using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AI.Util;

namespace AI.QLearning
{
    public class QLearningAI
    {
        #region Public Properties
        public IGameState GameState { get; set; }
        public double ExplorationRate { get; set; }
        public double DiscountRate { get; set; }
        public double LearningRate { get; set; }
        #endregion

        #region Private Members

        private QTable LearnedTable;
        private System.Random Rand = new System.Random();

        #endregion

        public QLearningAI(float dr)
        {
            GameState = null;
            ExplorationRate = 0.8f;
            DiscountRate = dr;
            LearningRate = 0.3f;
            LearnedTable = new QTable();
        }

        #region Public Functions
        public void Learn(int numIterations)
        {
            if (GameState == null) throw new InvalidOperationException("GameState has not been initialized!");
            for (int i = 0; i < numIterations; i++)
            {
                if (GameState.getPossibleActions().Count > 0)
                {
                    Step();
                }
                else
                {
                    Controller.Instance.ResetGame();
                }
            }
        }
        #endregion

        #region Private Functions
        protected void Step()
        {
            uint CurrentState = GameState.getID();
            IAction a = SelectAction(GameState);
            double Reward = GameState.ExecuteAction(a);
            uint FollowState = GameState.getID();

            double OldQuality = LearnedTable.ValueFor(CurrentState, a);
            double FollowStateQuality = LearnedTable.ValueFor(FollowState, GameState.getPossibleActions());
            double NewQuality =
                (1.0 - LearningRate) * OldQuality +
                (LearningRate) * (Reward + DiscountRate * FollowStateQuality);
            if (NewQuality != OldQuality)
            {
                LearnedTable.SetValue(CurrentState, a, NewQuality);
            }
        }

        private IAction SelectAction(IGameState state)
        {
            if (Rand.NextDouble() < ExplorationRate)
            {
                return RandomList<IAction>.RandomEntry(state.getPossibleActions());
            }
            else
            {
                return LearnedTable.BestAction(state.getID(), state.getPossibleActions());
            }
        }

        #endregion
    }
}

