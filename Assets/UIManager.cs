using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        UIStatic.isOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab")) UIStatic.isOpen = true;
        MouseHandler();
        OnIsOpenHandler();
    }

    void MouseHandler() {
        bool isOpen = UIStatic.isOpen;
        CursorLockMode lockMode = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.lockState = lockMode;
        Cursor.visible = isOpen;
    }

    void OnIsOpenHandler() {
        if (player == null) return;
        bool isOpen = UIStatic.isOpen;
        var script = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        script.enabled = !isOpen;
    }
}
