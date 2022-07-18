using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class CreateChatWindow : Window
{
    public Button createChat;
    public TMPro.TMP_InputField name_field, description_field, tags_field;

    public override string GetID()
    {
        return "CREATE_CHAT";
    }    

    async Task CreateChat()
    {
        Dictionary<string,string> form = new Dictionary<string, string>()
        {
            {"name", name_field.text},
            {"description", description_field.text},
            {"tags", tags_field.text},
            {"type", "PUBLIC"}
        };

        await WindowManager.StartLoading();
        UnityWebRequest s = await NetworkManager.PostWR(AppSettings.API_URL + "chat", form);

        await WindowManager.StopLoading();

        Debug.Log(s.downloadHandler.text);
        
        if(s.result != UnityWebRequest.Result.Success)
            return;
        
        await WindowManager.Navigate("CHAT");

    }

    public override Task Init() 
    {
        createChat.onClick.AddListener(async () => {await CreateChat();});

        return Task.CompletedTask;
    }
}
