using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;

namespace HandTracking
{

    public class RenderUIOnHand : HandTrackingBootStrap
    {

        public GameObject MenuPrefab;
        // private GameObject MapPrefab;

        private GameObject renderObject;

        private string uiStatus;

        // Start is called before the first frame update
        void Start()
        {
            uiStatus = HandUI.Menu;
            RenderPrefab();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateGesture(HandJointID.Palm);
        }

        private void RenderPrefab()
        {
            if (uiStatus == HandUI.Menu)
                RenderMenuPrefab();
            else { }
            // GameObject map = Instantiate(MapObejct) as GameObject;
            // map.transform.SetParent(pinAnchor.transform, false);
        }

        private void RenderMenuPrefab()
        {

            Destroy(renderObject);
            renderObject = null;
            renderObject = Instantiate(MenuPrefab) as GameObject;
            renderObject.transform.SetParent(pinAnchor.transform, false);
            uiStatus = HandUI.Menu;
        }


        // private void RenderMapPrefab()
        // {
        //     GameObject map = Instantiate(MapObejct) as GameObject;
        //     map.transform.SetParent(pinAnchor.transform, false);
        //     uiStatus = HandUI.Map;
        // }
    }
}