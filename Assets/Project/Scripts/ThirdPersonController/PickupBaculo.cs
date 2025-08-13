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

    
    public bool GetEstadoAdherido()
    {
        return adherido;
    }

   
    public void SetEstadoAdherido(bool estado)
    {
        adherido = estado;

        if (adherido)
        {
            
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                PegarABrazo(player.transform);
            }
        }
        else
        {
            
            transform.SetParent(null);
            GetComponent<Collider>().enabled = true;
        }
    }
}
