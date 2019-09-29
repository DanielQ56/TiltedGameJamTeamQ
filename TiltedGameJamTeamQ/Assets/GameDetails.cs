using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameDetails : MonoBehaviour
{
    public static GameDetails instance;

    [SerializeField] int phase = 1;
    [SerializeField] int waifu = 1;
    [SerializeField] Image FadeImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
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
        Debug.Log("Hey");
        phase += 1;
        waifu += 1;
        StartCoroutine(FadeIn());
        
        
    }

    IEnumerator FadeOut()
    {
        Color change = FadeImage.color;
        while(FadeImage.color.a > 0)
        {
            change.a -= Time.deltaTime;
            FadeImage.color = change;
            yield return null;
        }
        Player.canMove = true;
    }

    IEnumerator FadeIn()
    {
        Player.canMove = false;
        Color change = FadeImage.color;
        while (FadeImage.color.a < 1)
        {
            change.a += Time.deltaTime;
            FadeImage.color = change;
            yield return null;
        }
        SceneManager.LoadScene("waifu" + waifu.ToString());

    }

    void OnSceneLoaded(Scene loadedScene, LoadSceneMode sceneMode)
    {
        FadeOut();
    }

}
