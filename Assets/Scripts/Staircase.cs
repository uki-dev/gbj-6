using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : MonoBehaviour
{
  [SerializeField]
  private bool _open;

  public bool shop;

  public Sprite opened;
  public Sprite closed;

  public Transform exit;
  public Transform exitRoom;

  public bool open
  {
    get
    {
      return _open;
    }
    set
    {
      _open = value;
      GetComponent<SpriteRenderer>().sprite = (open) ? opened : closed;
    }
  }

  void OnValidate()
  {
    open = _open;
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (open)
    {
      Player player = collider.GetComponent<Player>();
      if (player)
      {
        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.x = exitRoom.position.x;
        cameraPosition.y = exitRoom.position.y;
        Camera.main.transform.position = cameraPosition;
        player.transform.position = exit.transform.position;
        if (shop)
        {
          Game.instance.shopping = false;
        }
      }
    }
  }
}
