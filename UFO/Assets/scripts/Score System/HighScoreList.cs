using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreList : MonoBehaviour {

    List<Attempt> _scorelist = new List<Attempt>();
    IComparer<Attempt> comparer = new CompareAttempts();
    DataPersister dp = new DataPersister();

    void Awake()
    {
        //highscore liste laden
        //_scorelist.Sort(IComparer<Attempt>);
        LoadScorelist(dp.loadData());
        PrintScore();
        _scorelist.Add(new Attempt("hans", 20));
        _scorelist.Add(new Attempt("hans", 30));
        _scorelist.Add(new Attempt("hans", 10));
        _scorelist.Add(new Attempt("hans", 50));
        _scorelist.Add(new Attempt("hans", 70));
        _scorelist.Sort(comparer);
        PrintScore();
        AddScore(new Attempt("kevin", 35));
        PrintScore();
        ClearScoreList();
        PrintScore();
        SaveScore();
    }

    void AddScore(Attempt att)
    {
        _scorelist.Add(att);
        _scorelist.Sort(comparer);
        if(_scorelist.Count > 5)
        {
            _scorelist.RemoveAt(0);
        }
    }

    void PrintScore()
    {
        foreach (Attempt a in _scorelist)
        {
            Debug.Log("Spieler " + a.name + " hat " + a.score + " Punkte");
        }
        if(_scorelist.Count == 0)
        {
            Debug.Log("Empty List");
        }
    }

    void ClearScoreList()
    {
        _scorelist.RemoveRange(0, _scorelist.Count);
    }

    void SaveScore()
    {
        dp.saveData(_scorelist);
    }

    void LoadScorelist(List<Attempt> _scorelist)
    {
        this._scorelist = _scorelist;
    }
}
