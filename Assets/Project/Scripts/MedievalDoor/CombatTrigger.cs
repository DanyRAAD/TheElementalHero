using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    public GameObject puerta; // Asigna aquí la puerta que se cerrará
    public Animator animPuerta; // Asigna el Animator si tiene animación de cierre

    private bool yaCerro = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaCerro)
        {
            if (animPuerta != null)
            {
                animPuerta.SetTrigger("Cerrar"); // Asegúrate de tener un trigger "Cerrar" en el Animator
            }
            else if (puerta != null)
            {
                puerta.SetActive(false); // Alternativa simple sin animación
            }

            yaCerro = true;
            gameObject.SetActive(false); // Desactiva este trigger para no volver a activarse
        }
    }
}
