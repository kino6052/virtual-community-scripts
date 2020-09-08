using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Material[] materialArrayGlow;
    public Material materialPodium;
    public float unit = 0.1f;
    public float scaleUnit = 0.1f;
    public float thickness = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject stage = CreateStage();
        stage.transform.position += new Vector3(0f, 25*unit, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject CreateStick(Material m) {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(thickness*unit, 100*unit, thickness*unit);
        cube.transform.position -= new Vector3(0f, 50*unit, 0f);
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
        renderer.material = m; 
        return cube;
    }

    GameObject CreateSquare(Material m) {
        GameObject empty = new GameObject("Square");
        GameObject cube01 = CreateStick(m);
        GameObject cube02 = CreateStick(m);
        GameObject cube03 = CreateStick(m);
        GameObject cube04 = CreateStick(m);
        empty.transform.position += new Vector3(50*unit, -50*unit, 0f);
        cube02.transform.Rotate(0f, 0f, 90f);
        cube02.transform.position += new Vector3(50*unit, 50*unit, 0f);
        cube03.transform.Rotate(0f, 0f, 180f);
        cube03.transform.position += new Vector3(100*unit, 0*unit, 0f);
        cube04.transform.Rotate(0f, 0f, 270f);
        cube04.transform.position += new Vector3(50*unit, -50*unit, 0f);
        cube01.transform.parent = empty.transform;
        cube02.transform.parent = empty.transform;
        cube03.transform.parent = empty.transform;
        cube04.transform.parent = empty.transform;
        empty.transform.position = new Vector3(0f, 0f, 0f);
        return empty;
    }

    GameObject CreateStage() {
        GameObject empty = new GameObject("Stage");
        for (int i = 0; i < 10; i++)
        {
            GameObject square = CreateSquare(materialArrayGlow[i]);
            square.transform.position += new Vector3(0f, 0f, 10*i*unit);
            square.transform.rotation = Quaternion.Euler(0f, 0f, 10*i);
            square.transform.localScale -= new Vector3(scaleUnit*i/2f, scaleUnit*i/2f, scaleUnit*i/2f);
            square.transform.parent = empty.transform;
        }
        GameObject sideScreens = CreateSideScreens();
        GameObject podium = CreatePodium();
        sideScreens.transform.parent = empty.transform;
        podium.transform.parent = empty.transform;
        return empty;
    }

    GameObject CreateSideScreens() {
        GameObject empty = new GameObject("Side Screens");
        GameObject square01 = CreateSquare(materialArrayGlow[0]);
        square01.transform.position += new Vector3(100*unit, 0f, -50*unit);
        square01.transform.rotation = Quaternion.Euler(0f, 45f, 0f);
        square01.transform.parent = empty.transform;
        GameObject square02 = CreateSquare(materialArrayGlow[0]);
        square02.transform.position += new Vector3(-100*unit, 0f, -50*unit);
        square02.transform.rotation = Quaternion.Euler(0f, -45f, 0f);
        square02.transform.parent = empty.transform;
        return empty;
    }


    GameObject CreateLight() {
        GameObject empty = new GameObject("Light");
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Light lightComp = empty.AddComponent<Light>();
        lightComp.intensity = 1f;
        sphere.transform.parent = empty.transform;
        MeshRenderer renderer = sphere.GetComponent<MeshRenderer>();
        renderer.material = materialArrayGlow[0]; 
        empty.transform.position = new Vector3(0, 5, 0);
        return empty;
    }

    GameObject CreateLights(int number) {
        GameObject empty = new GameObject("Lights");
        float distance = 20*unit;
        for (int i = 0; i < number; i++)
        {
            GameObject lightLeft = CreateLight();
            lightLeft.transform.position = new Vector3(-distance, 0, distance*(number-1)/2-i*distance);
            lightLeft.transform.parent = empty.transform;
            GameObject lightRight = CreateLight();
            lightRight.transform.position = new Vector3(distance, 0, distance*(number-1)/2-i*distance);
            lightRight.transform.parent = empty.transform;
        }
        return empty;
    }
    GameObject CreatePodium() {
      GameObject empty = new GameObject("Podium");
      GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
      MeshRenderer renderer = cylinder.GetComponent<MeshRenderer>();
      renderer.material = materialPodium; 
      cylinder.transform.localScale = new Vector3(30*unit, 2*unit, 30*unit);
      cylinder.transform.parent = empty.transform;
      GameObject lights = CreateLights(5);
      lights.transform.parent = empty.transform;
      empty.transform.position = new Vector3(0f, -25*unit, -50*unit);
      return empty;
    }
}
