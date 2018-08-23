using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
  public Text waveText;
  public Text goldText;
  public RectTransform health;

  void Update()
  {
    if (Game.instance)
    {
      waveText.text = "Wave " + Game.instance.wave;
    }
    if (Player.instance)
    {
      goldText.text = Player.instance.gold.ToString();
      health.localScale = new Vector2((float)Player.instance.health / Player.instance.healthMax, 1f);
    }
  }
}
