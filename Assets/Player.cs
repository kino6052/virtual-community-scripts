using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float x = 0.0f;
    float y = 0.0f;
    float z = 0.0f;
    float yAngle = 0.0f;
    string name = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateName();
        UpdatePosition();
        SendData();
    }
    void UpdatePosition() {
        Transform pivot = transform.Find("Pivot")?.GetComponent<Transform>();
        var _x = pivot.position.x;
        var _y = pivot.position.y;
        var _z = pivot.position.z;
        var _yAngle = pivot.eulerAngles.y;
        if (_x == x && _y == y && _z == z && _yAngle == yAngle) return;
        x = _x;
        y = _y;
        z = _z;
        yAngle = _yAngle;
    }
    void UpdateName() {
        name = UIStatic.name;
    }
    void SendData() {
        var controller = transform.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        PositionStructure structure = new PositionStructure();
        structure.x = x;
        structure.y = y;
        structure.z = z;
        structure.yAngle = yAngle;
        structure.isRunning = !controller.m_IsWalking;
        structure.isJumping = false;
        Static.SendPositionDebug(name, structure);
    }
}
