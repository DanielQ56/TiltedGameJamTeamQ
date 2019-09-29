using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossShoot : MonoBehaviour
{
    [SerializeField] List<WaitTimes> listOfTimes;
    [SerializeField] List<ObjectPool> pool;


    float angle = 0f;

    float timer = 0;

    delegate void Attacks(int index);

    List<bool> isShooting;
    List<float> timers;

    List<Attacks> listOfAttacks;

    // Start is called before the first frame update
    void Start()
    {
        timers = new List<float>();
        isShooting = new List<bool>();
        listOfAttacks = new List<Attacks>();
        listOfAttacks.Add(One);
        foreach (Attacks a in listOfAttacks)
        {
            isShooting.Add(false);
            timers.Add(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        float time = Time.deltaTime;
        for (int i = 0; i < GameDetails.instance.GetCurrentPhase(); ++i)
        {
            if (timers[i] <= 0)
            {
                if (!isShooting[i])
                {
                    listOfAttacks[i](i);
                }
                else
                {
                    WaitTimes t = listOfTimes[i];
                    timers[i] = (t.useConstantTime ? t.ConstantTime : Random.Range(t.minWaitTime, t.maxWaitTime));
                }
            }
            else
            {
                timers[i] -= time;
            }

        }
    }

    void One(int index)
    {
        StartCoroutine(SpawnBullets(index));
    }

    IEnumerator SpawnBullets(int i)
    {
        float timer = 0f;
        List<GameObject> movement = new List<GameObject>();
        isShooting[i] = true;
        float radius = pool[i].GetRadius();
        float angleInBetween = pool[i].getAngleInBetween();
        float spawnLag = pool[i].getSpawnLag();
        angle = 170f;
        float bulletSpace = 0f;

        while (timer < 10f)
        {
            Vector3 pos = pool[i].transform.localPosition + (Vector3.down * radius);

            while (bulletSpace < 5f)
            {
                GameObject b = pool[i].GetUnusedObject();
                b.SetActive(true);
                //b.GetComponent<SpriteRenderer>().enabled = false;
                
                b.transform.localPosition = new Vector3(pos.x + (0.1f * bulletSpace), pos.y, pos.z);
                movement.Add(b);
                yield return new WaitForSeconds(spawnLag);
                angle += angleInBetween;
                bulletSpace += 1f;
            }

            foreach (GameObject bullet in movement)
            {
                bullet.GetComponent<SpriteRenderer>().enabled = true;
                //bullet.GetComponent<BulletMovement>().FireOff(bullet.transform.localPosition + Vector3.up, pool[i].GetBulletSpeed());
            }
            movement = new List<GameObject>();
        }
        Debug.Log("bye");
        isShooting[i] = false;
    }


}
