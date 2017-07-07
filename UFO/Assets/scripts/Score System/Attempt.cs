using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Attempt
{
    int _score;
    string _name;
   
    public Attempt(string _name, int _score)
    {
        this._name = _name;
        this._score = _score;
    }

    public int score
    {
        get
        {
            return _score;
        }
    }

    public string name
    {
        get
        {
            return _name;
        }
    }

}
