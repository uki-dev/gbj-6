using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
  protected virtual void Update()
  {
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
  }
}
