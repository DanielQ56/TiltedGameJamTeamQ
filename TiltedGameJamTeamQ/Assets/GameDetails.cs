using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDetails : MonoBehaviour
{
    public static GameDetails instance;

    [SerializeField] int phase = 1;

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

    public void LoadNextScene(string scene)
    {
        phase += 1;
    }

    public int GetCurrentPhase()
    {
        return phase;
    }

    public void ChangePhase()
    {
        phase += 1;
    }
}
