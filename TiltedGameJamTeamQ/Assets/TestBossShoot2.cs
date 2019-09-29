using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossShoot2 : MonoBehaviour
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
        angle = 0f;
        List<GameObject> movement = new List<GameObject>();
        isShooting = true;
        float radius = pool.GetRadius();
        float angleInBetween = pool.getAngleInBetween();
        float spawnLag = pool.getSpawnLag();
        while (angle < 360f)
        {
            float rad = Mathf.Deg2Rad * angle;
            GameObject b = pool.GetUnusedObject();
            b.SetActive(true);
            b.GetComponent<SpriteRenderer>().enabled = false;
            b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * Random.Range(radius, radius + 0.1f);
            movement.Add(b);
            yield return new WaitForSeconds(spawnLag);
            angle += angleInBetween + Random.Range(-10f, 10f);
        }
        foreach (GameObject b in movement)
        {
            b.GetComponent<SpriteRenderer>().enabled = true;

            b.GetComponent<BulletMovement>().FireOff(pool.transform.localPosition, pool.GetBulletSpeed());
        }
        isShooting = false;
        waitingToShoot = false;
    }


}
