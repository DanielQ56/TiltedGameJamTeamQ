using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditB : Button
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
        Debug.Log("Credits");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
