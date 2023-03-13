using UnityEngine;
using Network;

namespace DataTypes
{
    [System.Serializable]
    public class UsersInfo
    {
        public string sub;
        public string username;
        public string email;

        public static TokenInfo CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<TokenInfo>(jsonString);
        }
    }
}
