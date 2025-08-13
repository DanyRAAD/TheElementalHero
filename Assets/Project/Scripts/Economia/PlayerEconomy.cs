using UnityEngine;
using TMPro;

public class PlayerEconomy : MonoBehaviour
{
    public int monedas = 500;
    public TextMeshProUGUI textoMonedas;

    void Start()
    {
        ActualizarHUD();
    }

    public bool GastarMonedas(int cantidad)
    {
        if (monedas >= cantidad)
        {
            monedas -= cantidad;
            ActualizarHUD();
            return true;
        }
        return false;
    }

    public void AgregarMonedas(int cantidad)
    {
        monedas += cantidad;
        ActualizarHUD();
    }

    void ActualizarHUD()
    {
        if (textoMonedas)
            textoMonedas.text = monedas.ToString();
    }
    public int GetMonedas()
    {
        return monedas;
    }

    public void SetMonedas(int cantidad)
    {
        monedas = cantidad;
        ActualizarHUD();
    }
}
