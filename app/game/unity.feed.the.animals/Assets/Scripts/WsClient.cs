﻿using System.Collections;
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
            ws.Send("Save Data...");
            Dictionary<string, object> dataToSave = new Dictionary<string, object>();

            //Add player position
            Vector3 playerPosition = GameObject.Find("Player").transform.position;
            dataToSave.Add("PlayerX", playerPosition.x);
            dataToSave.Add("PlayerY", playerPosition.y);
            dataToSave.Add("PlayerZ", playerPosition.z);

            // Add other game data here (like number of thrown meat, last collided animal, etc.)
            // Add other game data here
            dataToSave.Add("NumberOfMeatThrows", GameManager.Instance.numberOfMeatThrows);


            // Convert dictionary to JSON and send it
            string json = JsonConvert.SerializeObject(dataToSave);
            ws.Send(json);
        }
    }
}
