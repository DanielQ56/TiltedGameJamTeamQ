using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float startingSpeed = 5f;
    [SerializeField] float focusSpeed = 3f;

    float speed;

    Rigidbody2D rb;

    bool canMove = true;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        speed = startingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
            Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed; 
    }

    public void Focus()
    {
        speed = focusSpeed;
    }
    
    public void NotFocus()
    {
        speed = startingSpeed;
    }
    public void StopMovement()
    {
        canMove = false;
        rb.velocity = Vector2.zero;
    }

    public void AllowMovement()
    {
        canMove = true;
    }
}
