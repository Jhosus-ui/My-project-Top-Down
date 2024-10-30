using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FadeOutAfterTime : MonoBehaviour
{
    public float delay = 10f; // Tiempo en segundos antes de comenzar a bajar la opacidad
    public float fadeDuration = 2f; // Duración del desvanecimiento
    private SpriteRenderer spriteRenderer;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        // Intentar obtener el SpriteRenderer (para objetos 2D)
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Si el objeto es parte de la UI, intentamos obtener un CanvasGroup
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
        // Esperar el tiempo antes de empezar a bajar la opacidad
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

