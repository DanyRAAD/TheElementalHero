using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Para manejar el texto en pantalla

public class PalancaInteraction : MonoBehaviour
{
    public GameObject textoInteraccionUI;
    public Animator animadorPalanca;
    public Animator animadorPuerta;

    private bool jugadorCerca = false;
    private bool yaActivada = false;

    void Update()
    {
        if (jugadorCerca && !yaActivada && Input.GetKeyDown(KeyCode.F))
        {
            ActivarPalanca();
        }
    }

    void ActivarPalanca()
    {
        yaActivada = true;
        textoInteraccionUI.SetActive(false);
        animadorPalanca.SetTrigger("Activar");

        // Esperamos que la animación de la palanca termine para abrir la puerta
        StartCoroutine(EsperarYActivarPuerta());
    }

    IEnumerator EsperarYActivarPuerta()
    {
        // Espera la duración exacta de la animación de la palanca
        yield return new WaitForSeconds(animadorPalanca.GetCurrentAnimatorStateInfo(0).length);

        animadorPuerta.SetTrigger("Abrir");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaActivada)
        {
            textoInteraccionUI.SetActive(true);
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textoInteraccionUI.SetActive(false);
            jugadorCerca = false;
        }
    }
}
