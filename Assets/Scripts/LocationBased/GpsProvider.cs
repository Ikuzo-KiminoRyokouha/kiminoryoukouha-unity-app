using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using static LatLng;
using Debug = UnityEngine.Debug;

namespace GPS
{
    public class GpsProvider : MonoBehaviour
    {
        protected float latitude = 35.94734f;
        protected float longitude = 128.4638f;
        private float earthRadius = 6378137f;
        public Vector3 unityCoor; // unityCoor를 담을 변수
        private float degreeToRadian = Mathf.PI / 180f;

        public float resendTime = 1.0f;

        public bool receiveGPS = false;

        protected float GetUnityX(double latitude, double longitude)
        {
            float x = (float)(earthRadius * Mathf.Cos((float)(latitude * degreeToRadian)) * Mathf.Sin((float)(longitude * degreeToRadian)));
            return x;
        }

        protected float GetUnityZ(double latitude, double longitude)
        {
            float z = (float)(earthRadius * Mathf.Cos((float)(latitude * degreeToRadian)) * Mathf.Cos((float)(longitude * degreeToRadian)));
            return z;
        }

        protected Vector3 GeoToWorldPosition(LatLng origin, LatLng target)
        {
            Vector3 originPos = new Vector3(GetUnityX(origin.latitude, origin.longitude), 0f, GetUnityZ(origin.latitude, origin.longitude));

            float x = GetUnityX(target.latitude, target.longitude);
            float z = GetUnityZ(target.latitude, target.longitude);
            Vector3 pos = new Vector3(x - originPos.x, 0f, z - originPos.z);
            return pos;
        }

        protected void AddObjUseGeo(LatLng origin, LatLng target, GameObject prefab)
        {
            Vector3 pos = GeoToWorldPosition(origin, target);
            Instantiate(prefab, pos, Quaternion.identity);
        }

        protected IEnumerator UseGps()
        {
            if (!Input.location.isEnabledByUser)
            {
                Debug.Log("Location services are not enabled");
                yield break;
            }

            // Start location service
            Input.location.Start();
            int maxWait = 20;
            // Wait until location service is initialized
            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                maxWait--;
                yield return new WaitForSeconds(1);
                if (maxWait < 0)
                {
                    break;
                }
            }

            // Check if location service has failed
            if (Input.location.status == LocationServiceStatus.Failed || maxWait < 0)
            {
                Debug.Log("Location services failed");
                yield break;
            }
            LocationInfo li = Input.location.lastData;

            // Location service is initialized and ready
            latitude = li.latitude;
            longitude = li.longitude;

            receiveGPS = true;
            while (receiveGPS)
            {
                li = Input.location.lastData;
                latitude = li.latitude;
                longitude = li.longitude;
                unityCoor = GPSEncoder.GPSToUCS(latitude, longitude);
                yield return new WaitForSeconds(resendTime);
            }
        }
    }

}
