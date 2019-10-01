﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] protected AudioClip clicksFx;
    [SerializeField] protected AudioSource sFxSource;

    private void Awake()
    {
        sFxSource.clip = clicksFx;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }
    private void OnMouseExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnMouseDown()
    {
        sFxSource.Play();
    }

}
