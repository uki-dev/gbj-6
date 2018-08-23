using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
  public Text waveText;
  public Text goldText;

  void Update()
  {
    if (Game.instance && waveText)
    {
      waveText.text = "Wave " + Game.instance.wave;
    }
    if (Player.instance && goldText)
    {
      goldText.text = Player.instance.gold.ToString();
    }
  }
}
