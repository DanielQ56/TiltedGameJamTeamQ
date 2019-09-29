using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject hitbox;
    public GameObject sprite;
    public Image lifeImage;

    public int speed = 10;
    public int life = 3;
    public bool hit = false;
    public float InvurTime = 1.5f;
    public bool Invurnable = false;
    public Vector2 direction;

    Rigidbody2D rb;
    [SerializeField] List<ObjectPool> pools;

    bool isShooting = false;

    public static bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            GetInput();
            Move();
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
    }

    // Activates focus that will narrow the bullet spread and slows player down
    //  Need to implement turning on the focus dot
    public void Focus()
    {
        speed = 3;
        hitbox.SetActive(true);
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
            speed = 6;
            hitbox.SetActive(false);
            foreach (ObjectPool p in pools)
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

    IEnumerator Shoot()
    {
        isShooting = true;
        foreach(ObjectPool p in pools)
        {
            GameObject b = p.GetUnusedObject();
            b.SetActive(true);
            b.transform.localPosition = p.gameObject.transform.localPosition + (p.gameObject.transform.up * 0.01f);
            b.GetComponent<BulletMovement>().FireOff(p.transform.localPosition, p.GetBulletSpeed(), p.GetLayer());
        }
        yield return new WaitForSeconds(0.05f);
        isShooting = false;
    }

    /*Turns on a Invurnability frame for 0.5 seconds after getting hit.
 */
    IEnumerator Iframe()
    {
        Invurnable = true;
        Debug.Log("Invurnable");
        // insert code for invurnablility frame animation.

        DamageBlink();
        yield return new WaitForSeconds(InvurTime);
        Invurnable = false;
        Debug.Log("no longer");
    }

    IEnumerator DamageBlink()
    {
        float time = 0f;
        while(Invurnable)
        {
            sprite.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            sprite.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            time += Time.deltaTime;
            Debug.Log(time);
        }
    }


    /*Detects if the player collides with a bullet and loses a heart if hit.\
     */
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            if (Invurnable == false)
            {
                if (life > 0)
                {
                    Debug.Log("hit");
                    life -= 1;
                    StartCoroutine(Iframe());
                    StartCoroutine(DamageBlink());
                    Debug.Log(life);
                    if (life == 0)
                    {
                        life = 0;
                        // Insert Code for death animation and end game.
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
