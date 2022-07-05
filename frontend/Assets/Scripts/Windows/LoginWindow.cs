using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

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

        Debug.Log($"{e} {p}");

        await Task.CompletedTask;
    }


    public override async Task Init()
    {
        string s = await NetworkManager.get("www.google.com");
        Debug.Log(s);

        open = true;
        gameObject.SetActive(false);

        login.onClick.AddListener(async () => {await Login();});
        register.onClick.AddListener(async () => {await WindowManager.Navigate("REGISTER");});
        
    }
}
