using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class MenuWindow : Window
{
    public TMPro.TMP_Text username;
    public Button goToChat;
    public override string GetID()
    {
        return "MENU";
    }

    public override Task Init() 
    {
        username.text = UserInformation.username;
        goToChat.onClick.AddListener(async () => {await WindowManager.Navigate("CHAT");});

        return Task.CompletedTask;
    }
}
