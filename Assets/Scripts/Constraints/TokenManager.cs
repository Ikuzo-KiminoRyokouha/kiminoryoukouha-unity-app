using UnityEngine;

public static class TokenManager{
    public static void  SetToken(string key, string value){
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static string GetToken(string key){
        return PlayerPrefs.GetString(key);
    }

    public static void DeleteToken(string key){
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }

}