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
        listOfAttacks.Add(SpawnBullets);
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

    IEnumerator SpawnBullets(int i)
    {
        float revs = 0f;
        angle = 0f;
        isShooting[i] = true;
        float radius = pool[i].GetRadius();
        float angleInBetween = pool[i].getAngleInBetween();
        float spawnLag = pool[i].getSpawnLag();
        while (angle < 360f && revs != 3f)
        {
            float rad = Mathf.Deg2Rad * angle;
            GameObject b = pool[i].GetUnusedObject(); //pool
            b.SetActive(true); //pool
            BulletMovement b2 = b.GetComponent<BulletMovement>();

            b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * radius;
            b2.FireOff(pool[i].transform.localPosition, pool[i].GetBulletSpeed());

            yield return new WaitForSeconds(spawnLag);
            angle += angleInBetween;

            if (angle >= 360)
            {
                angle = 0f + (revs * 30);
                revs++;
            }

        }
        isShooting[i] = false;
    }


}
