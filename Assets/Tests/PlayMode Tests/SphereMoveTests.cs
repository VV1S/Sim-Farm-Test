using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static Core.Globals;

namespace Sphere.Tests
{
    public class SphereMoveTests
    {
        private GameObject gameObject;
        private SphereMove sphereMove;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            gameObject = new GameObject();
            sphereMove = gameObject.AddComponent<SphereMove>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(gameObject);
        }

        [UnityTest]
        public IEnumerator TestSphereMovement()
        {
            // Arrange
            // Already arranged

            // Act
            sphereMove.Play();

            // Assert
            var initialPosition = sphereMove.transform.position;
            yield return new WaitForSeconds(1f);
            EndPositionShouldntBeEqualTo(Vector3.zero, sphereMove.transform.position);
            EndPositionShouldntBeEqualTo(initialPosition, sphereMove.transform.position);
        }

        [UnityTest]
        public IEnumerator TestSphereStops()
        {
            // Arrange
            sphereMove.Play();
            var initialPosition = sphereMove.transform.position;
            yield return new WaitForSeconds(1f);

            // Act
            sphereMove.Stop();

            // Assert
            var endPosition = sphereMove.transform.position;
            yield return new WaitForSeconds(1f);
            var endPositionAfterOneSecond = sphereMove.transform.position;
            EndPositionShouldntBeEqualTo(Vector3.zero, endPosition);
            EndPositionShouldntBeEqualTo(initialPosition, endPosition);
            EndPositionIsEqualTo(endPositionAfterOneSecond, endPosition);
        }

        [UnityTest]
        public IEnumerator TestSphereShrink()
        {
            // Arrange
            var sphereScales = new List<Vector3>();
            sphereMove.Play();
            yield return new WaitForSeconds(1f);
            sphereMove.transform.localScale = Vector3.one;
            var initialScale = sphereMove.transform.localScale;
            while (sphereMove.transform.localScale == initialScale)
            {
                yield return null;
            }

            // Act
            //Sphere starts shrinking automatically

            // Assert
            while (sphereMove != null && sphereMove.transform.localScale.x > 0)
            {
                sphereScales.Add(sphereMove.transform.localScale);
                yield return null;
            }
            EveryNextScaleShoudBeSmaller(sphereScales);
        }

        private void EveryNextScaleShoudBeSmaller(List<Vector3> scales)
        {
            var nextScaleIsSmaller = true;
            for (var i = 0; i < scales.Count - 1; i++)
            {
                nextScaleIsSmaller = (scales[i].x > scales[i + 1].x && 
                                      scales[i].y > scales[i + 1].y &&
                                      scales[i].z > scales[i + 1].z);
            }
            if(!nextScaleIsSmaller)
                Assert.Fail("Not every next frame scale is smaller than the previous one!");
        }

        private void EndPositionIsEqualTo(Vector3 endPositionAfterOneSecond, Vector3 endPosition)
        {
            var postionsArentEqual = CheckIfVectorsAreEqual(endPositionAfterOneSecond, endPosition);
            if (!postionsArentEqual)
                Assert.Fail("End position isn't equal to:" + endPositionAfterOneSecond);
        }

        private void EndPositionShouldntBeEqualTo(Vector3 unwantedPosition, Vector3 endPosition)
        {
            var postionsArentEqual = CheckIfVectorsAreEqual(unwantedPosition, endPosition);
            if (postionsArentEqual)
                Assert.Fail("End position is equal to:" + unwantedPosition);
        }
    }
}