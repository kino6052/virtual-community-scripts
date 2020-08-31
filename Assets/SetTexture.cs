using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTexture : MonoBehaviour
{
    public Material m;

    // Start is called before the first frame update
    void Start()
    {
        CreateTextures();
        Static.SendTexture(Data.image);
        Static.SendTexture(Data.image);
    }
    
    public void CreateTextures() {
        if (m == null) return;
        Static.texture = new Texture2D(512, 512);
        Debug.Log(m);
        m.mainTexture = Static.texture;
    }
}
