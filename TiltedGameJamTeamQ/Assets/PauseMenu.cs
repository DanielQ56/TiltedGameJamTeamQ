using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausemenu;
    [SerializeField] AudioSource source;

    bool paused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("HI");
            if (!paused)
            {
                pausemenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pausemenu.SetActive(false);
                Time.timeScale = 1;
            }

            paused = !paused;
        }
    }

    //for the continue button
    public void onUnpause()
    {
        StartCoroutine(Unpause());
    }

    IEnumerator Unpause()
    {
        Time.timeScale = 1;
        Debug.Log(source.clip.length);
        yield return new WaitForSeconds(source.clip.length);
        Debug.Log("hey");
        pausemenu.SetActive(false);
        paused = !paused;
    }

}
