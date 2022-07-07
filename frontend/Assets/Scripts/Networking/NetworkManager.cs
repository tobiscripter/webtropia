using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;

public static class NetworkManager
{
    public static async Task<string> Get(string url)
    {
        try
        {
            using var www = UnityWebRequest.Get(url);

            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"Failed: {www.error}");

            return www.downloadHandler.text;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Webrequest failed: {ex.Message}");
            return default;
        }
    }
}
