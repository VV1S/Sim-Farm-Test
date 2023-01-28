using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour, IMoveable
{
    [SerializeField] private DistanceTraveledText distanceText;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private float maxSpeed = 5f;
    [Range(0,float.MaxValue)][SerializeField] private float radius = 4f;
    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private float timeOfAcclerationIncrease = 5f;
    [SerializeField] private float shrinkingModifier = 2;
    private float speedMultiplier = 10;
    private float angle = 0;
    private float accelerationTime = 0;
    private Action updateAction;
    public float distance { get; private set; } = 0;

    void Start()
    {
        this.transform.position = new Vector3(radius, this.transform.position.y, this.transform.position.z);
        updateAction = NullAction;
        var moveCommandPublisher = new MoveCommandPublisher();
        moveCommandPublisher.Subscribe(this);
        distanceText.ClearText();
    }

    void Update()
    {
        updateAction.Invoke();
    }

    public void Move()
    {
        updateAction = MoveAction;
        distanceText.ClearText();
    }

    public void Stop()
    {
        updateAction = NullAction;
        distanceText.UpdateText(distance);
        accelerationTime = 0;

    }

    private void NullAction()
    {
        //Intentionally left blank
    }

    private void MoveAction()
    {
        angle += (SetSpeed() / (radius * Mathf.PI * 2)) * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        var newPosition = new Vector3(x, transform.position.y, z);
        CountDistance(transform.position, newPosition);
        transform.position = newPosition;
        radius -= Time.deltaTime;
        CheckIfSphereAchievedCenter();
    }

    private void ShrinkAction()
    {
        var newScale = transform.localScale.x - Time.deltaTime * shrinkingModifier;
        transform.localScale = new Vector3(newScale, newScale, newScale);
        CheckIfNewScaleAchievedZero(newScale);
    }

    private void CheckIfNewScaleAchievedZero(float newScale)
    {
        if (newScale <= 0)
        {
            var explosion = Instantiate(explosionParticles);
            explosion.Play();
            Destroy(this.gameObject,0.1f);
            updateAction = NullAction;
        }
    }

    private void CountDistance(Vector3 previousPosition, Vector3 newPosition)
    {
        distance += Vector3.Distance(previousPosition, newPosition);
    }

    private void CheckIfSphereAchievedCenter()
    {
        if (Mathf.Abs(radius) < Time.deltaTime)
        {
            transform.position = new Vector3(0, transform.position.y, 0);
            updateAction = ShrinkAction;
        }
    }

    private float SetSpeed()
    {
        accelerationTime += Time.deltaTime;
        var a = accelerationCurve.Evaluate(accelerationTime / timeOfAcclerationIncrease);
        return maxSpeed * a * speedMultiplier;
    }
}

public interface IMoveable
{
    void Move();
    void Stop();
}
