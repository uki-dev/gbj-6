using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
  void Update()
  {
    if (!attacking)
    {
      Vector2 direction = Vector2.zero;
      if (Input.GetKey(KeyCode.UpArrow))
        direction.y += 1;
      if (Input.GetKey(KeyCode.DownArrow))
        direction.y -= 1;
      if (Input.GetKey(KeyCode.LeftArrow))
        direction.x -= 1;
      if (Input.GetKey(KeyCode.RightArrow))
        direction.x += 1;

      Walk(direction);
    }

    if (Input.GetKeyDown(KeyCode.Z) && canAttack)
      StartCoroutine(Attack());
  }
}
