using UnityEngine;

public static class UserInformation
{
    public static string username;
    public static string email;
    public static string password;

    public static string privateKey;
    public static string publicKey;


    public static void Save()
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("email", email);
        PlayerPrefs.SetString("password", password);
        PlayerPrefs.SetString("privateKey", privateKey);
        PlayerPrefs.SetString("publicKey", publicKey);
    }

    public static void Load()
    {
        username = PlayerPrefs.GetString("username");
        email = PlayerPrefs.GetString("email");
        password = PlayerPrefs.GetString("password");
        privateKey = PlayerPrefs.GetString("privateKey");
        publicKey = PlayerPrefs.GetString("publicKey");
    }

    public static void Show()
    {
        Debug.Log("username: " + username);
        Debug.Log("email: " + email);
        Debug.Log("password: " + password);
        Debug.Log("privateKey: " + privateKey);
        Debug.Log("publicKey: " + publicKey);
    }
    
}
