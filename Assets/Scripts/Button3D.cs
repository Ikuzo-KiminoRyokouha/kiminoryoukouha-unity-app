using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Threading;
using Debug = UnityEngine.Debug;


namespace UI.Button
{
    public class Button3D : MonoBehaviour, IPointerClickHandler
    {

        public GameObject button;

        public delegate void OnClick();

        bool isPressed;

        public OnClick onClick;

        // Start is called before the first frame update
        void Start()
        {
            isPressed = false;
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            OnTriggerClick();
        }

        protected void SetOnClickEvent(OnClick onClick)
        {
            this.onClick = onClick;
        }

        private void OnTriggerClick()
        {
            if (!isPressed)
            {
                button.transform.localPosition = new Vector3(0, 0.003f, 0);
                isPressed = true;
                onClick();
                Invoke("OnTriggerExit", 0.3f);
            }
        }

        private void OnTriggerExit()
        {
            if (isPressed)
            {
                button.transform.localPosition = new Vector3(0, 0.015f, 0);
                isPressed = false;
            }
        }

    }
}

