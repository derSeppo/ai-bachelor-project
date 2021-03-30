using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : GameStateObserver
{
    #region Public Members
    public GameObject startButton;
    public GameObject stopButton;
    public GameObject buildMenu;
    public GameObject optionsMenu;
    public GameObject progressBar;
    #endregion

    //Start coroutine, because it needs access to the instance of Controller
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        Controller.Instance.Attach(this); 
    }

    //Updates the UI on game state changes
    public override void gameStateUpdate(GameState state)
    {
        switch (state){
            case GameState.building:
                startButton.SetActive(true);
                stopButton.SetActive(false);
                buildMenu.SetActive(true);
                optionsMenu.SetActive(false);
                progressBar.SetActive(false);
                break;

            case GameState.driving:
                startButton.SetActive(false);
                stopButton.SetActive(true);
                buildMenu.SetActive(false);
                optionsMenu.SetActive(false);
                progressBar.SetActive(true);
                break;
            case GameState.inMenu:
                startButton.SetActive(false);
                stopButton.SetActive(false);
                buildMenu.SetActive(false);
                optionsMenu.SetActive(true);
                progressBar.SetActive(false);
                break;
        }
    }
}
