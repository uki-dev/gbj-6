using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
  public static Vector2 Direction(Vector2 direction)
  {
    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    angle = Mathf.Round(angle / 45f) * 45f;
    Quaternion rotation = Quaternion.Euler(0f, 0f, -angle);
    return rotation * Vector2.up;
  }
}
