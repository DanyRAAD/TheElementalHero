using UnityEngine;

public class BaculoAdherente : MonoBehaviour
{
    public string nombreMano = "mixamorig:RightHand";
    private bool adherido = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!adherido && other.CompareTag("Player"))
        {
            PegarABrazo(other.transform);
        }
    }

    // Método para pegar el báculo a la mano (o brazo)
    private void PegarABrazo(Transform playerTransform)
    {
        Transform mano = playerTransform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand");

        if (mano != null)
        {
            transform.SetParent(mano);
            transform.localPosition = new Vector3(-0.111f, 0.044f, 0.056f);
            transform.localRotation = Quaternion.Euler(-179.006f, -36.18201f, 56.951f);
            adherido = true;

            GetComponent<Collider>().enabled = false;
        }
    }

    // Método para obtener el estado actual (para guardar)
    public bool GetEstadoAdherido()
    {
        return adherido;
    }

    // Método para restaurar el estado al cargar
    public void SetEstadoAdherido(bool estado)
    {
        adherido = estado;

        if (adherido)
        {
            // Buscamos el jugador y pegamos el báculo a la mano
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                PegarABrazo(player.transform);
            }
        }
        else
        {
            // Si no está adherido, soltamos el báculo
            transform.SetParent(null);
            GetComponent<Collider>().enabled = true;
        }
    }
}
