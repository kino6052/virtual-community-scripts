using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Static
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
        SendUnityMessage(channel);
    }
}
