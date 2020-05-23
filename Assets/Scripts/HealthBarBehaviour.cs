using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{

    // this is the image we increase/decrease to show how much health is remaining
    public Image filledPart;
    
    // this is public as the health system needs to talk to it
    // void as we don't need anything returned
    public void ShowHealthFraction(float fraction)
    {
        // scales the filled part to the fraction provided
        filledPart.rectTransform.localScale = new Vector3(fraction, 1, 1);
    }

}
