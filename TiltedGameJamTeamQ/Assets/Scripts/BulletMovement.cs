﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void FireOff(Vector3 pos, float speed)
    {
        rb.velocity = Vector3.Normalize(this.transform.localPosition - pos) * speed;
        this.transform.parent = null;
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);

    }
}
