using System;
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
        public float distance { get; private set; } = 0;
        private bool endPositionAchieved = false;
        private bool started = false;
        private float speedMultiplier = 10;
        private float angle = 0;
        private float accelerationTime = 0;
        private Action updateAction;

        void Start()
        {
            this.transform.position = new Vector3(radius + endPoint.x, this.transform.position.y, this.transform.position.z + endPoint.y);
            updateAction = NullAction;
            var moveCommandPublisher = new Controls.PlayCommandPublisher();
            moveCommandPublisher.Subscribe(this);
            distanceText.ClearText();
        }

        void Update()
        {
            updateAction.Invoke();
        }

        public void Play()
        {
            if (endPositionAchieved || started) return;
            updateAction = Move;
            distanceText.ClearText();
            UpdateStartedCondition();
        }

        public void Stop()
        {
            if (!started) return;
            updateAction = NullAction;
            distanceText.UpdateText(distance);
            ResetAccelerationTime();
            UpdateStartedCondition();
        }

        private void UpdateStartedCondition()
        {
            started = !started;
        }

        private void Move()
        {
            var speedFraction = GetSpeedFraction();
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
                endPositionAchieved = true;
                updateAction = Shrink;
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
    }
}
