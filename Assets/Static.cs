using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class MessageType
{
    public static string Present    { get { return "present"; } }
    public static string FullScreen   { get { return "fullscreen"; } }
    public static string Position   { get { return "position"; } }
    public static string ImageData   { get { return "image"; } }
    public static string Start   { get { return "start"; } }
    public static string Proceed { get { return "proceed"; } }
}

public class Message {
    public string type;
    public Message(string t) {
        type = t;
    }
}

public class MessageWithId: Message {
    public string id;
    public MessageWithId(string t): base(t) {}
}

[System.Serializable]
public class StoredMessage: Message {
    public string message = "";
    public StoredMessage(string s): base("") {
        Message m = JsonUtility.FromJson<Message>(s);
        type = m?.type;
        message = s;
    }
}

[System.Serializable]
public class PositionMessage: MessageWithId {
    public PositionStructure position;
    public string name;
    public PositionMessage(): base(MessageType.Position) {}
}

[System.Serializable]
public class ImageMessage: MessageWithId {
    public string image;
    public ImageMessage(string t): base(t) {}
}

[System.Serializable]
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
    public static extern void SendUnityMessage(string str);
    public static Texture2D texture;
    public static byte[] bTexture = System.Convert.FromBase64String(Data.black);
    public static string base64Image = Data.black;
    public static StoredMessage message = new StoredMessage(""); 

    // Entry Point
    public static void OnMessage(string s) {
        message = new StoredMessage(s);
    }
    // Texture Related
    public static void SendTexture(string argsString) {
        string textureString = argsString;
        bool isEmpty = textureString == "" || textureString == null;
        if (isEmpty) textureString = Data.black;
        bTexture = System.Convert.FromBase64String(textureString);
    }
    public static void UpdateTexture() {
        if (!texture) return;
        texture.LoadImage(bTexture);
    }
    // Listeners
    public static void OnStart() {
        string json = JsonUtility.ToJson(new Message(MessageType.Start));
        Static.SendUnityMessage(json);
    }
    public static void OnProceed() {
        string json = JsonUtility.ToJson(new Message(MessageType.Proceed));
        Static.SendUnityMessage(json);
    }
    public static void OnPresentListener() {
        if (!UIStatic.hasClickedPresent) return;
        UIStatic.hasClickedPresent = false;
        string json = JsonUtility.ToJson(new Message(MessageType.Present));
		Static.SendUnityMessage(json);
    }
    public static void OnFullScreenListener() {
        if (!UIStatic.hasClickedFullScreen) return;
        UIStatic.hasClickedFullScreen = false;
        string json = JsonUtility.ToJson(new Message(MessageType.FullScreen));
		Debug.Log(json);
		Static.SendUnityMessage(json);
    }
    public static void OnPositionChange(string name, PositionStructure position) {
        var positionMessage = new PositionMessage();
        positionMessage.name = name;
        positionMessage.position = position;
        string json = JsonUtility.ToJson(positionMessage);
        Static.SendUnityMessage(json);
    }
    public static void OnPositionListener() {
        if (message == null) return;
        if (message.type != MessageType.Position) return;
        var m = message;
        message = null;
        PositionMessage positionMessage = JsonUtility.FromJson<PositionMessage>(m.message);
        MultiplayerStatic.UpdatePositionById(positionMessage.id, positionMessage.name, positionMessage.position);
    }
    public static void OnImageDataListener() {
        if (message == null) return;
        if (message.type != MessageType.ImageData) return;
        var m = message;
        message = null;
        ImageMessage imageMessage = JsonUtility.FromJson<ImageMessage>(m.message);
        Static.base64Image = imageMessage.image;
    }    // Debug
    public static void SendPositionDebug(string name, PositionStructure structure) {
        structure.z += 2f;
        MultiplayerStatic.UpdatePositionById("1", "Ratto", structure);
    }
}
