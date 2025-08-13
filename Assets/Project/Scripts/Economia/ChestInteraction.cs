using UnityEngine;
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

    [Header("Recompensa")]
    public ParticleSystem monedasParticulas;
    public int monedasMinComun = 50;
    public int monedasMaxComun = 100;
    public int monedasMinSupremo = 100;
    public int monedasMaxSupremo = 300;
    public GameObject monedas3D;

    [Header("Economía")]
    public PlayerEconomy economy;

    private bool jugadorCerca = false;
    public bool cofreAbierto = false;

    public string chestID; 


    void Start()
    {
        mensajeUI.SetActive(false);
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

        animatorCofre.SetBool("isOpen", true);

        if (audioSource && sonidoAbrirCofre)
            audioSource.PlayOneShot(sonidoAbrirCofre);

        StartCoroutine(IniciarRecompensa());
    }

    IEnumerator IniciarRecompensa()
    {
        yield return new WaitForSeconds(1.0f);

        if (monedasParticulas) monedasParticulas.Play();
        if (monedas3D) monedas3D.SetActive(true);
        if (audioSource && sonidoMonedas) audioSource.PlayOneShot(sonidoMonedas);

        int recompensa = 0;

        // Verificar tag para asignar recompensa
        if (gameObject.CompareTag("CofreComun"))
        {
            recompensa = Random.Range(monedasMinComun, monedasMaxComun + 1);
        }
        else if (gameObject.CompareTag("CofreSupremo"))
        {
            recompensa = Random.Range(monedasMinSupremo, monedasMaxSupremo + 1);
        }
        else
        {
            // Si no tiene tag, le puedes asignar valor por defecto o 0
            recompensa = monedasMinComun;
        }

        economy.AgregarMonedas(recompensa);

        yield return new WaitForSeconds(1.0f);

        if (monedasParticulas) monedasParticulas.Stop();
        if (monedas3D) monedas3D.SetActive(false);
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
    public void SetCofreEstado(bool abierto)
    {
        cofreAbierto = abierto;
        animatorCofre.SetBool("isOpen", abierto);
        mensajeUI.SetActive(!abierto);
    }

}
