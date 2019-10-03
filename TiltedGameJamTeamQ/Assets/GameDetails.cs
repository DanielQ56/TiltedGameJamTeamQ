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
    [SerializeField] AudioSource source;
    int startingPhase;


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
        LevelDialogueManager.instance.ActivateDialogueSequence();
    }

    IEnumerator FadeIn(bool ded)
    {
        LevelDialogueManager.instance.StopAllActions();
        Color change = FadeImage.color;
        while (FadeImage.color.a < 1)
        {
            change.a += Time.deltaTime;
            FadeImage.color = change;
            yield return null;
        }
        if (!ded)
        {
            SceneManager.LoadScene("waifu" + waifu.ToString());
        }
        else
        {
            Debug.Log("hi");
            gameOver.SetActive(true);
        }
    }

    void OnSceneLoaded(Scene loadedScene, LoadSceneMode sceneMode)
    {
        startingPhase = phase;
        StartCoroutine(FadeOut());
    }

    public void GameOver()
    {

        StartCoroutine(FadeIn(true));
    }

    public void onRestart()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(source.clip.length);
        if (gameOver)
        {
            gameOver.SetActive(false);
            phase = startingPhase;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
