using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerSpawner : MonoBehaviour
{
    void FixedUpdate()
    {
        ObjectPool.Instance.SpawnFromPool("Daggers", transform.position, Quaternion.identity);
        
    }
}
