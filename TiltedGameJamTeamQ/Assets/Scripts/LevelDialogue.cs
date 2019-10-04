using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class LevelDialogue : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] List<string> levelDialogue;

    public UnityEvent done;


    bool started = false;

    bool waitingToContinue = false;
    bool doneWithDialogue = false;
    bool entered = false;


    private void Awake()
    {
        done = new UnityEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if(started && Input.GetKeyDown(KeyCode.P))
        {
            Done();
        }
        if (waitingToContinue)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (!doneWithDialogue)
                {
                    waitingToContinue = false;
                    StartCoroutine(ReadLine());
                }
                else
                {
                    Done();
                }
            }
        }
    }

    public void StartDialogue()
    {
        started = true;
        dialoguePanel.SetActive(true);
        StartCoroutine(ReadLine());
    }

    void Done()
    {
        waitingToContinue = false;
        dialoguePanel.SetActive(false);
        done.Invoke();
        Destroy(this.gameObject);
    }

    void NextLine()
    {
        StopCoroutine(ReadLine());
        levelDialogue.RemoveAt(0);
        if (levelDialogue.Count == 0)
        {
            doneWithDialogue = true;
        }
        waitingToContinue = true;
    }

    IEnumerator ReadLine()
    {
        dialogueText.text = "";
        string l = levelDialogue[0];
        for(int i = 0; i < l.Length; ++i )
        {
            dialogueText.text += l[i];
            yield return new WaitForSeconds(0.01f);
        }
        NextLine();
    }

}
