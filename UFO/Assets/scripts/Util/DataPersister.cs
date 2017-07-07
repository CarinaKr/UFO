using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

[Serializable]
public class DataPersister
{
    private readonly string DATA_KEY = "game_data";

    public void saveData(List<Attempt> data)
    {

        using (MemoryStream ms = new MemoryStream())
        {
            new BinaryFormatter().Serialize(ms, data);
            string dataString = Convert.ToBase64String(ms.ToArray());
            PlayerPrefs.SetString(DATA_KEY, dataString);
            Debug.Log("Saved to " + DATA_KEY);
        }
    }

    public List<Attempt> loadData()
    {
        if(PlayerPrefs.HasKey(DATA_KEY))
        {
            Debug.Log("Key exists");
            string dataString = PlayerPrefs.GetString(DATA_KEY);
            byte[] bytes = Convert.FromBase64String(dataString);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                List<Attempt> myList = (List<Attempt>)new BinaryFormatter().Deserialize(ms);
                foreach(Attempt a in myList)
                {
                    Debug.Log(a.name);
                }
                Debug.Log(myList.Count);
                return myList;
            }
        }

        return new List<Attempt>();
    }
}
