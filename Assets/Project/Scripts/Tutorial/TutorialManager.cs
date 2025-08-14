using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Textos Tutorial en Panel")]
    public TextMeshProUGUI textoMovimiento;
    public TextMeshProUGUI textoSaltoAgacharse;
    public TextMeshProUGUI textoBaculo;
    public TextMeshProUGUI textoDispararMagia;
    public TextMeshProUGUI textoCofre;
    public TextMeshProUGUI textoTiendaAbrir;
    public TextMeshProUGUI textoTiendaCerrar;
    public TextMeshProUGUI textoPociones;
    public TextMeshProUGUI textoTutorialCompletado;

    private void Start()
    {
        
        textoMovimiento.gameObject.SetActive(false);
        textoSaltoAgacharse.gameObject.SetActive(false);
        textoBaculo.gameObject.SetActive(false);
        textoDispararMagia.gameObject.SetActive(false);
        textoCofre.gameObject.SetActive(false);
        textoTiendaAbrir.gameObject.SetActive(false);
        textoTiendaCerrar.gameObject.SetActive(false);
        textoPociones.gameObject.SetActive(false);
        textoTutorialCompletado.gameObject.SetActive(false);

        StartCoroutine(MostrarTutorial());
    }

    private IEnumerator MostrarTutorial()
    {
        // Paso 1: WASD
        textoMovimiento.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoMovimiento.gameObject.SetActive(false);

        // Paso 2: Saltar y agacharse
        textoSaltoAgacharse.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        textoSaltoAgacharse.gameObject.SetActive(false);

        // Paso 6: Abrir tienda
        textoTiendaAbrir.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        textoTiendaAbrir.gameObject.SetActive(false);

        // Paso 7: Cerrar tienda
        textoTiendaCerrar.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        textoTiendaCerrar.gameObject.SetActive(false);

        // Paso 8: Uso de pociones
        textoPociones.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        textoPociones.gameObject.SetActive(false);

        //// Paso 3: Dirigirse al báculo
        //textoBaculo.gameObject.SetActive(true);
        //yield return new WaitForSeconds(15f);
        //textoBaculo.gameObject.SetActive(false);

        //// Paso 4: Disparar magia
        //textoDispararMagia.gameObject.SetActive(true);
        //yield return new WaitForSeconds(6f);
        //textoDispararMagia.gameObject.SetActive(false);

        //// Paso 5: Ir al cofre
        //textoCofre.gameObject.SetActive(true);
        //yield return new WaitForSeconds(10f);
        //textoCofre.gameObject.SetActive(false);

        

        
    }
}
