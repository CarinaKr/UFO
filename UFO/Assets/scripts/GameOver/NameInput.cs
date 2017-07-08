using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NameInput : MonoBehaviour {

    private char[] alphabet = { ' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    private int currChar;
    private int curLength;
    private int maxLength = 10;
    private int blockPressed;
    private string _playerName;
    private int _playerScore;

    //Each text has a dist. of 60 px starting at 0 (useful for the "handles")
    public Text[] chars;
    public GameObject handle;
    public ScoreManager scoreManager;

    private bool isPressed;

    void Awake()
    {
        currChar = 0;
        curLength = 0;
        chars[0].gameObject.SetActive(true);
        _playerScore = 45;
    }

    void Update()
    {
        if (Input.GetAxis("Submit") == -1 && !isPressed)
        {
            //nächster buchstabe
            isPressed = true;
            Debug.Log("-1");
            currChar -= 1;
            if(currChar < 0)
            {
                currChar = 26;
            }
        }
        else if (Input.GetAxis("Submit") == 1 && !isPressed)
        {
            //voriger buchstabe
            isPressed = true;
            Debug.Log("1");
            currChar += 1;
            currChar %= alphabet.Length;
        }

        chars[curLength].text = "" + alphabet[currChar];

        if (Input.GetButtonDown("Fire1") && curLength < chars.Length-1)
        {
            _playerName += "" + alphabet[currChar];
            chars[curLength + 1].gameObject.SetActive(true);
            if(curLength < 4)
            {
                //put every textfield 60 units to the left and let the cursor stay in position
                for(int i = 0; i <= curLength; i++)
                {
                    chars[i].gameObject.transform.localPosition = new Vector3((chars[i].gameObject.transform.localPosition.x - 60), chars[i].gameObject.transform.localPosition.y, chars[i].gameObject.transform.localPosition.z);
                }
            }
            else
            {
                //let the textfields where they are and put the cursor in position
                handle.transform.localPosition = new Vector3((handle.transform.localPosition.x + 60), handle.transform.localPosition.y, handle.transform.localPosition.z);
            }
          

            curLength++;
        }

        if(isPressed)
        {
            blockPressed++;
            if(blockPressed > 15)
            {
                isPressed = false;
                blockPressed = 0;
            }
        }
    }

    public string playerName
    {
        get
        {
            return _playerName;
        }
    }

    public int playerScore
    {
        get
        {
            return _playerScore;
        }
    }
}
