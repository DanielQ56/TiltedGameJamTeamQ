using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] List<ObjectPool> pools;
    [SerializeField] PlayerMovement pMove;
    [SerializeField] AudioSource source;
    SpriteRenderer sp;

    bool isShooting = false;

    bool canShoot = true;

    void Start()
    {
        sp = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                Shoot();
            }
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Focus();
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                Revert();
            }
        }
    }

    void Focus()
    {
        pMove.Focus();
        sp.enabled = true;
        foreach (ObjectPool p in pools)
        {
            p.Straighten();
        }
    }

    void Revert()
    {
        pMove.NotFocus();
        foreach (ObjectPool p in pools)
        {
            p.Revert();
        }
        sp.enabled = false;
    }

    void Shoot()
    {
        if(!isShooting && canShoot)
        {
            StartCoroutine(ShootBullets());
        }
    }

    IEnumerator ShootBullets()
    {
        isShooting = true;
        foreach (ObjectPool p in pools)
        {
            GameObject b = p.GetUnusedObject();
            b.SetActive(true);
            b.transform.localPosition = p.gameObject.transform.localPosition + (p.gameObject.transform.up * 0.01f);
            b.GetComponent<BulletMovement>().FireOff(p.transform.localPosition, p.GetBulletSpeed(), p.GetLayer());
        }
        yield return new WaitForSeconds(0.05f);
        isShooting = false;
    }

    public void StopShooting()
    {
        canShoot = false;
    }

    public void AllowShooting()
    {
        canShoot = true;
    }
}
