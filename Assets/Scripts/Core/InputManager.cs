using System.Collections;
using UnityEngine;

namespace Core
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private float timeOfBreak = 5;
        private Controls.PlayCommandPublisher _playCommandPublisher;
        [SerializeField] private bool canStopMove = false;
        private bool allowGetingKeys = false;
        void Awake()
        {
            _playCommandPublisher = new Controls.PlayCommandPublisher();
        }
        private void Update()
        {
            if (canStopMove && Input.GetKeyDown(KeyCode.Space))
            {
                StopForFiveSeconds(timeOfBreak);
            }
        }

        public void AllowGetingKeys()
        {
            if (allowGetingKeys) return;
            allowGetingKeys = true;
            canStopMove = true;
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