using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
  const int LOOKBACK_COUNT = 10;
  static List<Projectile> PROJECTILES = new List<Projectile>();

  [SerializeField]
  private bool _awake = true;
  public bool awake {
    get { return _awake; }
    private set { _awake = value; }
  }

  private Vector3 previousPosition;
  private List<float> deltas = new List<float>();
  private Rigidbody rigidbody;

  void Start()  {
    rigidbody = GetComponent<Rigidbody>();
    awake = true;
    previousPosition = new Vector3(1000, 1000, 0);
    deltas.Add(1000);

    PROJECTILES.Add(this);      
  }

  void FixedUpdate()
  {
    if (rigidbody.isKinematic || !awake) { return; }

    Vector3 deltaV3 = transform.position - previousPosition;
    deltas.Add(deltaV3.magnitude);
    previousPosition = transform.position;

    while (deltas.Count > LOOKBACK_COUNT) {
      deltas.RemoveAt(0);
    }

    float maxDelta = 0;

    foreach(float f in deltas) {
      if (f > maxDelta) { maxDelta = f; }
    }

    if (maxDelta <= Physics.sleepThreshold) {
      awake = false;
      rigidbody.Sleep();
    }   
  }

  private void OnDestroy() {
    PROJECTILES.Remove(this);
  }

  static public void DESTROY_PROJECTILES() {
    foreach (Projectile projectile in PROJECTILES) {
      Destroy(projectile.gameObject);
    }
  }
}
