using UnityEngine;
using UnityEngine.UI;

public class BrilloManager : MonoBehaviour
{
    [Range(0f, 1f)]
    public float maxOscurecimiento = 0.6f;

    [SerializeField] private Image brilloOverlay;
    [SerializeField] private Slider slider;

    private float brillo;

    private const float BRILLO_PREDETERMINADO = 0.6f; // 60% de claridad (40% de oscurecimiento)
    private const string BRILLO_KEY = "brillo";

    void Start()
    {
        brillo = PlayerPrefs.GetFloat(BRILLO_KEY, BRILLO_PREDETERMINADO);
        AplicarBrillo();

        if (slider != null)
        {
            slider.value = brillo;
        }
    }

    public void CambiarBrillo(float valor)
    {
        brillo = valor;
        AplicarBrillo();
    }

    public void GuardarBrillo()
    {
        PlayerPrefs.SetFloat(BRILLO_KEY, brillo);
        PlayerPrefs.Save();
    }

    public void RestablecerBrillo()
    {
        brillo = BRILLO_PREDETERMINADO;
        AplicarBrillo();

        if (slider != null)
        {
            slider.value = brillo;
        }

        GuardarBrillo();
    }

    private void AplicarBrillo()
    {
        if (brilloOverlay != null)
        {
            Color color = brilloOverlay.color;
            color.a = (1f - brillo) * maxOscurecimiento;
            brilloOverlay.color = color;
        }
    }
}

