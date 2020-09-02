using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float x = 0.0f;
    float y = 0.0f;
    float z = 0.0f;
    float yAngle = 0.0f;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ResetCounter() {
        counter = 500;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= 1;
        if (counter >= 0) return;
        var _x = transform.position.x;
        var _y = transform.position.y;
        var _z = transform.position.z;
        var _yAngle = transform.eulerAngles.y;
        if (_x == x && _y == y && _z == z && _yAngle = yAngle) return;
        Debug.Log("Change");
        x = _x;
        y = _y;
        z = _z;
        yAngle = _yAngle;
        Static.SendPosition(_x, _y, _z, _yAngle);
        ResetCounter();
    }
}
