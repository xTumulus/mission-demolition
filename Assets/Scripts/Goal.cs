using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Goal : MonoBehaviour
{
  static public bool goalMet = false;

  void OnTriggerEnter(Collider other) {
    Projectile projectile = other.GetComponent<Projectile>();
    if(projectile != null) {
      Goal.goalMet = true;
      Material material = GetComponent<Renderer>().material;
      Color color = material.color;
      color.a = 0.75f;
      material.color = color;
    }
  }
}
