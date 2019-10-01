using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    [SerializeField] protected AudioClip clicksFx;
    [SerializeField] protected AudioSource sFxSource;

    [SerializeField] GameObject gameOver;
    // Start is called before the first frame update
    private void Awake()
    {
        sFxSource.clip = clicksFx;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void clickedsFx()
    {
        sFxSource.Play();
    }
    public void onQuit()
    {
        Application.Quit();
    }

    public void onPlay()
    {
        SceneManager.LoadScene("waifu1");
    }

    public void onCredits()
    {
        SceneManager.LoadScene("CreditScene");
    }
    public void onCreditBack()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void onRestart()
    {
        //Debug.Log(SceneManager.GetActiveScene().ToString());
        if (gameOver) //gameOVer != null
        {
            gameOver.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
