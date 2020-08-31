using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static
{
    [DllImport("__Internal")]
    private static extern void sendUnityMessage(string str);
    public static Texture2D texture;

    public static void SendTexture(string argsString) {
        string textureString = argsString;
        byte[] bTexture = System.Convert.FromBase64String(textureString);
        texture.LoadImage(bTexture);
    }

    public static void ChangeChannel(string channel) {
        Debug.Log(channel);
        sendUnityMessage(channel);
    }
}
