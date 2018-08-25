using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintText : MonoBehaviour
{
  public float speed = 0.1f;

  private Text text;

  void Start()
  {
    text = GetComponent<Text>();
    StartCoroutine(Print(text.text));
  }

  IEnumerator Print(string text)
  {
    this.text.text = "";
    foreach (char c in text)
    {
      yield return new WaitForSeconds(speed);
      this.text.text += c;
    }
  }
}
