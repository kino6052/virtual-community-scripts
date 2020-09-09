﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class PositionStructure {
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    public float yAngle = 0f;
    public bool isJumping = false;
    public bool isRunning = false;
}
public static class Static
{
    [DllImport("__Internal")]
    private static extern void SendUnityMessage(string str);
    public static Texture2D texture;

    public static void SendTexture(string argsString) {
        string textureString = argsString;
        bool isEmpty = textureString == "" || textureString == null;
        if (isEmpty) textureString = Data.black;
        byte[] bTexture = System.Convert.FromBase64String(textureString);
        texture.LoadImage(bTexture);
    }

    public static void ChangeChannel(string channel) {
        Debug.Log(channel);
        SendUnityMessage($"channel,{channel}");
    }

    public static void SendPosition(float x, float y, float z, float yAngle) {
        SendUnityMessage($"position,{x},{y},{z},{yAngle}");
    }
    public static void SendPositionDebug(PositionStructure structure) {
        structure.z += 2f;
        MultiplayerStatic.UpdatePositionById("1", "Ratto", structure);
    }
}
