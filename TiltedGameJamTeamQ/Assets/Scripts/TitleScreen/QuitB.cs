using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitB : Button
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        sFxSource.Play();
        Application.Quit();
    }
}
