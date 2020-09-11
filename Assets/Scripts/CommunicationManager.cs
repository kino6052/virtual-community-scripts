﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommunicationManager : MonoBehaviour
{
    public StoredMessage message = new StoredMessage(""); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // In
        OnPositionListener();
        // Out
        OnPresentListener();
        OnFullScreenListener();
    }

    // Receive
    void OnMessage(string s) {
        message = new StoredMessage(s);
    }

    void OnPositionListener() {
        if (message == null) return;
        if (message.type != MessageType.Position) return;
        var m = message;
        message = null;
        PositionMessage positionMessage = JsonUtility.FromJson<PositionMessage>(m.message);
        MultiplayerStatic.UpdateTargetById(positionMessage.id, positionMessage.position);
    }
    void OnImageDataListener() {
        if (message == null) return;
        if (message.type != MessageType.ImageData) return;
        var m = message;
        message = null;
        ImageMessage imageMessage = JsonUtility.FromJson<ImageMessage>(m.message);
        Static.base64Image = imageMessage.image;
    }

    // GUI
    void OnPresentListener() {
        if (!UIStatic.hasClickedPresent) return;
        UIStatic.hasClickedPresent = false;
        string json = JsonUtility.ToJson(new Message(MessageType.Present));
		Debug.Log(json);
		Static.SendUnityMessage(json);
    }

    void OnFullScreenListener() {
        if (!UIStatic.hasClickedFullScreen) return;
        UIStatic.hasClickedFullScreen = false;
        string json = JsonUtility.ToJson(new Message(MessageType.FullScreen));
		Debug.Log(json);
		Static.SendUnityMessage(json);
    }
}
