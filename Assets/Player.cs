using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PositionStructure position = new PositionStructure();
    string name = "";
    // Start is called before the first frame update
    void Start()
    {
        Static.OnStart();    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateName();
        var lastPosition = UpdatePosition();
        SendData(lastPosition);
    }
    PositionStructure UpdatePosition() {
        var controller = transform.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        Transform pivot = transform.Find("Pivot")?.GetComponent<Transform>();
        var lastPosition = position;
        position = new PositionStructure();
        position.x = pivot.position.x;
        position.y = pivot.position.y > 0 ? pivot.position.y : 0;
        position.z = pivot.position.z;
        position.yAngle = pivot.eulerAngles.y;
        position.isRunning = !controller.m_IsWalking;
        position.isJumping = false;
        return lastPosition;
    }
    void UpdateName() {
        name = UIStatic.name;
    }
    void SendData(PositionStructure lastPosition) {
        if (
            lastPosition.x == position.x &&
            lastPosition.y == position.y &&
            lastPosition.z == position.z &&
            lastPosition.yAngle == position.yAngle
        ) return;
        Static.OnPositionChange(name, position);
    }
}
