using System.Collections;
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
        isShooting = true;
        
        while (angle < 360f && revs != 3f)
        {
            float rad = Mathf.Deg2Rad * angle;
            GameObject b = pool.GetUnusedObject(); //pool
            b.SetActive(true); //pool
            BulletMovement b2 = b.GetComponent<BulletMovement>();
            
            b.transform.localPosition = Vector3.Normalize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad))) * radius;
            b2.FireOff(this.transform.position, pool.GetBulletSpeed());

            yield return new WaitForSeconds(spawnLag);
            angle += angleInBetween;

            if (angle >= 360)
            {
                angle = 0f + (revs * 30);
                revs++;
            }
                
        }

        isShooting = false;
        waitingToShoot = false;
    }


}
