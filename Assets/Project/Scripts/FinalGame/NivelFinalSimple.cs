using UnityEngine;

public class NivelFinalSimple : MonoBehaviour
{
    [Header("UI")]
    public GameObject mensajeProximoNivel;  

    private void Start()
    {
        if (mensajeProximoNivel != null)
            mensajeProximoNivel.SetActive(false);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mensajeProximoNivel != null)
                mensajeProximoNivel.SetActive(true);  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mensajeProximoNivel != null)
                mensajeProximoNivel.SetActive(false);  
        }
    }
}
