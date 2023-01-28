using System;
using TMPro;
using UnityEngine;

namespace UiElements
{
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
    }
}

