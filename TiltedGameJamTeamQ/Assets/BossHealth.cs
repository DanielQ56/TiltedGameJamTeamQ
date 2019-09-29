using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int baseHealth = 5000;

    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void DecreaseHealth()
    {
        currentHealth -= 1;
        if(currentHealth <= baseHealth / 2)
        {
            if (currentHealth <= 0)
            {
                GameDetails.instance.nextBoss();
                Destroy(this.gameObject);
            }
            GameDetails.instance.ChangePhase();
        }
    }

    
}
