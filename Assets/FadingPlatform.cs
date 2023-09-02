using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatform : MonoBehaviour
{
  public float fadeSpeed = 0.5f;  // Speed at which the platform fades
  public float waitTime = 2.0f;   // Time to wait before starting to fade

  private SpriteRenderer spriteRenderer;
  private Collider2D collider2D;
  private bool isFading = false;

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    collider2D = GetComponent<CapsuleCollider2D>();
    StartCoroutine(FadeInOut());
  }

  IEnumerator FadeInOut()
  {
    while (true)
    {
      yield return new WaitForSeconds(waitTime);

      // Fade out
      for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * fadeSpeed)
      {
        SetAlpha(alpha);
        yield return null;
      }

      // Disable collider when fully transparent
      collider2D.enabled = false;

      yield return new WaitForSeconds(waitTime);

      // Enable collider when starting to fade in
      collider2D.enabled = true;

      // Fade in
      for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime * fadeSpeed)
      {
        SetAlpha(alpha);
        yield return null;
      }
    }
  }

  void SetAlpha(float alpha)
  {
    Color color = spriteRenderer.color;
    color.a = alpha;
    spriteRenderer.color = color;
  }
}

