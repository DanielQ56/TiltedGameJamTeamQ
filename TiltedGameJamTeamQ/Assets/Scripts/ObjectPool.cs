using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] ScriptableBullet objectToPool;
    [SerializeField] int poolMax;
    [SerializeField] bool shouldExpand = false;
    [SerializeField] AudioSource source;

    Vector3 originalRotation;

    List<GameObject> objects;
    void Start()
    {
        originalRotation = this.transform.eulerAngles;
        objects = new List<GameObject>();
        for(int i = 0; i < poolMax; ++i)
        {
            GameObject g = Instantiate(objectToPool.bulletPrefab, this.transform);
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
            GameObject obj = Instantiate(objectToPool.bulletPrefab, this.transform);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public float GetRadius()
    {
        return objectToPool.radius;
    }

    public float getSpawnLag()
    {
        return objectToPool.spawnLag;
    }

    public float getAngleInBetween()
    {
        return objectToPool.angleInBetween;
    }

    public float GetBulletSpeed()
    {
        return objectToPool.bulletSpeed;
    }

    public void Straighten()
    {
        this.transform.eulerAngles = Vector3.zero;
    }

    public void Revert()
    {
        this.transform.eulerAngles = originalRotation;
    }

    public string GetLayer()
    {
        return objectToPool.layer;
    }

    public void PlayClip()
    {
        source.Play();
    }
}
