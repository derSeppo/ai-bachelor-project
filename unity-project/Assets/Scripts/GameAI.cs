using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AI;

public class GameAI : IGameState
{
    #region Private Members
    private double goalReward;
    private double roadTypeReward;
    private double roadTypePenalty;

    private MovementAction[] MovementActions;
    private StartAction[] StartActions;
    #endregion
    
    public GameAI()
    {
        InitActions();

        goalReward = 1.0;

        roadTypeReward = 0.2;
        roadTypePenalty = 0.2;
    }

    #region Public Methods
    public uint getID() {

        uint ID = 0;
        ID |= (uint)(((Car.Instance.CurrNode.X + 32)/4) & 0x1F);                               //Current X Position 5Bit
        ID |= (uint)(((Car.Instance.CurrNode.Z + 22)/4) & 0xF) << 5;                           //Current Z Position 4Bit
        ID |= (uint)((Controller.Instance.State == GameState.starting ? 1 : 0) & 0x1) << 9;    //Current Game State 1Bit
        ID |= (uint)(Car.Instance.startNode.StartingPosition & 0xF) << 10;                     //Current Starting Position 4Bit
        return ID;
    }

    public List<IAction> getPossibleActions()
    {
        List<IAction> actions = new List<IAction>();
        if (Controller.Instance.State == GameState.driving)
        {
            BitArray movementOptions = new BitArray(Car.Instance.getMovementOptions());
            for (int i = 0; i < 4; i++)
            {
                if (movementOptions[i])
                    actions.Add(MovementActions[i]);
            }
        }
        else if(Controller.Instance.State == GameState.starting)
        {
            foreach (IAction action in StartActions)
            {
                actions.Add(action);
            }
        }
        

        return actions;
    }

    public double ExecuteAction(IAction a)
    {
        if (Controller.Instance.State == GameState.driving)
        {
            MovementAction action = (MovementAction)a;
            if (null != action)
            {
                Car.Instance.MoveCar((int)action.Movement);

                double result = 0.0;
                if (Car.Instance.CurrNode == Car.Instance.EndNode)
                {
                    result += goalReward;
                    Controller.Instance.ResetGame();
                }
                if (Car.Instance.CurrNode.Road.RoadType == RoadType.reward)
                {
                    result += roadTypeReward;
                }
                else if (Car.Instance.CurrNode.Road.RoadType == RoadType.penalty)
                {
                    result -= roadTypePenalty;
                }
                return result;
            }
        }
        else if (Controller.Instance.State == GameState.starting)
        {
            StartAction action = (StartAction)a;
            if (null != action)
            {
                Car.Instance.startNode.moveStart(action.StartingPlace);
                Controller.Instance.StartDriving();
                return 0.0f;
            }
        }
            throw new ArgumentException("Not a valid action!");
    }
    #endregion

    #region Private Methods
    private void InitActions()
    {
        MovementActions = new MovementAction[(int)Motion.NumActions];
        MovementActions[(int)Motion.Down] = new MovementAction { Name = "Move Down", Movement = Motion.Down };
        MovementActions[(int)Motion.Right] = new MovementAction { Name = "Move Right", Movement = Motion.Right };
        MovementActions[(int)Motion.Up] = new MovementAction { Name = "Move Up", Movement = Motion.Up };
        MovementActions[(int)Motion.Left] = new MovementAction { Name = "Move Left", Movement = Motion.Left };

        StartActions = new StartAction[11];
        for(int i = 0; i < StartActions.Length; i++)
        {
            StartActions[i] = new StartAction { Name = "Starting Position " + i, StartingPlace = i };
        }
    }
    #endregion

    #region Helper Classes
    private class MovementAction : IAction
    {
        public MovementAction() { }
        public Motion Movement;
        public string Name;
        public string getName()
        {
            return Name;
        }
    }

    private class StartAction : IAction
    {
        public StartAction() { }
        public int StartingPlace;
        public string Name;
        public string getName()
        {
            return Name;
        }
    }
    #endregion

    enum Motion : int
    {
        Down = 0,
        Right,
        Up,
        Left,
        NumActions
    }
}
