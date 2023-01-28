using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float timeOfBreak = 5;
    private PlayCommandPublisher _playCommandPublisher;
    private bool canStopMove = true;
    void Awake()
    {
        _playCommandPublisher = new PlayCommandPublisher();
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
        _playCommandPublisher.StopSubscribers();
        yield return new WaitForSeconds(timeOfBreak);
        _playCommandPublisher.PlaySubscribers();
        canStopMove = true;
    }
}