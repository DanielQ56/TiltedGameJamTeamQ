using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    [SerializeField] float magnitude;
    [SerializeField] float maxShakeTime = 2f;
    [SerializeField] float timeDamper;

    Vector3 originalPosition;

    float shakeTime = 0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTime > 0)
        {
            float percentComplete = (maxShakeTime - shakeTime) / maxShakeTime;
            float damper = 1 - percentComplete;
            this.transform.position = new Vector3(Random.Range(-1f, 1f) * magnitude * damper, Random.Range(-1f, 1f) * magnitude * damper, transform.position.z); 
            shakeTime -= Time.deltaTime * timeDamper;
        }
        else
            this.transform.position = originalPosition;
    }

    public void Shake()
    {
        shakeTime = maxShakeTime;
    }
}
