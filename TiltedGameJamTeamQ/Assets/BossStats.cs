using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    public static BossStats instance;

    [SerializeField] int DifficultyLevel = 1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public int GetDifficultyLevel()
    {
        return DifficultyLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
