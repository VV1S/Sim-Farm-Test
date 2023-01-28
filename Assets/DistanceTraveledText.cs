using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceTraveledText : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;

    public void UpdateText(float distance)
    {
        displayText.text = "DISTANCE TRAVELED: " + String.Format("{0:0}", distance);
    }

    public void ClearText()
    {
        displayText.text = string.Empty;
    }

    private void Update()
    {
        //var health = fighter.GetTarget();
        //if (health == null)
        //{
        //    healthValue.text = "No enemy targeted";
        //    return;
        //}
        //healthValue.text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
    }
}
