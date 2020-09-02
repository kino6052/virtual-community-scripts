using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Multiplayer : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private float angularVelocity = 0.0f;
    private Dictionary<string, Transform> positions = new Dictionary<string, Transform>(); 

    void Update()
    {
        UpdatePositionById("1", "Ratto", Time.time, 1, 5, 0);
    }

    public Transform CreatePlayer() {
        var p = Instantiate(player);
        GameObject text = new GameObject();
        text.name = "Title";
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = "Player";
        t.fontSize = 12;
        t.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        t.transform.position = p.transform.position + new Vector3(0f, 1.9f, 0f);
        t.color = new Color(255, 0, 0);
        t.alignment = TextAlignment.Center;
        text.transform.parent = p;
        return p;
    }

    public void UpdatePositionById(string id, string name, float x, float y, float z, float yAngle) {
        if (!positions.ContainsKey(id)) positions.Add(id, CreatePlayer());
        var transform = positions[id];
        TextMesh textMesh = transform.Find("Title")?.GetComponent<TextMesh>();
        var me = GameObject.Find("Me")?.transform;
        textMesh.transform.LookAt(me);
        textMesh.text = name;
        Vector3 targetPosition = new Vector3(x, y, z);
        if (transform == null) return;
        float _yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, yAngle, ref angularVelocity, smoothTime);
        var position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        position += Quaternion.Euler(0, _yAngle, 0) * Vector3.zero;
        transform.position = position;
    }
}
