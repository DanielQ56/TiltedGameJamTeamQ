using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameDetails : MonoBehaviour
{
    public static GameDetails instance;

    [SerializeField] int phase = 1;
    [SerializeField] int waifu = 1;

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
        phase += 1;
        waifu += 1;
        SceneManager.LoadScene("waifu" + waifu.ToString());
        //fade out fade in 
        
    }

}
