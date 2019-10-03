using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] List<string> touchableLayers;



    Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        this.transform.localScale = Vector3.one * 0.18f;
    }

    public void Setup(string layer)
    {
        this.gameObject.layer = LayerMask.NameToLayer(layer);
    }

    public void FireOff(Vector3 pos, float speed, string layer)
    {
        this.gameObject.layer = LayerMask.NameToLayer(layer);
        rb.velocity = Vector3.Normalize(this.transform.localPosition - pos) * speed;
        this.transform.parent = null;
    }

    public void Homing(Vector3 pos, float speed, string layer)
    {
        this.gameObject.layer = LayerMask.NameToLayer(layer);
        this.transform.LookAt(pos, Vector3.forward);
        rb.velocity = Vector3.Normalize(pos - this.transform.position) * speed ;
        this.transform.parent = null;
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (touchableLayers.Contains(LayerMask.LayerToName(other.gameObject.layer)))
        {
            if (this.gameObject.layer != other.gameObject.layer && !other.CompareTag("Bullet"))
            {
                try
                {
                    other.gameObject.GetComponent<BossHealth>().DecreaseHealth();
                }
                catch (NullReferenceException)
                {
                    other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
                }
                this.gameObject.SetActive(false);
            }
        }
    }

}
