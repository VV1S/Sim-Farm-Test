using System;
using System.Collections;
using UnityEngine;
using static Core.Globals;

namespace Core
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private float timeOfBreak = 5;
        [SerializeField] private bool canStopMove = true;

        private Controls.PlayCommandPublisher _playCommandPublisher;
        private Action updateAction;
        void Awake()
        {
            _playCommandPublisher = new Controls.PlayCommandPublisher();
            updateAction = NullAction;
        }
        private void Update()
        {
            updateAction.Invoke();
        }

        public void AllowGettingKeys()
        {
            updateAction = GetInput;
        }

        private void GetInput()
        {
            if (canStopMove && Input.GetKeyDown(KeyCode.Space))
            {
                StopForFiveSeconds(timeOfBreak);
            }
        }

        private void StopForFiveSeconds(float timeOfBreak)
        {
            StartCoroutine(StopMovementCoroutine(timeOfBreak));
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
}