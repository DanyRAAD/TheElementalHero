using System.Collections;
using TMPro;
using UnityEngine;

public class InventarioLlave : MonoBehaviour
{
    public TextMeshProUGUI textoLlaveObtenida;
    public TextMeshProUGUI textoNecesitasLlave;
    public TextMeshProUGUI textoPresionaF;

    public Animator animPuerta;
    public Collider colliderPuerta;

    private bool tieneLlave = false;
    private bool cercaPuerta = false;
    private bool puertaAbierta = false;  

    void Start()
    {
        textoLlaveObtenida.gameObject.SetActive(false);
        textoNecesitasLlave.gameObject.SetActive(false);
        textoPresionaF.gameObject.SetActive(false);
    }

    void Update()
    {
        if (cercaPuerta && tieneLlave && Input.GetKeyDown(KeyCode.F))
        {
            AbrirPuerta();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Llave"))
        {
            tieneLlave = true;

            StartCoroutine(MostrarTextoTemporal(textoLlaveObtenida, 3f));

            textoNecesitasLlave.gameObject.SetActive(false);
            textoPresionaF.gameObject.SetActive(false);

            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Puerta"))
        {
            cercaPuerta = true;

            textoLlaveObtenida.gameObject.SetActive(false);

            if (puertaAbierta)
            {
                
                textoNecesitasLlave.gameObject.SetActive(false);
                textoPresionaF.gameObject.SetActive(false);
            }
            else if (tieneLlave)
            {
                textoPresionaF.gameObject.SetActive(true);
                textoNecesitasLlave.gameObject.SetActive(false);
            }
            else
            {
                textoNecesitasLlave.gameObject.SetActive(true);
                textoPresionaF.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Puerta"))
        {
            cercaPuerta = false;

            textoLlaveObtenida.gameObject.SetActive(false);
            textoNecesitasLlave.gameObject.SetActive(false);
            textoPresionaF.gameObject.SetActive(false);
        }
    }

    void AbrirPuerta()
    {
        textoLlaveObtenida.gameObject.SetActive(false);
        textoNecesitasLlave.gameObject.SetActive(false);
        textoPresionaF.gameObject.SetActive(false);

        tieneLlave = false;
        puertaAbierta = true;  

        if (animPuerta != null)
        {
            animPuerta.SetTrigger("IsOpen");
        }

        if (colliderPuerta != null)
        {
            colliderPuerta.enabled = false;
        }
    }

    IEnumerator MostrarTextoTemporal(TextMeshProUGUI texto, float duracion)
    {
        texto.gameObject.SetActive(true);
        yield return new WaitForSeconds(duracion);
        texto.gameObject.SetActive(false);
    }
}
