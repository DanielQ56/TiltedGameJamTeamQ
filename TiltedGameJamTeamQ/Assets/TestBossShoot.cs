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
        float revs = 0f;
        angle = 0f;
        isShooting[i] = true;
        float radius = pool[i].GetRadius();
        float angleInBetween = pool[i].getAngleInBetween();
        float spawnLag = pool[i].getSpawnLag();

        while (angle < 360f && revs != 5f)
        {
            float rad = Mathf.Deg2Rad * angle;
            GameObject b = pool[i].GetUnusedObject(); //pool
            b.SetActive(true); //pool
            BulletMovement b2 = b.GetComponent<BulletMovement>();

            b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * radius;
            b2.FireOff(pool[i].transform.localPosition, pool[i].GetBulletSpeed());

            float rad2 = Mathf.Deg2Rad * angle + Mathf.PI;
            GameObject b3 = pool[i].GetUnusedObject(); //pool
            b3.SetActive(true); //pool
            BulletMovement b4 = b3.GetComponent<BulletMovement>();

            b3.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad2), Mathf.Sin(rad2))) * radius;
            b4.FireOff(pool[i].transform.localPosition, pool[i].GetBulletSpeed());

            yield return new WaitForSeconds(spawnLag);
            angle += angleInBetween;
            revs += 1f;

        }
        isShooting[i] = false;
    }


}
