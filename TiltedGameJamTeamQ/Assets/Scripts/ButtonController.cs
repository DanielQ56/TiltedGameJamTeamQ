using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    [SerializeField] protected AudioClip clicksFx;
    [SerializeField] protected AudioSource sFxSource;

    
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
        clickedsFx();
    }

    public void onPlay()
    {
        SceneManager.LoadScene("waifu1");
        clickedsFx();
    }

    public void onCredits()
    {
        SceneManager.LoadScene("CreditScene");
        clickedsFx();
    }
    public void onCreditBack()
    {
        SceneManager.LoadScene("TitleScreen");
        clickedsFx();
    }

}
