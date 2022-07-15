using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class RegisterWindow : Window
{
    public TMPro.TMP_InputField username, password, email;
    public Button register, login;
    public override string GetID()
    {
        return "REGISTER";
    }

    async Task Register()
    {
        Dictionary<string,string> form = new Dictionary<string, string>()
        {
            {"email", email.text},
            {"username", username.text},
            {"password", password.text}
        };

        Debug.Log(form.Count);

        string s = await NetworkManager.Post(AppSettings.API_URL + "account", form);

        Debug.Log(s);
    }
    
    private void Start()
    {
        register.onClick.AddListener(async () => {await WindowManager.Navigate("LOGIN");});
        login.onClick.AddListener(async () => {await Register();});
    }
}
