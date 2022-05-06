using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    
    // Start is creating replicas of the object in question, and sets them to be inactive. 
    //https://learn.unity.com/tutorial/introduction-to-object-pooling#5ff8d015edbc2a002063971d
    //Brackeys
    //https://www.youtube.com/watch?v=tdSmKaJvCoA


    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size; 
    }

    public static ObjectPool Instance; //Container to hold an instance. 
    
    #region singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    #endregion
    

    public List<Pool> pools; //Different types of pools that hold anything that needs to be replicated.
    public Dictionary<string, Queue<GameObject>> poolDic; //Holds information about objects, and the objects in a queue. 
    
    void Start()
    {
        poolDic = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) //Building the objects and setting them as inactive on the scene. 
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDic.Add(pool.tag, objectPool);
        }
    }

    //SPAWNS IN ITEMS FROM OBJECT POOL
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion identity) //Method for accessing the inactive objects, activating them for use. 
    {
        if (!poolDic.ContainsKey(tag))
        {
            Debug.Log("Pool is broken");
            return null;
        }
        
        GameObject objToSpawn = poolDic[tag].Dequeue();
        
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = identity;
        
        poolDic[tag].Enqueue(objToSpawn);
        
        return objToSpawn;

    }
}
