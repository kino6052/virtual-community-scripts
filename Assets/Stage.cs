using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        GameObject stage = CreateStage();
        stage.transform.position += new Vector3(0f, 25*UNIT, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float UNIT = 0.1f; 
    float SCALE_UNIT = 0.1f;

    GameObject CreateStick() {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(1.5f*UNIT, 100*UNIT, 1.5f*UNIT);
        cube.transform.position -= new Vector3(0f, 50*UNIT, 0f);
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
        renderer.material = material; 
        return cube;
    }

    GameObject CreateSquare() {
        GameObject empty = new GameObject("Square");
        GameObject cube01 = CreateStick();
        GameObject cube02 = CreateStick();
        GameObject cube03 = CreateStick();
        GameObject cube04 = CreateStick();
        empty.transform.position += new Vector3(50*UNIT, -50*UNIT, 0f);
        cube02.transform.Rotate(0f, 0f, 90f);
        cube02.transform.position += new Vector3(50*UNIT, 50*UNIT, 0f);
        cube03.transform.Rotate(0f, 0f, 180f);
        cube03.transform.position += new Vector3(100*UNIT, 0*UNIT, 0f);
        cube04.transform.Rotate(0f, 0f, 270f);
        cube04.transform.position += new Vector3(50*UNIT, -50*UNIT, 0f);
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
            GameObject square = CreateSquare();
            square.transform.position += new Vector3(0f, 0f, 10*i*UNIT);
            square.transform.rotation = Quaternion.Euler(0f, 0f, 10*i);
            square.transform.localScale -= new Vector3(SCALE_UNIT*i/2f, SCALE_UNIT*i/2f, SCALE_UNIT*i/2f);
            square.transform.parent = empty.transform;
        }
        GameObject sideScreens = CreateSideScreens();
        sideScreens.transform.parent = empty.transform;
        return empty;
    }

    GameObject CreateSideScreens() {
        GameObject empty = new GameObject("Stage");
        GameObject square01 = CreateSquare();
        square01.transform.position += new Vector3(100*UNIT, 0f, -50*UNIT);
        square01.transform.rotation = Quaternion.Euler(0f, 45f, 0f);
        square01.transform.parent = empty.transform;
        GameObject square02 = CreateSquare();
        square02.transform.position += new Vector3(-100*UNIT, 0f, -50*UNIT);
        square02.transform.rotation = Quaternion.Euler(0f, -45f, 0f);
        square02.transform.parent = empty.transform;
        return empty;
    }
}
