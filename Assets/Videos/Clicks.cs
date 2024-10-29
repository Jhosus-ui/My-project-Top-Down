using System.Collections;
using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class ShowTextMeshProAfterTime : MonoBehaviour
{
    public float delay = 12f; // Tiempo en segundos antes de que aparezca el texto
    private TextMeshProUGUI tmpText; // Componente de TextMeshProUGUI

    private void Start()
    {
        // Obtener el componente TextMeshProUGUI del objeto
        tmpText = GetComponent<TextMeshProUGUI>();

        if (tmpText != null)
        {
            // Hacer invisible el texto al principio
            tmpText.enabled = false;

            // Iniciar la rutina para mostrar el texto después del tiempo de delay
            StartCoroutine(ShowText());
        }
        else
        {
            Debug.LogError("Este objeto no tiene un componente TextMeshProUGUI.");
        }
    }

    IEnumerator ShowText()
    {
        // Esperar el tiempo de delay
        yield return new WaitForSeconds(delay);

        // Mostrar el texto
        tmpText.enabled = true;
    }
}
