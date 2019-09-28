using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject objectToPool;
    [SerializeField] int poolMax;
    [SerializeField] bool shouldExpand = false;

    Vector3 originalRotation;

    List<GameObject> objects;
    void Start()
    {
        originalRotation = this.transform.eulerAngles;
        objects = new List<GameObject>();
        for(int i = 0; i < poolMax; ++i)
        {
            GameObject g = Instantiate(objectToPool, this.transform);
            g.SetActive(false);
            objects.Add(g);
        }
    }

    public GameObject GetUnusedObject()
    {
        foreach(GameObject g in objects)
        {
            if(!g.activeInHierarchy)
            {
                g.transform.SetParent(this.transform);
                return g;
            }
        }
        if(shouldExpand)
        {
            GameObject obj = Instantiate(objectToPool, this.transform);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void Straighten()
    {
        this.transform.eulerAngles = Vector3.zero;
    }

    public void Revert()
    {
        this.transform.eulerAngles = originalRotation;
    }
}
