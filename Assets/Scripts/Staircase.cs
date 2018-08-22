using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : MonoBehaviour
{
  [SerializeField]
  private bool _opened;

  public Sprite open;
  public Sprite closed;

  public Transform exit;
  public Transform exitRoom;

  public bool opened
  {
    get
    {
      return _opened;
    }
    set
    {
      _opened = value;
      GetComponent<SpriteRenderer>().sprite = (opened) ? open : closed;
    }
  }

  void OnValidate()
  {
    opened = opened;
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (opened)
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
}
