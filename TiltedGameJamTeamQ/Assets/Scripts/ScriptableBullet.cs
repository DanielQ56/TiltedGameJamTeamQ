using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet", order = 1)]
public class ScriptableBullet : ScriptableObject
{
    public GameObject bulletPrefab;
    public float angleInBetween;
    public float radius;
    public float spawnLag;
    public float bulletSpeed;
    public string layer;
}
