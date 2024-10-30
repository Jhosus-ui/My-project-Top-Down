using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FadeOutAfterTime : MonoBehaviour
{
    public float delay = 10f; 
    public float fadeDuration = 2f;
    private SpriteRenderer spriteRenderer;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (spriteRenderer != null || canvasGroup != null)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            Debug.LogError("El objeto no tiene un SpriteRenderer ni un CanvasGroup.");
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;

        // Si es un SpriteRenderer
        if (spriteRenderer != null)
        {
            Color initialColor = spriteRenderer.color;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
                yield return null;
            }
        }

        // Si es un CanvasGroup (para UI)
        if (canvasGroup != null)
        {
            float initialAlpha = canvasGroup.alpha;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(initialAlpha, 0f, elapsedTime / fadeDuration);
                yield return null;
            }
        }
    }
}

