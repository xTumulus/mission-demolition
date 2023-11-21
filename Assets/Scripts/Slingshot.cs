using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
  [Header("Inscribed")]
  public GameObject projectilePrefab;
  public float velocityMultiplier = 10f;
  public GameObject projectileLinePrefab;

  [Header("Dynamic")]
  public GameObject launchPoint;
  public Vector3 launchPosition;
  public GameObject projectile;
  public bool aimingMode;

  void Awake() {
    Transform launchPointTransform = transform.Find("LaunchPoint");
    launchPoint = launchPointTransform.gameObject;
    launchPoint.SetActive(false);
    launchPosition = launchPointTransform.position;
  }
    void OnMouseEnter() {
    launchPoint.SetActive(true);
    }

    void OnMouseExit() {
    launchPoint.SetActive(false);
    }

    void OnMouseDown() {
      aimingMode = true;
      projectile = Instantiate(projectilePrefab) as GameObject;
      projectile.transform.position = launchPosition;
      projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update() {
      if (!aimingMode) { return; }

      Vector3 mousePosition2D = Input.mousePosition;
      mousePosition2D.z = -Camera.main.transform.position.z;
      Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(mousePosition2D);

      Vector3 mouseDelta = mousePosition3D - launchPosition;
      float maxMagnitude = this.GetComponent<SphereCollider>().radius;
      if (mouseDelta.magnitude > maxMagnitude) {
        mouseDelta.Normalize();
        mouseDelta *= maxMagnitude;
      }

      Vector3 projectilePosition = launchPosition + mouseDelta;
      projectile.transform.position = projectilePosition;

      if (Input.GetMouseButtonUp(0)) {
        aimingMode = false;
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = false;
        projectileRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        projectileRigidbody.velocity = -mouseDelta * velocityMultiplier;
        FollowCam.POINT_OF_INTEREST = projectile;
        Instantiate<GameObject>(projectileLinePrefab, projectile.transform);
        projectile = null;
        MissionDemolition.SHOT_FIRED();
      }
    }
}
