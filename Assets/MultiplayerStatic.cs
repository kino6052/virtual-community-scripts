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
    public delegate void DestroyDelegate(Transform t);
    public static DestroyDelegate DDestroy;
    public static float smoothTime;
    public static Transform player;
    public static int TTL = 1000;
    public static Dictionary<string, Transform> transforms = new Dictionary<string, Transform>();
    public static Dictionary<string, Target> targets = new Dictionary<string, Target>();
    public static Dictionary<string, int> TTLDictionary = new Dictionary<string, int>();
    public static void UpdateTargetById(string id, PositionStructure structure) {
      if (!targets.ContainsKey(id)) { 
        targets[id] = new Target();
        TTLDictionary[id] = TTL;
      }
      RefreshTTL(id);
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
      t.transform.position = p.transform.position + new Vector3(0f, 2.2f, 0f);
      t.color = new Color(255, 255, 255);
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
    public static int GetTTLById(string id) {
      if (!TTLDictionary.ContainsKey(id)) return -1;
      return TTLDictionary[id];
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
    public static void RemovePlayer(string id, Transform transform) {
      transforms.Remove(id);
      targets.Remove(id);
      TTLDictionary.Remove(id);
      Destroy(transform.parent);
    }
    public static void UpdateByFrame(string id) {
        var ttl = ReduceTTL(id);
        Target target = MultiplayerStatic.GetTargetById(id);
        Transform transform = MultiplayerStatic.GetTransformById(id);
        if (ttl == 0) RemovePlayer(id, transform);
        if (target == null || transform == null) return;
        MultiplayerStatic.Animate(transform, target);
        MultiplayerStatic.UpdateTextPosition(transform);
        var updatedPosition = Vector3.SmoothDamp(transform.position, target.position, ref target.velocity, smoothTime);
        float updatedYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.yAngle, ref target.angularVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0, updatedYAngle, 0);
        transform.position = updatedPosition;
    }
    private static int ReduceTTL(string id) {
        var ttl = MultiplayerStatic.GetTTLById(id);
        if (ttl == -1) return -1;
        ttl -= 1;
        if (ttl < 0) ttl = 0;
        TTLDictionary[id] = ttl;
        return ttl;
    }

    private static void RefreshTTL(string id) {
      var ttl = MultiplayerStatic.GetTTLById(id);
      if (ttl == -1) return;
      TTLDictionary[id] = TTL;
    }
}
