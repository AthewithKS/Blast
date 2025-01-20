using System.Collections.Generic;
using UnityEngine;

public class BallObjectPooling : MonoBehaviour
{
    public static BallObjectPooling SharedInstance;
    public GameObject prefab;
    public int initialPoolSize=20;
    [SerializeField]
    private List<GameObject> pool;

    private void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
        pool = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < initialPoolSize; i++)
        {
            temp = Instantiate(prefab);
            temp.SetActive(false);
            pool.Add(temp);
        }
    }
    public GameObject GetFromPool()
    {
       for(int i = 0; i < initialPoolSize; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
       return null;
    }
}
