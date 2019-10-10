using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameDetails : MonoBehaviour
{
    public static GameDetails instance = null;

    [SerializeField] int phase = 1;
    [SerializeField] int waifu = 1;
    [SerializeField] Image FadeImage;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject youWon;
    [SerializeField] AudioSource source;
    int startingPhase;

    bool onMenu = false;

    PauseMenu p;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        p = GetComponent<PauseMenu>();
    }


    public int GetCurrentPhase()
    {
        return phase;
    }

    public void ChangePhase()
    {
            phase += 1;
       
    }

    public void nextBoss()
    {
        phase += 1;
        waifu += 1;
        StartCoroutine(FadeIn(false));
        
        
    }

    IEnumerator FadeOut()
    {
        if(!onMenu)
            LevelDialogueManager.instance.StopAllActions();
        Color change = FadeImage.color;
        if(FadeImage.color.a < 1)
        {
            change.a = 1f;
            FadeImage.color = change;
            yield return null;
        }
        while(FadeImage.color.a > 0)
        {
            change.a -= Time.deltaTime;
            FadeImage.color = change;
            yield return null;
        }
        if(!onMenu)
            LevelDialogueManager.instance.ActivateDialogueSequence();
    }

    IEnumerator FadeIn(bool ded)
    {
        LevelDialogueManager.instance.StopAllActions();
        Color change = FadeImage.color;
        youWon.SetActive(false);
        while (FadeImage.color.a < 1)
        {
            change.a += Time.deltaTime;
            FadeImage.color = change;
            yield return null;
        }
        if (onMenu)
        {
            SceneManager.LoadScene("TitleScreen");
            Debug.Log("Going to menu");
        }
        else if (!ded && waifu != 4)
        {
            SceneManager.LoadScene("waifu" + waifu.ToString());
        }
        else if (!ded && waifu == 4)
        {
            youWon.SetActive(true);
        }
        else
        {
            gameOver.SetActive(true);
        }
    }

    void OnSceneLoaded(Scene loadedScene, LoadSceneMode sceneMode)
    {
        if (loadedScene == SceneManager.GetSceneByName("TitleScreen"))
        {
            startingPhase = 1;
            phase = 1;
            waifu = 1;
            StartCoroutine(FadeOut());
        }
        else
        {
            onMenu = false;
            startingPhase = phase;
            StartCoroutine(FadeOut());
        }
    }

    public void HeadingToMainMenu()
    {
        gameOver.SetActive(false);
        youWon.SetActive(false);
        onMenu = true;
        StartCoroutine(FadeIn(false));
    }

    public void GameOver()
    {
        StartCoroutine(FadeIn(true));
    }

    public void onRestart()
    {
        StartCoroutine(Restart());
    }

    public void FullReset()
    {
        startingPhase = 1;
        phase = 1;
        waifu = 1;
        StartCoroutine(FadeIn(false));
    }

    IEnumerator Restart()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(source.clip.length);
        if (gameOver)
        {
            gameOver.SetActive(false);
            youWon.SetActive(false);
            phase = startingPhase;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
