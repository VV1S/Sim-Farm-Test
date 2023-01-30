using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sphere.Tests
{
    [TestFixture]
    public class ColorChangeTests
    {
        private GameObject sphereGameObject;
        private ColorChange colorChange;
        private Material myMaterial;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            sphereGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Sphere"));
            colorChange = sphereGameObject.GetComponent<ColorChange>();
            myMaterial = colorChange.myMaterial;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(sphereGameObject);
        }

        [UnityTest]
        public IEnumerator When_Play_Is_Called_Then_Color_Changes_On_Change_Of_Position()
        {
            // Arrange
            yield return null;
            var previousColor = myMaterial.color;

            // Act
            colorChange.Play();

            // Assert
            sphereGameObject.transform.position = Vector3.one;
            yield return null;
            PrivateColorsArentTheSame(previousColor, myMaterial.color);
        }

        [UnityTest]
        public IEnumerator When_Stop_Is_Called_Color_Doesnt_Change_On_Change_Of_Position()
        {
            // Arrange
            yield return null;
            colorChange.Play();

            // Act
            colorChange.Stop();

            // Assert
            var previousColor = myMaterial.color;
            sphereGameObject.transform.position = Vector3.one;
                        yield return null;
            PrivateColorsAreTheSame(previousColor, myMaterial.color);
        }

        private void PrivateColorsAreTheSame(Color previousColor, Color color)
        {
            if (color != previousColor)
                Assert.Fail("Colors aren't the same! They first one is equal to " +
                            previousColor + ", while the second one is equal to " + color);
        }

        private void PrivateColorsArentTheSame(Color previousColor, Color color)
        {
            if (color == previousColor)
                Assert.Fail("Colors are the same! They are bot equal to " + color);
        }
    }
}
