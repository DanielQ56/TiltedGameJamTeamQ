using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDialogueManager : MonoBehaviour
{
    public static LevelDialogueManager instance = null;

    [SerializeField] PlayerMovement pMove;
    [SerializeField] PlayerShoot pShoot;
    [SerializeField] BossShooting bShoot;
    [SerializeField] BossMovement bMove;
    [SerializeField] BossHealth bHealth;
    [SerializeField] PlayerHealth pHealth;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ActivateDialogueSequence()
    {
        if (transform.childCount > 0)
        {
            LevelDialogue l = this.transform.GetChild(0).GetComponent<LevelDialogue>();
            l.done.AddListener(DoneListener);
            l.StartDialogue(); 
        }
    }

    public void StopAllActions()
    {
        try
        {
            pMove.StopMovement();
            bMove.StopMovement();
            bShoot.StopShooting();
            pShoot.StopShooting();
            pHealth.EndInvulnerability();
        }
        catch (MissingReferenceException) { Debug.Log("OMEGALUL"); }
    }

    void AllowActions()
    {
        pMove.AllowMovement();
        bMove.AllowMovement();
        bShoot.AllowShooting();
        pShoot.AllowShooting();
        bHealth.DoneWithDialogue();
        pHealth.AllowDamage();
    }

    void DoneListener()
    {
        AllowActions();
    }

    
}
