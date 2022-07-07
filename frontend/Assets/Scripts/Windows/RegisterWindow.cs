using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class RegisterWindow : Window
{
    public TMPro.TMP_InputField username, password, email;
    public Button register;
    public override string GetID()
    {
        return "REGISTER";
    }
    
    public override async Task Init()
    {
        string s = await NetworkManager.Get("www.google.com");
        Debug.Log(s);

        open = true;
        gameObject.SetActive(false);
    }
}
