using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossHealth : MonoBehaviour
{
    [SerializeField] int baseHealth = 5000;
    [SerializeField] GameObject healthBar;
    [SerializeField] RectTransform health;

    int currentHealth;
    bool waitingForDialogue = false;

    float widthDec;
    float posDec;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = baseHealth;
        widthDec = health.rect.width / baseHealth;
        posDec = widthDec / 2f;
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
        if(!healthBar.activeInHierarchy)
        {
            healthBar.SetActive(true);
        }
        currentHealth -= 1;
        health.sizeDelta = new Vector2(health.sizeDelta.x - widthDec, health.sizeDelta.y);
        health.localPosition = health.localPosition + Vector3.left * posDec;
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
