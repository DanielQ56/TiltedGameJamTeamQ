using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] List<Transform> DestinationPoints;
    [SerializeField] float maxWaitTime;
    [SerializeField] float minWaitTime;


    bool waitingToMove = false;
    bool moving = false;

    float waitTimer;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!waitingToMove)
        {
            waitTimer = Random.Range(minWaitTime, maxWaitTime);
            waitingToMove = true;
        }
        if (waitTimer <= 0)
        {
            if (!moving)
                ChooseNewDestination();
        }
        else
            waitTimer -= Time.deltaTime;
        
    }



    IEnumerator MoveToNewPosition(Transform t)
    {
        moving = true;
        float maxDistance = Mathf.Abs(Vector3.Distance(this.transform.position, t.position));
        while (Mathf.Abs(Vector3.Distance(this.transform.position, t.position)) > 0.5f)
        {
            float tthing = Mathf.Clamp(maxDistance - Mathf.Abs(Vector3.Distance(this.transform.position, t.position)) / maxDistance, 0.01f, 0.5f);
            this.transform.position = Vector3.Lerp(this.transform.position, t.position, tthing * 0.5f);
            yield return null;
        }
        this.transform.position = t.position;
        moving = false;
        waitingToMove = false;
        CameraScript.instance.Shake();
    }

    void ChooseNewDestination()
    {
        if (BossStats.instance.GetDifficultyLevel() > 1)
        {
            Transform t = DestinationPoints[Random.Range(0, BossStats.instance.GetDifficultyLevel())];
            while (t.position == this.transform.position)
            {
                t = DestinationPoints[Random.Range(0, BossStats.instance.GetDifficultyLevel() + 1)];
            }
            StartCoroutine(MoveToNewPosition(t));
        }
        
    }
}
