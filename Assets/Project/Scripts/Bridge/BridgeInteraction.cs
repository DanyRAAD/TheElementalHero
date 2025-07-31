using UnityEngine;
using TMPro;
using System.Collections; // Necesario para usar IEnumerator

public class BridgeInteraction : MonoBehaviour
{
    [Header("UI Textos con TextMeshPro")]
    public GameObject uiTextoContainer;
    public TextMeshProUGUI textoRecoger;
    public TextMeshProUGUI textoColocar;
    public TextMeshProUGUI textoNecesitasPieza;
    public TextMeshProUGUI uiContadorPiezas;

    [Header("Animación")]
    private Animator playerAnimator;
    public string triggerRecolectarPieza = "RecolectarPieza";
    public string triggerColocarPieza = "RecolectarPieza";

    [Header("Colocación")]
    public GameObject piezaPrefab;
    public string tagPieza = "PiezaPuente";
    public string tagHueco = "BridgeSlot";

    private int piezasRecolectadas = 0;
    private GameObject piezaActual = null;
    private GameObject huecoActual = null;

    private string textoBase; 

    void Awake()
    {
        if (playerAnimator == null)
            playerAnimator = GetComponent<Animator>();

        
        if (uiContadorPiezas != null)
            textoBase = uiContadorPiezas.text;
    }

    void Start()
    {
        ActualizarTextoPiezas(); 
    }

    void Update()
    {
        if (piezaActual != null && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(RecolectarPiezaConAnimacion());
        }

        if (huecoActual != null && piezasRecolectadas > 0 && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ColocarPiezaConAnimacion());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagPieza))
        {
            piezaActual = other.gameObject;
            MostrarMensaje("recoger");
        }
        else if (other.CompareTag(tagHueco))
        {
            huecoActual = other.gameObject;
            if (piezasRecolectadas > 0)
                MostrarMensaje("colocar");
            else
                MostrarMensaje("necesitas");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagPieza)) piezaActual = null;
        if (other.CompareTag(tagHueco)) huecoActual = null;

        OcultarMensajes();
    }

    IEnumerator RecolectarPiezaConAnimacion()
    {
        OcultarMensajes();

        if (playerAnimator != null)
        {
            playerAnimator.speed = 5.0f; // Velocidad aumentada 
            playerAnimator.SetTrigger(triggerRecolectarPieza);
        }


        yield return new WaitForSeconds(0.9f / playerAnimator.speed);

        if (piezaActual != null)
        {
            piezaActual.SetActive(false);
            piezaActual = null;

            piezasRecolectadas++;
            ActualizarTextoPiezas();
        }
        // Restablecer la velocidad al terminar
        if (playerAnimator != null)
            playerAnimator.speed = 1.0f;
    }

    IEnumerator ColocarPiezaConAnimacion()
    {
        OcultarMensajes();

        if (playerAnimator != null && triggerColocarPieza != "")
            playerAnimator.SetTrigger(triggerColocarPieza);

        yield return new WaitForSeconds(0.9f); 

        if (huecoActual != null)
        {
            Instantiate(piezaPrefab, huecoActual.transform.position, huecoActual.transform.rotation);
            Destroy(huecoActual);
            huecoActual = null;

            piezasRecolectadas--;
            ActualizarTextoPiezas();
        }
    }

    
    void ActualizarTextoPiezas()
    {
        if (uiContadorPiezas == null || string.IsNullOrEmpty(textoBase))
            return;

        
        string[] partes = textoBase.Split(':');

        if (partes.Length > 1)
            uiContadorPiezas.text = partes[0] + ": " + piezasRecolectadas;
        else
            uiContadorPiezas.text = textoBase + " " + piezasRecolectadas;

        
        uiContadorPiezas.gameObject.SetActive(piezasRecolectadas > 0);
    }

    void MostrarMensaje(string tipo)
    {
        if (uiTextoContainer != null) uiTextoContainer.SetActive(true);

        textoRecoger.gameObject.SetActive(tipo == "recoger");
        textoColocar.gameObject.SetActive(tipo == "colocar");
        textoNecesitasPieza.gameObject.SetActive(tipo == "necesitas");
    }

    void OcultarMensajes()
    {
        if (uiTextoContainer != null) uiTextoContainer.SetActive(false);

        textoRecoger.gameObject.SetActive(false);
        textoColocar.gameObject.SetActive(false);
        textoNecesitasPieza.gameObject.SetActive(false);
    }
}
