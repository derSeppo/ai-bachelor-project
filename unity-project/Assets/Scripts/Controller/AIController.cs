using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.QLearning;

using System.Timers;

public class AIController : ProgressSubject
{
    #region Singleton

    private static AIController instance = null;

    public static AIController Instance
    {
        get
        {
            return instance;
        }
    }

    #endregion

    #region Public Properties
    public int CurrentIteration { get => currentIteration; }
    public int MaxNumIterations { get => maxNumIterations; }
    #endregion

    #region Private Members
    private QLearningAI learningAI;
    private int maxNumIterations = 100000;
    private int currentIteration = 0;
    private float learningDelay = 0.01f;
    private float discountRate = 0.9f;

    private IEnumerator coroutine;
    #endregion

    private void Start()
    {
        instance = this;
    }

    #region Public Methods
    public void StartTraining()
    {
        InitializeAI();

        currentIteration = 0;

        coroutine = Learn(learningDelay);
        StartCoroutine(coroutine);
    }

    public void StopTraining()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
    }

    //Setter for the options menu
    public void SetMaxNumIterations(string value)
    {
        if (int.Parse(value) > 400)
        {
            maxNumIterations = int.Parse(value);
        }
    }

    public void SetDelay(string value)
    {
        if (int.Parse(value) > 0)
        {
            learningDelay = int.Parse(value)/1000;
        }
    }

    public void SetDiscountRate(float value)
    {
        discountRate = value;
    }
    #endregion

    private void InitializeAI()
    {
        learningAI = new QLearningAI(discountRate)
        { GameState = new GameAI() };
    }

    //Learning coroutine
    IEnumerator Learn(float delay)
    {
        while (currentIteration < maxNumIterations)
        {
            int LearnPhase = maxNumIterations / 4;
            int LearnSteps = LearnPhase / 100;

            learningAI.Learn(LearnSteps);
            currentIteration += LearnSteps;
            Notify(currentIteration);

            if (currentIteration < LearnPhase)
            {
                learningAI.LearningRate = 0.5;
                learningAI.ExplorationRate = 1.0;
            }
            else if (currentIteration < 2 * LearnPhase)
            {
                learningAI.LearningRate = 0.4;
                learningAI.ExplorationRate = 0.7;
            }
            else if (currentIteration < 3 * LearnPhase)
            {
                learningAI.LearningRate = 0.3;
                learningAI.ExplorationRate = 0.5;
            }
            else if (currentIteration < 4 * LearnPhase)
            {
                learningAI.LearningRate = 0.2;
                learningAI.ExplorationRate = 0.3;
            }
            yield return new WaitForSeconds(delay);
        }
        Controller.Instance.ResetGame();
        while (true)
        {
            learningAI.Learn(1);
            learningAI.ExplorationRate = 0.0;
            learningAI.LearningRate = 0.0;
            currentIteration++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}