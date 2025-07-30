using UnityEngine;
using TMPro;
using System.Collections;

public class ChestInteraction : MonoBehaviour
{
    [Header("UI")]
    public GameObject mensajeUI;

    [Header("Animator")]
    public Animator animatorCofre;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoAbrirCofre;
    public AudioClip sonidoMonedas;

    [Header("Monedas")]
    public ParticleSystem monedasParticulas;
    public TextMeshProUGUI textoMonedasHUD;
    public int monedasMin = 50;
    public int monedasMax = 100;

    private bool jugadorCerca = false;
    private bool cofreAbierto = false;
    private int monedasActuales = 0;

    public GameObject monedas3D;

    void Start()
    {
        mensajeUI.SetActive(false);

        if (textoMonedasHUD != null)
        {
            int valorParseado = 0;

            // Intenta convertir solo si el texto no está vacío
            if (!string.IsNullOrWhiteSpace(textoMonedasHUD.text) && int.TryParse(textoMonedasHUD.text, out valorParseado))
            {
                monedasActuales = valorParseado;
            }
            else
            {
                monedasActuales = 500; // Valor por defecto si está vacío o no es numérico
                textoMonedasHUD.text = "500"; // También puedes inicializarlo visualmente
            }
        }
    }


    void Update()
    {
        if (jugadorCerca && !cofreAbierto && Input.GetKeyDown(KeyCode.F))
        {
            AbrirCofre();
        }
    }

    void AbrirCofre()
    {
        cofreAbierto = true;
        mensajeUI.SetActive(false);

        // Animación
        animatorCofre.SetBool("isOpen", true);

        // Sonido abrir cofre
        if (audioSource && sonidoAbrirCofre)
            audioSource.PlayOneShot(sonidoAbrirCofre);

        // Espera a que termine animación para lanzar monedas
        StartCoroutine(IniciarRecompensa());
    }

    IEnumerator IniciarRecompensa()
    {
        yield return new WaitForSeconds(1.0f); // Esperar que termine la animación de apertura del cofre

        // Lanzar partículas de monedas
        if (monedasParticulas)
            monedasParticulas.Play();

        // Mostrar objeto 3D de monedas (opcional)
        if (monedas3D)
            monedas3D.SetActive(true);

        // Sonido monedas
        if (audioSource && sonidoMonedas)
            audioSource.PlayOneShot(sonidoMonedas);

        // Empezar a sumar monedas al HUD
        int recompensa = Random.Range(monedasMin, monedasMax + 1);
        yield return StartCoroutine(SumarMonedas(recompensa));

        // Esperar que termine el efecto de partículas
        yield return new WaitForSeconds(1.0f);

        // Detener partículas
        if (monedasParticulas)
            monedasParticulas.Stop();

        //  Desactivar el objeto 3D de monedas
        if (monedas3D)
            monedas3D.SetActive(false);
    }


    IEnumerator SumarMonedas(int cantidad)
    {
        int suma = 0;
        int delay = 5;
        while (suma < cantidad)
        {
            int incremento = Random.Range(1, 5);
            suma += incremento;
            monedasActuales += incremento;

            if (suma > cantidad)
            {
                monedasActuales -= (suma - cantidad); // Corrige exceso
                suma = cantidad;
            }

            textoMonedasHUD.text = monedasActuales.ToString();
            yield return new WaitForSeconds(0.05f); // Velocidad de conteo
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cofreAbierto)
        {
            jugadorCerca = true;
            mensajeUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            mensajeUI.SetActive(false);
        }
    }
}
