using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [Header("Economía")]
    public PlayerEconomy economy;

    [Header("Inventario de pociones")]
    public PotionInventory potionInventory;

    [Header("Precios")]
    public int precioPocionVida = 500;
    public int precioPocionEscudo = 550;

    [Header("Botones")]
    public Button botonPocionVida;
    public Button botonPocionEscudo;

    [Header("UI Tienda")]
    public GameObject panelTienda;

    private bool tiendaAbierta = false;

    void Start()
    {
        panelTienda.SetActive(false);
        BloquearMouse();
        Time.timeScale = 1f;  // Aseguramos que el tiempo esté corriendo al iniciar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            tiendaAbierta = !tiendaAbierta;
            panelTienda.SetActive(tiendaAbierta);

            if (tiendaAbierta)
            {
                ActivarMouse();
                PausarJuego();
            }
            else
            {
                BloquearMouse();
                ReanudarJuego();
            }
        }

        botonPocionVida.interactable = economy.monedas >= precioPocionVida;
        botonPocionEscudo.interactable = economy.monedas >= precioPocionEscudo;
    }

    public void ComprarPocionVida()
    {
        if (economy.GastarMonedas(precioPocionVida))
        {
            potionInventory.AgregarPocionVida();
        }
    }

    public void ComprarPocionEscudo()
    {
        if (economy.GastarMonedas(precioPocionEscudo))
        {
            potionInventory.AgregarPocionEscudo();
        }
    }

    void ActivarMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void BloquearMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void PausarJuego()
    {
        Time.timeScale = 0f;
    }

    void ReanudarJuego()
    {
        Time.timeScale = 1f;
    }
}
