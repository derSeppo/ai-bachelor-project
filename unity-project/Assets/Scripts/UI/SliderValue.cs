using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public void ShowValue(float Value)
    {
        GetComponent<Text>().text = Value.ToString();
    }
}
