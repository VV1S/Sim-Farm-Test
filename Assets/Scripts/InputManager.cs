using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float timeOfBreak = 5;
    private MoveCommandPublisher moveCommandPublisher;
    private bool canStopMove = true;
    void Awake()
    {
        moveCommandPublisher = new MoveCommandPublisher();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopForFiveSeconds(timeOfBreak);
        }
    }

    private void StopForFiveSeconds(float timeOfBreak)
    {
        if (canStopMove)
        {
            StartCoroutine(StopMovementCoroutine(timeOfBreak));
        }
    }

    IEnumerator StopMovementCoroutine(float timeOfBreak)
    {
        canStopMove = false;
        moveCommandPublisher.StopSubscribers();
        yield return new WaitForSeconds(timeOfBreak);
        moveCommandPublisher.MoveSubscribers();
        canStopMove = true;
    }
}