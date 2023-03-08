using UnityEngine;

namespace Network {
    [System.Serializable]
    public class TokenInfo{
        public string accessToken;

        public static TokenInfo CreateFromJSON(string jsonString){
            return  JsonUtility.FromJson<TokenInfo>(jsonString);
        }
    }
}
