using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : ProgressObserver
{
    //Start coroutine, because it needs access to the instance of AIController
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        AIController.Instance.Attach(this);
    }

    //Updates value and text of the progress bar
    public override void progressUpdate(int currentIteration)
    {
        GetComponent<Slider>().value = (float)currentIteration / AIController.Instance.MaxNumIterations;
        GetComponentInChildren<Text>().text = currentIteration.ToString() + " / " + AIController.Instance.MaxNumIterations.ToString();
    }
}
