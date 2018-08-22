using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.gameObject.tag == "Player")
    {
      FindObjectOfType<Game>().shopping = false;
    }
  }
}
