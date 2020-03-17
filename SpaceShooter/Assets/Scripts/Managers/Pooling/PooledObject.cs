using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObject
{
    public ObjectPool m_OwningPool = null;
    public GameObject gameObject = null;

    public void Activate()
    {
        if (gameObject)
        {
            gameObject.transform.root.gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        if (gameObject)
        {
            gameObject.transform.root.gameObject.SetActive(false);
        }
    }
}
