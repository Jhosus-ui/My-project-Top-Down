using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextHoverClickSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;  // Sonido cuando el cursor pasa por encima
    public AudioClip clickSound;  // Sonido de clic
    private AudioSource audioSource;

    void Start()
    {
        // Obtener el componente AudioSource del objeto
        audioSource = GetComponent<AudioSource>();
    }

    // Detectar cuando el cursor pasa por encima del texto (hover)
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Reproducir el sonido de hover
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // Detectar el clic en el texto
    public void OnPointerClick(PointerEventData eventData)
    {
        // Reproducir el sonido de clic
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
