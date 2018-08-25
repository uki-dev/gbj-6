using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
  public Sprite small;
  public Sprite large;

  public AudioClip pickupSound;

  public int amount
  {
    get
    {
      return _amount;
    }
    set
    {
      _amount = value;
      GetComponent<SpriteRenderer>().sprite = (amount < 3) ? small : large;
    }
  }
  [SerializeField]
  int _amount;

  void OnValidate()
  {
    amount = _amount;
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    Player player = collider.GetComponent<Player>();
    if (player)
    {
      player.gold += amount;
      AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);
      Destroy(gameObject);
    }
  }
}
