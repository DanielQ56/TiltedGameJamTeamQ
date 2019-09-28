﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float angleInBetween;
    [SerializeField] float radius;
    [SerializeField] float spawnLag;
    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;


    bool waitingToShoot = false;
    bool isShooting = false;

    float angle = 0f;

    float timer = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!waitingToShoot)
        {
            timer = Random.Range(minWaitTime, maxWaitTime);
            waitingToShoot = true;
            Debug.Log(timer);
        }
        if(timer <= 0)
        {
            if(!isShooting)
                StartCoroutine(SpawnBullets());
        }
        else
        {
            timer -= Time.deltaTime;
        }
       
    }

    IEnumerator SpawnBullets()
    {
        angle = 0f;
        Debug.Log("Shooting");
        List<BulletMovement> movement = new List<BulletMovement>();
        isShooting = true;
        while(angle < 360f)
        {
            float rad = Mathf.Deg2Rad * angle;
            GameObject b = Instantiate(bullet, transform);
            b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * radius;
            movement.Add(b.GetComponent<BulletMovement>());
            yield return new WaitForSeconds(spawnLag);
            angle += angleInBetween;
        }
        foreach(BulletMovement b in movement)
        {
            b.FireOff();
        }
        isShooting = false;
        waitingToShoot = false;
    }


}
