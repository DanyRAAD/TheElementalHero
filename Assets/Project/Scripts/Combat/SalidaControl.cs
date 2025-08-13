using UnityEngine;

public class SalidaControl : MonoBehaviour
{
    public CheckDestructibles checkDestructibles;      
    public GameObject mensajeNecesitasDestruir;        
    public Collider puertaCollider;                     
    private bool jugadorCerca = false;

    void Start()
    {
        if (mensajeNecesitasDestruir != null)
            mensajeNecesitasDestruir.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            RevisarEstado();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (mensajeNecesitasDestruir != null)
                mensajeNecesitasDestruir.SetActive(false);
        }
    }

    void Update()
    {
        if (jugadorCerca)
            RevisarEstado();
    }

    void RevisarEstado()
    {
        if (checkDestructibles.QuedanObjetos())
        {
            if (puertaCollider != null)
                puertaCollider.enabled = true;  // Bloquea paso

            if (mensajeNecesitasDestruir != null)
                mensajeNecesitasDestruir.SetActive(true); // Muestra mensaje
        }
        else
        {
            if (puertaCollider != null)
                puertaCollider.enabled = false; // Desbloquea paso

            if (mensajeNecesitasDestruir != null)
                mensajeNecesitasDestruir.SetActive(false); // Oculta mensaje
        }
    }
}
