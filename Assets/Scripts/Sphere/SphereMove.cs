using System;
using Controls;
using Unity.VisualScripting;
using UnityEngine;
using static Core.Globals;

namespace Sphere
{
    public class SphereMove : MonoBehaviour, IPlayable
    {
        [SerializeField] private UiElements.DistanceTraveledText distanceText;
        [SerializeField] private ParticleSystem explosionParticles;
        [SerializeField] private float maxSpeed = 5f;
        [Range(0, float.MaxValue)][SerializeField] private float radius = 4f;
        [SerializeField] Vector2 endPoint = Vector2.zero;
        [SerializeField] private AnimationCurve accelerationCurve;
        [SerializeField] private float timeOfAcclerationIncrease = 5f;
        [SerializeField] private float shrinkingModifier = 2;

        private float distance = 0;
        private float speedMultiplier = 10;
        private float angle = 0;
        private float accelerationTime = 0;
        private Action updateAction;
        private Func<float> maxSpeedFraction;
        private PlayCommandPublisher playCommandPublisher;

        void Start()
        {
            this.transform.position = new Vector3(radius + endPoint.x, this.transform.position.y, this.transform.position.z + endPoint.y);
            updateAction = NullAction;
            playCommandPublisher = new Controls.PlayCommandPublisher();
            playCommandPublisher.Subscribe(this.gameObject.GetInstanceID(), this);
            ClearDistanceText();
            DefineMaxSpeedFraction();
        }

        void Update()
        {
            updateAction.Invoke();
        }

        public void Play()
        {
            updateAction = Move;
            ClearDistanceText();
        }

        public void Stop()
        {
            updateAction = NullAction;
            UpdateDistanceText();
            ResetAccelerationTime();
        }

        private void Move()
        {
            var speedFraction = maxSpeedFraction.Invoke();
            var newPosition = GetNewPosition(speedFraction);
            CountDistance(transform.position, newPosition);
            transform.position = newPosition;
            CalculateNewRadiusValue(speedFraction);
            CheckIfSphereAchievedCenter();
        }

        private void Shrink()
        {
            var newScale = transform.localScale.x - Time.deltaTime * shrinkingModifier;
            transform.localScale = new Vector3(newScale, newScale, newScale);
            CheckIfNewScaleAchievedZero(newScale);
        }

        private void ResetAccelerationTime()
        {
            accelerationTime = 0;
        }

        private void CalculateNewRadiusValue(float speedFraction)
        {
            radius -= Time.deltaTime * speedFraction;
        }

        private Vector3 GetNewPosition(float speedFraction)
        {
            angle += GetAngle(speedFraction);
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            var newPosition = new Vector3(endPoint.x + x, transform.position.y, endPoint.y + z);
            return newPosition;
        }

        private float GetAngle(float speedFraction)
        {
            return (SetSpeed(speedFraction) / (radius * Mathf.PI * 2)) * Time.deltaTime;
        }

        private void CheckIfNewScaleAchievedZero(float newScale)
        {
            if (newScale <= 0)
            {
                updateAction = NullAction;
                InstanceExplosion();
                Destroy(this.gameObject);
            }
        }

        private void InstanceExplosion()
        {
            if (explosionParticles == null) return;
            var explosion = Instantiate(explosionParticles);
            explosion.transform.position = this.transform.position;
            explosion.Play();
            Destroy(explosion.gameObject, 10);
        }

        private void CountDistance(Vector3 previousPosition, Vector3 newPosition)
        {
            distance += Vector3.Distance(previousPosition, newPosition);
        }

        private void CheckIfSphereAchievedCenter()
        {
            if (Mathf.Abs(radius) < Time.deltaTime)
            {
                transform.position = new Vector3(endPoint.x, transform.position.y, endPoint.y);
                updateAction = Shrink;
                playCommandPublisher.Unsubscribe(this.gameObject.GetInstanceID());
            }
        }

        private float SetSpeed(float speedParticle)
        {
            return maxSpeed * speedParticle * speedMultiplier;
        }

        private float GetSpeedFraction()
        {
            accelerationTime += Time.deltaTime;
            var speedFraction = accelerationCurve.Evaluate(accelerationTime / timeOfAcclerationIncrease);
            return speedFraction;
        }

        private void UpdateDistanceText()
        {
            if (distanceText == null) return;
            distanceText.UpdateText(distance);
        }

        private void ClearDistanceText()
        {
            if (distanceText == null) return;
            distanceText.ClearText();
        }

        private void DefineMaxSpeedFraction()
        {
            if (accelerationCurve == null)
                maxSpeedFraction = ReturnOne;
            else
                maxSpeedFraction = GetSpeedFraction;
        }

        private float ReturnOne()
        {
            return 1;
        }
    }
}
