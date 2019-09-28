using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary1
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Boundary1 boundary;
    [SerializeField] GameObject Shot;
    [SerializeField] Transform BulletSpawn;
    [SerializeField] float fireRate;
    
    private Rigidbody2D m_rigidbody;
    private float nextFire;

    void Update()
    {
        if (Input.GetKey(KeyCode.Z) && Time.time > nextFire)
        {
            Debug.Log("hi");
            nextFire = Time.time + fireRate;
            GameObject clone = Instantiate(Shot, BulletSpawn);
            m_rigidbody = clone.GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical   = Input.GetAxis("Vertical");

        var movement = new Vector2(moveHorizontal, moveVertical);
        m_rigidbody.velocity = movement * speed;
        
    }
}
