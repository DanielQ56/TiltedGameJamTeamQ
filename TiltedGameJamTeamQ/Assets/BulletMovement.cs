using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void FireOff()
    {
        rb.velocity = Vector3.Normalize(this.transform.localPosition) * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
