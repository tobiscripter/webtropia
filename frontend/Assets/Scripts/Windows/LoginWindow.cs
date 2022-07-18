using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class LoginWindow : Window
{
    public TMPro.TMP_InputField email, password;
    public Button login, register;
    public override string GetID()
    {
        return "LOGIN";
    }
    
    async Task Login()
    {
        string e = email.text;
        string p = password.text;
        
        await WindowManager.StartLoading();
        UnityWebRequest s = await NetworkManager.GetWR(AppSettings.API_URL + "account");
        await WindowManager.StopLoading();

//Request header: raw source
        if(s.result != UnityWebRequest.Result.Success)
            return;

        UserInformation.email = e;
        UserInformation.password = p;

        JObject res = JObject.Parse(s.downloadHandler.text);
        UserInformation.privateKey = (string)res["privateKey"];
        UserInformation.publicKey = (string)res["publicKey"];
        UserInformation.username = (string)res["username"];
        UserInformation.Save();
        
        await WindowManager.Navigate("MENU");

    }


    private void Start()
    {
        login.onClick.AddListener(async () => {await Login();});
        register.onClick.AddListener(async () => {await WindowManager.Navigate("REGISTER");});

        email.text = UserInformation.email;
        password.text = UserInformation.password;
    }
}
