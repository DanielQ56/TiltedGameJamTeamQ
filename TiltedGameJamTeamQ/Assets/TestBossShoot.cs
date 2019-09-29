﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float angleInBetween;
    [SerializeField] float radius;
    [SerializeField] float spawnLag;
    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    [SerializeField] ObjectPool pool;


    bool waitingToShoot = false;
    bool isShooting = false;

    float angle = 0f;

    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (!waitingToShoot)
        {
            timer = Random.Range(minWaitTime, maxWaitTime);
            waitingToShoot = true;
        }
        if (timer <= 0)
        {
            if (!isShooting)
                StartCoroutine(SpawnBullets());
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }

    IEnumerator SpawnBullets()
    {
        float revs = 0f;
        angle = 0f;
        List<GameObject> movement = new List<GameObject>();
        isShooting = true;
        float radius = pool.GetRadius();
        float angleInBetween = pool.getAngleInBetween();
        float spawnLag = pool.getSpawnLag();

        while (revs < 3f)
        {
            while (angle < 360f)
            {
                float rad = Mathf.Deg2Rad * angle;
                GameObject b = pool.GetUnusedObject();
                b.SetActive(true);
                b.GetComponent<SpriteRenderer>().enabled = false;
                b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * (radius);
                movement.Add(b);
                yield return new WaitForSeconds(spawnLag);
                angle += angleInBetween;

            }
            foreach (GameObject b in movement)
            {
                b.GetComponent<SpriteRenderer>().enabled = true;
                b.GetComponent<BulletMovement>().FireOff(pool.transform.localPosition, pool.GetBulletSpeed());
            }

            Debug.Log(radius + (revs * 0.2f));
            revs += 1f;
            angle = 0f;
            movement = new List<GameObject>();
            
        }

        isShooting = false;
        waitingToShoot = false;
    }


}
