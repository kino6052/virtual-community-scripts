using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Transform))]
public class Multiplayer : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.01f;
    private int counter = 0;
    private Vector3 target = new Vector3();
    
    void Start() {
        MultiplayerStatic.player = player;
        MultiplayerStatic.DInstantiate = Instantiate;
    }
    void Update()
    {
        StartCoroutine("UpdateByFrameCoroutine");
    }
    private void UpdateByFrame(string id) {
        Target target = MultiplayerStatic.GetTargetById(id);
        Transform transform = MultiplayerStatic.GetTransformById(id);
        if (target == null || transform == null) return;
        MultiplayerStatic.Animate(transform, target);
        MultiplayerStatic.UpdateTextPosition(transform);
        var updatedPosition = Vector3.SmoothDamp(transform.position, target.position, ref target.velocity, smoothTime);
        float updatedYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.yAngle, ref target.angularVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0, updatedYAngle, 0);
        transform.position = updatedPosition;
    }
    private IEnumerator UpdateByFrameCoroutine() {
        foreach (var t in MultiplayerStatic.transforms)
        {
            UpdateByFrame(t.Key);
            yield return null; 
        }
    }
}
