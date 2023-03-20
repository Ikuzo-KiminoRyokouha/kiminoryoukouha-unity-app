using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace UI.Button
{
    public class MenuButton : Button3D
    {
        public GameObject menuObject;

        public GameObject buttonObject;

        private bool currentStatus;

        private bool CurrentStatus
        {
            get
            {
                return currentStatus;
            }
            set
            {
                buttonObject.SetActive(!value);
                menuObject.SetActive(value);
                currentStatus = value;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            CurrentStatus = false;
            SetOnClickEvent(CustomClickEvent);
        }

        // Update is called once per frame  
        void Update()
        {

        }

        public void CustomClickEvent()
        {
            Debug.Log("CustomClickEvent is occured");
            CurrentStatus = !CurrentStatus;
        }

    }

}
