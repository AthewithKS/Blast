using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormManager : MonoBehaviour
{
    public static PlatFormManager instance;

    public Transform[] platforms; 
    public bool[] isPlatformOccupied; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        isPlatformOccupied = new bool[platforms.Length]; 
    }
    public int GetAvailablePlatform()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            if (!isPlatformOccupied[i]) 
            {
                isPlatformOccupied[i] = true; 
                return i;
            }
        }
        return -1; 
    }
    public void FreePlatform(int index)
    {
        if (index >= 0 && index < isPlatformOccupied.Length)
        {
            isPlatformOccupied[index] = false; 
        }
    }
    public Transform GetPlatformTransform(int index)
    {
        if (index >= 0 && index < platforms.Length)
        {
            return platforms[index]; 
        }
        return null;
    }
}
