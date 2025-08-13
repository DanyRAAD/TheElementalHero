using UnityEngine;

public class CheckDestructibles : MonoBehaviour
{
    public DestructibleObject[] destructibles;

    
    public bool QuedanObjetos()
    {
        foreach (DestructibleObject d in destructibles)
        {
            if (d != null && d.gameObject.activeInHierarchy)
                return true;
        }
        return false;
    }
}
