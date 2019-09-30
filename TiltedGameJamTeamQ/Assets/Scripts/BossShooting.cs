using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    [SerializeField] List<WaitTimes> listOfTimes;
    [SerializeField] List<ObjectPool> pool;
    [SerializeField] GameObject player;


    float angle = 0f;

    float timer = 0;

    delegate void Attacks(int index);

    List<bool> isShooting;
    List<float> timers;

    List<Attacks> listOfAttacks;

    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        timers = new List<float>();
        isShooting = new List<bool>();
        listOfAttacks = new List<Attacks>();
        listOfAttacks.Add(One);
        listOfAttacks.Add(Two);
        listOfAttacks.Add(Three);
        listOfAttacks.Add(Four);
        listOfAttacks.Add(Five);
        listOfAttacks.Add(Six);
        foreach(Attacks a in listOfAttacks)
        {
            isShooting.Add(false);
            timers.Add(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot)
            Shoot();
       
    }

    public void StopShooting()
    {
        canShoot = false;
    }

    public void AllowShooting()
    {
        canShoot = true;
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
        StartCoroutine(PhaseOne(index));
    }
    void Two(int index)
    {
        StartCoroutine(PhaseTwo(index));
    }

    void Three(int index)
    {
        StartCoroutine(PhaseThree(index));
    }

    void Four(int index)
    {
        StartCoroutine(PhaseFour(index));
    }

    void Five(int index)
    {
        StartCoroutine(PhaseFive(index));
    }

    void Six(int index)
    {
        StartCoroutine(PhaseSix(index));
    }

    IEnumerator PhaseOne(int i)
    {
        angle = 0f;
        List<BulletMovement> movement = new List<BulletMovement>();
        isShooting[i] = true;
        float radius = pool[i].GetRadius();
        float angleInBetween = pool[i].getAngleInBetween();
        float spawnLag = pool[i].getSpawnLag();
        while(angle < 360f)
        {
            float rad = Mathf.Deg2Rad * angle;
            GameObject b = pool[i].GetUnusedObject();
            b.SetActive(true);
            BulletMovement m = b.GetComponent<BulletMovement>();
            m.Setup(pool[i].GetLayer());
            b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * radius;
            movement.Add(b.GetComponent<BulletMovement>());
            yield return new WaitForSeconds(spawnLag);
            angle += angleInBetween;
        }
        foreach(BulletMovement b in movement)
        {
            b.FireOff(pool[i].transform.localPosition, pool[i].GetBulletSpeed(), pool[i].GetLayer());
        }
        isShooting[i] = false;
    }

    IEnumerator PhaseTwo(int i)
    {
        float revs = 0f;
        angle = 0f;
        isShooting[i] = true;
        float radius = pool[i].GetRadius();
        float angleInBetween = pool[i].getAngleInBetween();
        float spawnLag = pool[i].getSpawnLag();
        while (revs < 3f)
        {
            float rad = Mathf.Deg2Rad * angle;
            GameObject b = pool[i].GetUnusedObject(); //pool
            b.SetActive(true); //pool
            BulletMovement b2 = b.GetComponent<BulletMovement>();

            b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * radius;
            b2.FireOff(pool[i].transform.localPosition, pool[i].GetBulletSpeed(), pool[i].GetLayer());

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

    IEnumerator PhaseThree(int i)
    {
        float revs = 0f;
        angle = 0f;
        List<GameObject> movement = new List<GameObject>();
        isShooting[i] = true;
        float radius = pool[i].GetRadius();
        float angleInBetween = pool[i].getAngleInBetween();
        float spawnLag = pool[i].getSpawnLag();

        while (revs < 3f)
        {
            while (angle < 360f)
            {
                float rad = Mathf.Deg2Rad * angle;
                GameObject b = pool[i].GetUnusedObject();
                b.SetActive(true);
                BulletMovement m = b.GetComponent<BulletMovement>();
                m.Setup(pool[i].GetLayer());
                b.GetComponent<SpriteRenderer>().enabled = false;
                b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * (radius);
                movement.Add(b);
                yield return new WaitForSeconds(spawnLag);
                angle += angleInBetween;

            }
            foreach (GameObject b in movement)
            {
                b.GetComponent<SpriteRenderer>().enabled = true;
                b.GetComponent<BulletMovement>().FireOff(pool[i].transform.localPosition, pool[i].GetBulletSpeed(), pool[i].GetLayer());
            }

            revs += 1f;
            angle = 0f;
            movement = new List<GameObject>();

        }

        isShooting[i] = false;
    }

    IEnumerator PhaseFour(int i)
    {
        angle = 0f;
        List<GameObject> movement = new List<GameObject>();
        isShooting[i] = true;
        float radius = pool[i].GetRadius();
        float angleInBetween = pool[i].getAngleInBetween();
        float spawnLag = pool[i].getSpawnLag();
        while (angle < 360f)
        {
            float rad = Mathf.Deg2Rad * angle;
            GameObject b = pool[i].GetUnusedObject();
            b.SetActive(true);
            BulletMovement m = b.GetComponent<BulletMovement>();
            m.Setup(pool[i].GetLayer());
            b.GetComponent<SpriteRenderer>().enabled = false;
            b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * Random.Range(radius, radius + 0.1f);
            movement.Add(b);
            yield return new WaitForSeconds(spawnLag);
            angle += angleInBetween + Random.Range(-10f, 10f);
        }
        foreach (GameObject b in movement)
        {
            b.GetComponent<SpriteRenderer>().enabled = true;

            b.GetComponent<BulletMovement>().FireOff(pool[i].transform.localPosition, pool[i].GetBulletSpeed(), pool[i].GetLayer());
        }
        isShooting[i] = false;
    }

    IEnumerator PhaseFive(int i)
    {
        float revs = 0f;
        angle = 0f;
        isShooting[i] = true;
        float radius = pool[i].GetRadius();
        float angleInBetween = pool[i].getAngleInBetween();
        float spawnLag = pool[i].getSpawnLag();
        while (revs < 5f)
        {
            float rad = Mathf.Deg2Rad * angle;
            GameObject b = pool[i].GetUnusedObject(); //pool
            b.SetActive(true); //pool
            BulletMovement b2 = b.GetComponent<BulletMovement>();

            b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * radius;
            b2.FireOff(pool[i].transform.localPosition, pool[i].GetBulletSpeed(), pool[i].GetLayer());

            float rad2 = Mathf.Deg2Rad * angle + Mathf.PI;
            GameObject b3 = pool[i].GetUnusedObject(); //pool
            b3.SetActive(true); //pool
            BulletMovement b4 = b3.GetComponent<BulletMovement>();

            b3.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad2), Mathf.Sin(rad2))) * radius;
            b4.FireOff(pool[i].transform.localPosition, pool[i].GetBulletSpeed(), pool[i].GetLayer());

            yield return new WaitForSeconds(spawnLag);
            angle += angleInBetween;
            if(angle >= 360f)
            {
                revs += 1;
                angle = 0;
            }
        }
        isShooting[i] = false;
    }
    IEnumerator PhaseSix(int i)
    {
        int numberSpawned = 0;
        List<GameObject> movement = new List<GameObject>();
        isShooting[i] = true;
        float radius = pool[i].GetRadius();
        float angleInBetween = pool[i].getAngleInBetween();
        float spawnLag = pool[i].getSpawnLag();
        while (numberSpawned < 7)
        {
            float rad = Mathf.Deg2Rad * angle;
            GameObject b = pool[i].GetUnusedObject();
            b.SetActive(true);
            b.transform.localPosition =  Vector3.Normalize((player.transform.position - pool[i].transform.position)) * radius;
            b.GetComponent<BulletMovement>().Homing(player.transform.position, pool[i].GetBulletSpeed(), pool[i].GetLayer());
            yield return new WaitForSeconds(spawnLag);
            numberSpawned += 1;
        }
        isShooting[i] = false;
    }



}
