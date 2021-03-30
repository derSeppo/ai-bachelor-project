using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { building, starting, driving, inMenu };

public class Controller : GameStateSubject
{
    #region Singleton

    private static Controller instance = null;

    public static Controller Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    #region Members
    private GameState state;
    public GameState State { get => state; }
    #endregion

    private void Start()
    {
        instance = this;
    }

    #region Public Methods

    public void StartGame()
    {
        ResetGame();
        AIController.Instance.StartTraining();
    }

    public void StopGame()
    {
        ResetGame();
        state = GameState.building;
        Notify(state);
        AIController.Instance.StopTraining();
    }

    public void StartDriving()
    {
        Car.Instance.ResetCar();
        state = GameState.driving;
        Notify(state);
    }

    public void StartMenu()
    {
        if (state == GameState.inMenu)
        {
            state = GameState.building;
            Notify(state);
        }
        else
        {
            StopGame();
            state = GameState.inMenu;
            Notify(state);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ResetGame()
    {
        Car.Instance.ResetCar();
        state = GameState.starting;
        Notify(state);
    }

    #endregion

}
