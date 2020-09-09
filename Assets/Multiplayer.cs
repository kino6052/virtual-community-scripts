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
        UpdateByFrame("1");
        // counter++;
        // if (counter < 100) return;
        // MultiplayerStatic.UpdatePositionById("1", "Ratto", 0f, 0f, Time.time, Time.time * 100);
        // counter = 0;
    }
    private void UpdatePositionById(string input) {
        string[] splitInput = input.Split(',');
        if (splitInput.Length != 6) return;
        var id = splitInput[0];
        var name = splitInput[1];
        var x = splitInput[2];
        var y = splitInput[3];
        var z = splitInput[4];
        var yAngle = splitInput[5];
        // MultiplayerStatic.UpdatePositionById(id, name, float.Parse(x), float.Parse(y), float.Parse(z), float.Parse(yAngle));
    }
    private void UpdateByFrame(string id) {
        Target target = MultiplayerStatic.GetTargetById(id);
        Transform transform = MultiplayerStatic.GetTransformById(id);
        if (target == null || transform == null) return;
        MultiplayerStatic.Animate(transform, target);
        var updatedPosition = Vector3.SmoothDamp(transform.position, target.position, ref target.velocity, smoothTime);
        float updatedYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.yAngle, ref target.angularVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0, updatedYAngle, 0);
        transform.position = updatedPosition;
    }
}
