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

    bool canMove = true;

    Transform newDest;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
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
        
    }



    IEnumerator MoveToNewPosition()
    {
        moving = true;
        float maxDistance = Mathf.Abs(Vector3.Distance(this.transform.position, newDest.position));
        while (Mathf.Abs(Vector3.Distance(this.transform.position, newDest.position)) > 0.5f)
        {
            float tthing = Mathf.Clamp(maxDistance - Mathf.Abs(Vector3.Distance(this.transform.position, newDest.position)) / maxDistance, 0.01f, 0.5f);
            this.transform.position = Vector3.Lerp(this.transform.position, newDest.position, tthing * 0.5f);
            yield return null;
        }
        this.transform.position = newDest.position;
        moving = false;
        waitingToMove = false;
        CameraScript.instance.Shake();
    }

    void ChooseNewDestination()
    {
        int phase = GameDetails.instance.GetCurrentPhase();
        if (phase > 1)
        {
            newDest = DestinationPoints[Random.Range(0, phase)];
            while (newDest.position == this.transform.position)
            {
                newDest = DestinationPoints[Random.Range(0,phase)];
            }
            StartCoroutine(MoveToNewPosition());
        }
        
    }

    public void StopMovement()
    {
        canMove = false;
        StopCoroutine(MoveToNewPosition());
    }

    public void AllowMovement()
    {
        canMove = true;
    }
}
