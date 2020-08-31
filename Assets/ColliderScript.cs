using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    public GameObject screen;

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Enter, " + other.name);
        Static.ChangeChannel(screen.name);
    }

    //When the Primitive exits the collision, it will change Color
    private void OnTriggerExit(Collider other)
    {
        // Debug.Log("Exit, " + other.name);
        Static.ChangeChannel(null);
    }
}
