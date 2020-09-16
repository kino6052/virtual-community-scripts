using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Transform))]
public class Multiplayer : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.01f;
    
    void Start() {
        MultiplayerStatic.player = player;
        MultiplayerStatic.DInstantiate = Instantiate;
        MultiplayerStatic.DDestroy = Destroy;
        MultiplayerStatic.smoothTime = smoothTime;
    }
    void Update()
    {
        StartCoroutine("UpdateByFrameCoroutine");
    }
    
    private IEnumerator UpdateByFrameCoroutine() {
        foreach (var t in MultiplayerStatic.transforms)
        {
            MultiplayerStatic.UpdateByFrame(t.Key);
            yield return null; 
        }
    }
}
