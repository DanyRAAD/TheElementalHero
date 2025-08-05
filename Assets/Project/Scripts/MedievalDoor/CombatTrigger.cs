using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    public GameObject puerta; 
    public Animator animPuerta; 

    private bool yaCerro = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaCerro)
        {
            if (animPuerta != null)
            {
                animPuerta.SetTrigger("Cerrar"); 
            }
            else if (puerta != null)
            {
                puerta.SetActive(false);
            }

            yaCerro = true;
            gameObject.SetActive(false); 
        }
    }
}
