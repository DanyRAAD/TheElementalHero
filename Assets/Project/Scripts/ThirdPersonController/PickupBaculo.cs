using UnityEngine;

public class BáculoAdherente : MonoBehaviour
{
    public string nombreMano = "mixamorig:RightHand";  
    private bool adherido = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!adherido && other.CompareTag("Player"))  
        {
            Transform mano = other.transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand");

            if (mano != null)
            {
                transform.SetParent(mano);  
                transform.localPosition = new Vector3(-0.111f, 0.044f, 0.056f);
                transform.localRotation = Quaternion.Euler(-179.006f, -36.18201f, 56.951f);
                adherido = true;

                
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
