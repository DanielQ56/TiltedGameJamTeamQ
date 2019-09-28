using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed = 10;
    public int life = 3;
    public bool hit = false;
    public double InvurTime = 0.5;
    public bool Invurnable = false;
    public Vector2 direction;
    [SerializeField] List<ObjectPool> pools;

    bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // Activates focus that will narrow the bullet spread and slows player down
    //  Need to implement turning on the focus dot
    public void Focus()
    {
        speed = 3;
        foreach(ObjectPool p in pools)
        {
            p.Straighten();
        }
    }

    // Gets player input.
    private void GetInput()
    {
        direction = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Focus();
            speed = 3;
            GetPlayerDirection();

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 10;
            foreach(ObjectPool p in pools)
            {
                p.Revert();
            }
        }
        if( Input.GetKey( KeyCode.Z ) )
        {

            if (!isShooting) StartCoroutine(Shoot());
        }
        
        GetPlayerDirection();

    }
    /* Gets the player direction by the arrows
     */
    public Vector2 GetPlayerDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return Vector2.up;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return Vector2.left;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            return Vector2.down;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.zero;
        }
    }

    /*Turns on a Invurnability frame for 0.5 seconds after getting hit.
     */
    private void Iframe()
    {
        Invurnable = true;
        InvurTime -= Time.deltaTime;
        if ( InvurTime == 0 )
        {
            Invurnable = false;
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        foreach(ObjectPool p in pools)
        {
            GameObject b = p.GetUnusedObject();
            b.SetActive(true);
            b.transform.localPosition = p.gameObject.transform.localPosition + (p.gameObject.transform.up * 0.01f);
            b.GetComponent<BulletMovement>().FireOff(p.transform.localPosition, p.GetBulletSpeed());
        }
        yield return new WaitForSeconds(0.05f);
        isShooting = false;
    }

    

    /*Detects if the player collides with a bullet and loses a heart if hit.\
     */
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            if (life > 0)
            {
                life -= 1;
                Iframe();
                // insert code for invurnablility frame animation.
            }
            else if (life == 0)
            {
                life = 0;
                // Insert Code for death animation and end game.
                Destroy(this.gameObject);
            }
        }
    }
}
