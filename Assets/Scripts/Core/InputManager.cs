using System;
using System.Collections;
using UnityEngine;
using static Core.Globals;

namespace Core
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private float timeOfBreak = 5;

        private Controls.PlayCommandPublisher _playCommandPublisher;
        private Action updateAction;
        private Action activateInputManagerAction;
        void Awake()
        {
            _playCommandPublisher = new Controls.PlayCommandPublisher();
            updateAction = NullAction;
            activateInputManagerAction = ActivateInputManager;
        }
        private void Update()
        {
            updateAction.Invoke();
        }

        public void UiButtonInput()
        {
            activateInputManagerAction.Invoke();
        }

        private void ActivateInputManager()
        {
            activateInputManagerAction = NullAction;
            updateAction = GetInput;
            _playCommandPublisher.PlaySubscribers();
        }

        private void GetInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
            updateAction = NullAction;
            _playCommandPublisher.StopSubscribers();
            yield return new WaitForSeconds(timeOfBreak);
            _playCommandPublisher.PlaySubscribers();
            updateAction = GetInput;
        }
    }
}