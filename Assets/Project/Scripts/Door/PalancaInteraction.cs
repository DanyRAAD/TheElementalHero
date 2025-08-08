using UnityEngine;

public class PalancaInteraction : MonoBehaviour
{
    public GameObject textoInteraccionUI;   
    public Animator animadorPalanca;
    public Animator animadorPuerta;

    private bool jugadorCerca = false;
    private bool yaActivada = false;

    void Start()
    {
        if (textoInteraccionUI != null)
            textoInteraccionUI.SetActive(false);  
    }

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
        StartCoroutine(EsperarYActivarPuerta());
    }

    System.Collections.IEnumerator EsperarYActivarPuerta()
    {
        float duracionAnimacion = animadorPalanca.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duracionAnimacion + 0.1f);
        animadorPuerta.SetTrigger("Abrir");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (textoInteraccionUI != null)
                textoInteraccionUI.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (textoInteraccionUI != null && !textoInteraccionUI.activeSelf)
                textoInteraccionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (textoInteraccionUI != null)
                textoInteraccionUI.SetActive(false);
        }
    }
}
