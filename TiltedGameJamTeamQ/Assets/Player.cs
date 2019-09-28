using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool focusOn = false;
    public int speed = 10;
    public int life = 3;
    public bool hit = false;
    private float InvurTime = 0.5f;
    private bool Invurnable = false;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        /*
        if ( OnHit() == true )
        {
            Iframe();
        }
        */
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            focusOn = true;
            speed = 5;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            focusOn = false;
            speed = 10;
        }
    }



    private void Iframe()
    {
        Invurnable = true;
        InvurTime -= Time.deltaTime;
        if ( InvurTime == 0 )
        {

        }
    }

    /*
    private bool OnHit()
    {
        //insert the hitbox and collision

        //if collide return true
        if ( hit = true )
            if ( life > 0 )
            {
                life -= 1;
                // insert code for invurnablility frame animation.
            }
            else if ( life == 0)
            {
                life = 0;
                // Insert Code for death animation and end game.
            }
        else
            {
                return false;
            }
    }
    */
}

