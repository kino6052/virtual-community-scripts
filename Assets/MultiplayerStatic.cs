using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target {
  public Vector3 position = new Vector3();
  public Vector3 velocity = new Vector3();
  public float angularVelocity = 0f;
  public float yAngle = 0f;
  public bool isRunning = false;
  public bool isJumping = false;
} 

public static class MultiplayerStatic
{
    public delegate Transform InstantiateDelegate(Transform t);
    public static InstantiateDelegate DInstantiate;
    public static Transform player;
    public static Dictionary<string, Transform> transforms = new Dictionary<string, Transform>();
    public static Dictionary<string, Target> targets = new Dictionary<string, Target>();
    public static void UpdateTargetById(string id, PositionStructure structure) {
      if (!targets.ContainsKey(id)) targets[id] = new Target();
      Target target = targets[id];
      target.position = new Vector3(structure.x, structure.y, structure.z);
      target.yAngle = structure.yAngle; 
      target.isJumping = !target.isJumping && structure.isJumping;
      target.isRunning = structure.isRunning; 
    }
    public static Transform CreatePlayer() {
      var p = DInstantiate(player);
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
    public static void UpdatePositionById(string id, string name, PositionStructure structure) {
      if (!transforms.ContainsKey(id)) transforms.Add(id, CreatePlayer());
      var t = transforms[id];
      TextMesh textMesh = t.Find("Title")?.GetComponent<TextMesh>();
      textMesh.text = name;
      UpdateTargetById(id, structure);
    }
    public static void UpdateTextPosition(Transform t) {
      TextMesh textMesh = t.Find("Title")?.GetComponent<TextMesh>();
      var me = GameObject.Find("Me")?.transform;
      textMesh.transform.LookAt(me);
    }
    public static Target GetTargetById(string id) {
      if (!targets.ContainsKey(id)) return null;
      return targets[id];
    }
    public static Transform GetTransformById(string id) {
      if (!transforms.ContainsKey(id)) return null;
      return transforms[id];
    }
    public static void Animate(Transform transform, Target target) {
        Actions actions = transform.GetComponent<Actions> ();
        bool isJumping = target.isJumping;
        bool isRunning = target.isRunning;
        bool isWalking = target.velocity.magnitude > 0.2f && !isRunning;
        bool isStanding = !isRunning && !isWalking;
        if (isJumping) actions.SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
        else if (isRunning) actions.SendMessage("Run", SendMessageOptions.DontRequireReceiver);
        else if (isWalking) actions.SendMessage("Walk", SendMessageOptions.DontRequireReceiver);
        else if (isStanding) actions.SendMessage("Stay", SendMessageOptions.DontRequireReceiver);
    }
}
