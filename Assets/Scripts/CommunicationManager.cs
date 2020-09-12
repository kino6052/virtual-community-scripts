using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommunicationManager : MonoBehaviour
{
    void Update()
    {
        // In
        Static.OnPositionListener();
        Static.OnImageDataListener();
        // Out
        Static.OnPresentListener();
        Static.OnFullScreenListener();
    }

    public void OnMessage(string s) {
        Static.OnMessage(s);
    }
}
