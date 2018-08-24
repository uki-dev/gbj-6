using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
  public static Vector2 Direction(Vector2 direction)
  {
    if (direction != Vector2.zero)
    {
      const float degrees = 45f;
      float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
      angle = Mathf.Round(angle / degrees) * degrees;
      return Quaternion.Euler(0f, 0f, -angle) * Vector2.up;
    }
    return Vector2.zero;
  }
}
