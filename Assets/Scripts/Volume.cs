using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
  public float volume;

  void Update()
  {
    AudioListener.volume = volume;
  }
}
