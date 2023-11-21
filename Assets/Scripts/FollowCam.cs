using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
  static public FollowCam followCamSingleton;
  static public GameObject POINT_OF_INTEREST;

  public enum eView {none, slingshot, castle, both};

  [Header("Inscribed")]
  public float easing = 0.05f;
  public Vector2 minXY = Vector2.zero;

  [Header("Dynamic")]
  public float cameraZ;

  void Awake() {
    cameraZ = this.transform.position.z;
  }

  void FixedUpdate()
  {
    Vector3 destination = Vector3.zero;

    if (POINT_OF_INTEREST != null) {
      Rigidbody poiRigidbody = POINT_OF_INTEREST.GetComponent<Rigidbody>();
      if ((poiRigidbody != null) && poiRigidbody.IsSleeping()) {
        POINT_OF_INTEREST = null;
      }
    }

    if (POINT_OF_INTEREST != null) {
      destination = POINT_OF_INTEREST.transform.position;
    }

    destination.x = Mathf.Max(minXY.x, destination.x);
    destination.y = Mathf.Max(minXY.y, destination.y);
    destination = Vector3.Lerp(transform.position, destination, easing);
    destination.z = cameraZ;
    transform.position = destination;

    Camera.main.orthographicSize = destination.y + 10;
  }
}
