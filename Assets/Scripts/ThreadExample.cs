using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThreadExample : MonoBehaviour
{
    Thread ChildThread = null;
    void MainLoop() {
        while (true) {
            string prev = "";
            if (Static.base64Image == prev) continue;
            prev = Static.base64Image;
            Static.SendTexture(Static.base64Image);
        }
    }
    void Awake(){
        ChildThread = new Thread(MainLoop);
        ChildThread.Start();
    }
}
