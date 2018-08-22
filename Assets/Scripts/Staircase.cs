using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : MonoBehaviour
{
  public Transform exit;
  public Transform exitRoom;

  void OnTriggerEnter2D(Collider2D collider)
  {
    Player player = collider.GetComponent<Player>();
    if (player)
    {
      Vector3 cameraPosition = Camera.main.transform.position;
      cameraPosition.x = exitRoom.position.x;
      cameraPosition.y = exitRoom.position.y;
      Camera.main.transform.position = cameraPosition;
      player.transform.position = exit.transform.position;
    }
  }
}
