using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
  public GUISkin guiSkin;
	void OnGUI () 
	{
		GUI.skin = guiSkin;
		GUI.Label (new Rect (20,20, 150, 20), "Press Tab for Menu");
	}
}
