using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadGameUIManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject slotPrefab;

    // Simulación de partidas guardadas
    private List<string> partidasSimuladas = new List<string> {
        "Partida 1", "Partida 2", "Partida 3", "Partida 4", "Partida 5"
    };

    void Start()
    {
        MostrarPartidasSimuladas();
    }

    void MostrarPartidasSimuladas()
    {
        foreach (Transform hijo in contentPanel)
        {
            Destroy(hijo.gameObject); // Limpia anteriores
        }

        foreach (string nombre in partidasSimuladas)
        {
            GameObject slot = Instantiate(slotPrefab, contentPanel);
            slot.transform.Find("SlotGameText").GetComponent<TMP_Text>().text = nombre;

            Button btnCargar = slot.transform.Find("LoadButton").GetComponent<Button>();
            Button btnEliminar = slot.transform.Find("DeleteButton").GetComponent<Button>();

            btnCargar.onClick.AddListener(() => {
                Debug.Log("Cargar partida: " + nombre);
            });

            btnEliminar.onClick.AddListener(() => {
                Debug.Log("Eliminar partida: " + nombre);
                partidasSimuladas.Remove(nombre);
                MostrarPartidasSimuladas();
            });
        }
    }
}
