using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Test {
	public float x;
}

public class Menu : MonoBehaviour {

	public GUISkin guiSkin;

	public static int width = 400;
	public static int height = 200;
	public static int left = 50;
	public static int right = 180;
	Rect windowRect = new Rect (0, 0, width, height);
	
	void Start () 
	{
		windowRect.x = (Screen.width - windowRect.width)/2;
		windowRect.y = (Screen.height - windowRect.height)/2;
	}


	void OnGUI () 
	{
		GUI.skin = guiSkin;
		if (UIStatic.isOpen) windowRect = GUI.Window (0, windowRect, DoMyWindow, "Main Menu");
		OnOkClickListener();
	}

	void DoMyWindow (int windowID) 
	{
		GUI.Label (new Rect (left,50, 150, 20), "Name");
		UIStatic.name = GUI.TextField (new Rect (left-5, 75, 160, 20), UIStatic.name, 25);
		UIStatic.hasClickedPresent = GUI.Button (new Rect (width-right,50,150,20), "PRESENT");
		UIStatic.hasClickedFullScreen = GUI.Button (new Rect (width-right,90,150,20), "FULL SCREEN");
		UIStatic.hasClickedOk = GUI.Button (new Rect (150,height-40,100,20), "OK");
		GUI.DragWindow (new Rect (0,0,10000,10000));
	}

	void OnOkClickListener () {
		if (UIStatic.hasClickedOk) {
			UIStatic.hasClickedOk = false;
			UIStatic.isOpen = false;
		};
	}
}
