using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D collider)
  {
    if (Game.instance && collider.tag == "Player")
    {
      Game.instance.shopping = false;
    }
  }
}
