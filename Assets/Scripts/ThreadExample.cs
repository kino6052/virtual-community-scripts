using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThreadExample : MonoBehaviour
{
    Thread ChildThread = null;
    string outside = "hey!";
    void MainLoop() {
        Debug.Log(outside);
        outside = "test";
        Debug.Log(outside);
        ChildThread.Join();
    }
    void Awake(){
        ChildThread = new Thread(MainLoop);
        ChildThread.Start();
    }
}
