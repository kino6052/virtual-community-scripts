using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThreadExample : MonoBehaviour
{
    string prev = "";
    // Thread ChildThread = null;
    void Update() {
        if (Static.base64Image == prev) return;
        prev = Static.base64Image;
        Static.SendTexture(Static.base64Image);
    }
}
