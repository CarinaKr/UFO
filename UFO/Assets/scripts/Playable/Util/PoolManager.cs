using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager {

    private GameObject[] objects;
    private int nextReturnIndex = 0;
    private int nextInsertIndex = 0;

    public PoolManager(int poolSize, GameObject toPool)
    {
        objects = new GameObject[poolSize];
        for(int i = 0; i < objects.Length; i++)
        {
            objects[i] = GameObject.Instantiate(toPool);
            objects[i].SetActive(false);
        }
    }
    
    public GameObject GetObject()
    {
        GameObject returnValue = objects[nextReturnIndex];
        objects[nextReturnIndex] = null;
        nextReturnIndex++;
        nextReturnIndex %= objects.Length;
        returnValue.SetActive(true);
        return returnValue;
    }

    public void ReleaseObject(GameObject obj)
    {
        objects[nextInsertIndex] = obj;
        nextInsertIndex++;
        nextInsertIndex %= objects.Length;
        obj.SetActive(false);
    }
}
