using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int baseHealth = 5000;

    int currentHealth;
    bool waitingForDialogue = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && !waitingForDialogue)
        {
            GameDetails.instance.nextBoss();
            Destroy(this.gameObject);
        }
    }

    public void DecreaseHealth()
    {
        currentHealth -= 1;
        if(currentHealth == baseHealth / 2)
        {
            GameDetails.instance.ChangePhase();
        }
        if (currentHealth <= 0 && !waitingForDialogue)
        {
            LevelDialogueManager.instance.StopAllActions();
            LevelDialogueManager.instance.ActivateDialogueSequence();
            waitingForDialogue = true;
        }
    }

    public void DoneWithDialogue()
    {
        waitingForDialogue = false;
    }

    
}
