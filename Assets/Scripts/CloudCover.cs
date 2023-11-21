using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCover : MonoBehaviour
{
  [Header("Inscribed")]
  public Sprite[] cloudSprites;
  public int numClouds = 40;
  public Vector3 minPosition = new Vector3(-20, -5, -5);
  public Vector3 maxPosition = new Vector3(300, 40, 5);
  
  [Tooltip("For scaleRange, x is the min value and y is the max value")]
  public Vector2 scaleRange = new Vector2(1, 4);

  void Start(){
    Transform parentTransform = this.transform;
    GameObject cloudGameObject;
    Transform cloudTransform;
    SpriteRenderer cloudSpriteRenderer;
    float scaleMultiplier;

    for (int i = 0; i < numClouds; i++) {
      cloudGameObject = new GameObject();
      cloudTransform = cloudGameObject.transform;
      cloudSpriteRenderer = cloudGameObject.AddComponent<SpriteRenderer>();

      int spriteNum = Random.Range(0, cloudSprites.Length);
      cloudSpriteRenderer.sprite = cloudSprites[spriteNum];

      cloudTransform.position = GetRandomPosition();
      cloudTransform.SetParent(parentTransform, true);

      scaleMultiplier = Random.Range(scaleRange.x, scaleRange.y);
      cloudTransform.localScale = Vector3.one * scaleMultiplier;
    }
  }

  Vector3 GetRandomPosition() {
    Vector3 position = new Vector3();

    position.x = Random.Range(minPosition.x, maxPosition.x);
    position.y = Random.Range(minPosition.y, maxPosition.y);
    position.z = Random.Range(minPosition.z, maxPosition.z);

    return position;
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
