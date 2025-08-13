using UnityEngine;
using TMPro;
using System.Collections;

public class PotionInventory : MonoBehaviour
{
    [Header("HUD Contadores")]
    public TextMeshProUGUI contadorPocionesVida;
    public TextMeshProUGUI contadorPocionesEscudo;

    [Header("Mensajes de advertencia (TextMeshProUGUI)")]
    public TextMeshProUGUI textoVidaMaximo;
    public TextMeshProUGUI textoEscudoMaximo;
    public TextMeshProUGUI textoSinPocionesVida;
    public TextMeshProUGUI textoSinPocionesEscudo;

    [Header("Referencia a PlayerHealth")]
    public PlayerHealth playerHealth;

    public float duracionMensaje = 2f;

    private int pocionesVida = 0;
    private int pocionesEscudo = 0;

    void Start()
    {
        ActualizarHUD();

        
        textoVidaMaximo.gameObject.SetActive(false);
        textoEscudoMaximo.gameObject.SetActive(false);
        textoSinPocionesVida.gameObject.SetActive(false);
        textoSinPocionesEscudo.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UsarPocionVida();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UsarPocionEscudo();
        }
    }

    public void AgregarPocionVida()
    {
        pocionesVida++;
        ActualizarHUD();
    }

    public void AgregarPocionEscudo()
    {
        pocionesEscudo++;
        ActualizarHUD();
    }

    public void UsarPocionVida()
    {
        if (pocionesVida > 0)
        {
            if (playerHealth.currentHealth >= playerHealth.maxHealth)
            {
                MostrarMensaje(textoVidaMaximo);
                return;
            }
            playerHealth.HealHealth(50);
            pocionesVida--;
            ActualizarHUD();
        }
        else
        {
            MostrarMensaje(textoSinPocionesVida);
        }
    }

    public void UsarPocionEscudo()
    {
        if (pocionesEscudo > 0)
        {
            if (playerHealth.currentShield >= playerHealth.maxShield)
            {
                MostrarMensaje(textoEscudoMaximo);
                return;
            }
            playerHealth.HealShield(50);
            pocionesEscudo--;
            ActualizarHUD();
        }
        else
        {
            MostrarMensaje(textoSinPocionesEscudo);
        }
    }

    void ActualizarHUD()
    {
        if (contadorPocionesVida != null)
            contadorPocionesVida.text = pocionesVida.ToString();

        if (contadorPocionesEscudo != null)
            contadorPocionesEscudo.text = pocionesEscudo.ToString();
    }

    void MostrarMensaje(TextMeshProUGUI texto)
    {
        
        textoVidaMaximo.gameObject.SetActive(false);
        textoEscudoMaximo.gameObject.SetActive(false);
        textoSinPocionesVida.gameObject.SetActive(false);
        textoSinPocionesEscudo.gameObject.SetActive(false);

        if (texto != null)
        {
            texto.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(DesactivarMensajeDespues(texto));
        }
    }

    IEnumerator DesactivarMensajeDespues(TextMeshProUGUI texto)
    {
        yield return new WaitForSeconds(duracionMensaje);
        texto.gameObject.SetActive(false);
    }

    public int GetPocionesVida()
    {
        return pocionesVida;
    }

    public int GetPocionesEscudo()
    {
        return pocionesEscudo;
    }

    public void SetPocionesVida(int cantidad)
    {
        pocionesVida = cantidad;
        ActualizarHUD();
    }

    public void SetPocionesEscudo(int cantidad)
    {
        pocionesEscudo = cantidad;
        ActualizarHUD();
    }
}
