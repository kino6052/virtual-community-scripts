using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTexture : MonoBehaviour
{
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        CreateTextures();
    }

    void Update() {
        Static.UpdateTexture();
    }
    
    public void CreateTextures() {
        if (material == null) return;
        Static.texture = new Texture2D(1024, 1024);
        material.mainTexture = Static.texture;
    }
}
