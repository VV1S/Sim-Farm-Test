using System.Linq;
using NUnit.Framework;
using UnityEngine;
using TMP_Text = TMPro.TMP_Text;

namespace UiElements.Tests
{
    public class DistanceTraveledTextTests
    {
        private DistanceTraveledText distanceTraveledText;
        private TMP_Text displayText;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            var uiGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI"));
            distanceTraveledText = uiGameObject.GetComponentInChildren<DistanceTraveledText>();
            displayText = uiGameObject.GetComponentsInChildren<TMP_Text>().First(x =>x.text !="START");
        }

        [Test]
        public void UpdateText_Updates_Text_With_Given_Distance()
        {
            // Arrange
            // Already arranged

            // Act
            distanceTraveledText.UpdateText(100.5f);

            // Assert
            Assert.AreEqual("DISTANCE TRAVELED: 100,5", displayText.text);
        }

        [Test]
        public void ClearText_Clears_Text()
        {
            // Arrange
            distanceTraveledText.UpdateText(100.5f);

            // Act
            distanceTraveledText.ClearText();

            // Assert
            Assert.AreEqual(string.Empty, displayText.text);
        }
    }
}
