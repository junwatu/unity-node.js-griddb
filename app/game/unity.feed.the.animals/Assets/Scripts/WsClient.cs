using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System.IO;
using System;
using Newtonsoft.Json;

public class WsClient : MonoBehaviour
{
    WebSocket ws;

    void Start()
    {
        try
        {
            string configPath = Path.Combine(Application.dataPath, "config.json").Replace("\\", "/");
            Debug.Log("Config Path: " + configPath);
            string configJson = File.ReadAllText(configPath);
            Dictionary<string, string> configData = JsonConvert.DeserializeObject<Dictionary<string, string>>(configJson);
            string url = configData["WebSocketURL"];


            ws = new WebSocket(url);
            ws.Connect();

            ws.OnMessage += (sender, e) =>
            {
                Debug.Log("Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
            };
        }
        catch (Exception e)
        {
            Debug.LogError("Could not read config file: " + e.Message);
        }
    }

    void Update()
    {
        if (ws == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Hello");
        }
    }
}
