using System;
using Unity.VisualScripting;
using UnityEngine;
using static Core.Globals;

namespace Sphere
{
    public class ColorChange : MonoBehaviour, IPlayable
    {
        [SerializeField] private Color32 myColor;
        [SerializeField] Material myMaterial;

        private byte r, g, b, a = 255;
        private int multiplier = 100;
        private int moduloDivider = 255;
        private Action changeColorAction;
        private bool started = false;

        void Start()
        {
            changeColorAction = NullAction;
            var moveCommandPublisher = new Controls.PlayCommandPublisher();
            moveCommandPublisher.Subscribe(this.gameObject.GetInstanceID(), this);
            UpdateColor();
        }

        void Update()
        {
            changeColorAction.Invoke();
        }

        public void Play()
        {
            if (started) return;
            changeColorAction = UpdateColor;
            UpdateStartedCondition();
        }

        public void Stop()
        {
            if (!started) return;
            changeColorAction = NullAction;
            UpdateStartedCondition();
        }

        private void UpdateStartedCondition()
        {
            started = !started;
        }

        private void UpdateColor()
        {
            r = (byte)((transform.position.x * multiplier) % moduloDivider);
            g = (byte)((transform.position.z * multiplier) % moduloDivider);
            b = (byte)(((transform.position.x + transform.position.z) * multiplier) % moduloDivider);
            myColor = new Color32(r, g, b, a);
            myMaterial.color = myColor;
        }
    }
}

