using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaitTimes", menuName = "WaitTimes", order = 2)]
public class WaitTimes : ScriptableObject
{
    public float minWaitTime;
    public float maxWaitTime;
    public float ConstantTime;
    public bool useConstantTime;
}
