using System.Numerics;
using System;
using Vector3 = UnityEngine.Vector3;
using UnityEngine;



namespace CustomMath
{
    public static class GeoMath
    {
        private static float earthRadius = 6378137f;
        private static float degreeToRadian = Mathf.PI / 180f;
        public static float GetUnityX(double latitude, double longitude)
        {
            float x = (float)(earthRadius * Mathf.Cos((float)(latitude * degreeToRadian)) * Mathf.Sin((float)(longitude * degreeToRadian)));
            return x;
        }

        public static float GetUnityZ(double latitude, double longitude)
        {
            float z = (float)(earthRadius * Mathf.Cos((float)(latitude * degreeToRadian)) * Mathf.Cos((float)(longitude * degreeToRadian)));
            return z;
        }

        public static Vector3 GeoToWorldPosition(LatLng origin, LatLng target)
        {
            Vector3 originPos = new Vector3(GetUnityX(origin.latitude, origin.longitude), 0f, GetUnityZ(origin.latitude, origin.longitude));

            float x = GetUnityX(target.latitude, target.longitude);
            float z = GetUnityZ(target.latitude, target.longitude);
            Vector3 pos = new Vector3(x - originPos.x, 0f, z - originPos.z);
            return pos;
        }
    }
}