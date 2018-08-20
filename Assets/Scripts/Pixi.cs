using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pixi : MonoBehaviour
{
  public int width = 128;
  public int height = 128;
  public float scale = 1f;

  protected void Start()
  {
    if (!SystemInfo.supportsImageEffects)
    {
      enabled = false;
      return;
    }
  }

  void Update()
  {
    Camera camera = GetComponent<Camera>();
    float targetAspect = (float)width / (float)height;
    float windowAspect = (float)Screen.width / (float)Screen.height;
    float scaleHeight = windowAspect / targetAspect;
    if (scaleHeight < 1.0f) // letterbox
    {
      camera.rect = new Rect(0f, (1f - scaleHeight) / 2f, 1f, scaleHeight);
    }
    else // pillarbox
    {
      float scaleWidth = 1.0f / scaleHeight;
      camera.rect = new Rect((1f - scaleWidth) / 2f, 0f, scaleWidth, 1f);
    }
    camera.orthographicSize = height * 0.5f * scale;
  }

  void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    source.filterMode = FilterMode.Point;
    RenderTexture buffer = RenderTexture.GetTemporary(width, height, -1);
    buffer.filterMode = FilterMode.Point;
    Graphics.Blit(source, buffer);
    Graphics.Blit(buffer, destination);
    RenderTexture.ReleaseTemporary(buffer);
  }
}
