using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class ChatWindow : Window
{
    public Button createChat;
    public override string GetID()
    {
        return "CHAT";
    }    

    public override Task Init() 
    {
        createChat.onClick.AddListener(async () => {await WindowManager.Navigate("CREATE_CHAT");});

        return Task.CompletedTask;
    }
}
