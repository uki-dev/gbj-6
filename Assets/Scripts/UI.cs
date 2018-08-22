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
    waveText.text = "Wave " + FindObjectOfType<Game>().wave;
    goldText.text = FindObjectOfType<Player>().gold.ToString();
  }
}
