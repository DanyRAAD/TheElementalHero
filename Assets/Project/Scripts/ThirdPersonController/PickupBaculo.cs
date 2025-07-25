using UnityEngine;

public class BáculoAdherente : MonoBehaviour
{
    public string nombreMano = "mixamorig:RightHand";  // Nombre del hueso
    private bool adherido = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!adherido && other.CompareTag("Player"))  // Asegúrate de que el personaje tenga el tag "Player"
        {
            Transform mano = other.transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand");

            if (mano != null)
            {
                transform.SetParent(mano);  // Se pega a la mano
                transform.localPosition = new Vector3(-0.111f, 0.044f, 0.056f);
                transform.localRotation = Quaternion.Euler(-179.006f, -36.18201f, 56.951f);
                adherido = true;

                // Opcional: desactiva el collider para que ya no se vuelva a activar
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
