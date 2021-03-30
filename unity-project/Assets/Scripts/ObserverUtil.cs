using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Observers
public abstract class GameStateObserver : MonoBehaviour
{
    public abstract void gameStateUpdate(GameState state);
}

public abstract class ProgressObserver : MonoBehaviour
{
    public abstract void progressUpdate(int currentIteration);
}
#endregion

#region Subjects
public abstract class GameStateSubject : MonoBehaviour
{
    private List<GameStateObserver> observers = new List<GameStateObserver>();


    public void Attach(GameStateObserver observer) {
        observers.Add(observer);
    }

    public void Detach(GameStateObserver observer) {
        observers.Remove(observer);
    }

    public void Notify(GameState state) {
        foreach (GameStateObserver i in observers) {
            i.gameStateUpdate(state);
        }
    }
}

public abstract class ProgressSubject : MonoBehaviour
{
    private List<ProgressObserver> observers = new List<ProgressObserver>();


    public void Attach(ProgressObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(ProgressObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(int currentIteration)
    {
        foreach (ProgressObserver i in observers)
        {
            i.progressUpdate(currentIteration);
        }
    }
}
#endregion