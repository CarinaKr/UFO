using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NameInput : MonoBehaviour {

    private char[] alphabet = { ' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    private int currChar;
    private int curLength;
    private int nameLength;
    private int maxLength = 10;
    private int blockPressed;
    private int sidePressed;
    private string _playerName;
    private int _playerScore;

    //Each text has a dist. of 60 px starting at 0 (useful for the "handles")
    public Text[] chars;
    public GameObject handle;
    public ScoreManager scoreManager;
    public Text scoreText;

    private bool isPressed;
    private bool isSideways;

    void Awake()
    {
        currChar = 0;
        curLength = 0;
        nameLength = 0;
        chars[0].gameObject.SetActive(true);
        if (PlayerPrefs.HasKey("Score"))
        { _playerScore = PlayerPrefs.GetInt("Score"); }
        else { _playerScore = 0; }
        scoreText.text = "Your score : " + _playerScore;
    }

    void Update()
    {
        chars[curLength].text = "" + alphabet[currChar];

        if (Input.GetAxisRaw("Submit") == -1 && !isPressed)
        {
            //vorheriger buchstabe
            isPressed = true;
            currChar --;
            if(currChar < 0)
            {
                currChar = 26;
            }
        }
        else if (Input.GetAxisRaw("Submit2") == 1 && !isPressed)
        {
            //nächster buchstabe
            isPressed = true;
            currChar++;
            currChar %= alphabet.Length;
        }
        else if(Input.GetAxisRaw("Submit2")==0&&isPressed)
        {
            isPressed = false;
        }

        chars[curLength].text = "" + alphabet[currChar];
        
        if (Input.GetAxisRaw("NameSpace2")!=0 &&isSideways==false)
        {
            isSideways = true;
            int axisRaw = (int)Input.GetAxisRaw("NameSpace2");
            //_playerName += "" + alphabet[currChar];
            if ((axisRaw < 0 && curLength > 0) || (axisRaw > 0 && curLength < chars.Length - 1))
            {
                curLength += axisRaw;

                //if walking to the right, new letter
                if (!chars[curLength].isActiveAndEnabled)
                {
                    chars[curLength].gameObject.SetActive(true);
                    if (curLength - 1 < 4)
                    {
                        //put every textfield 60 units to the left and let the cursor stay in position
                        for (int i = 0; i <= curLength - 1; i++)
                        {
                            chars[i].gameObject.transform.localPosition = new Vector3((chars[i].gameObject.transform.localPosition.x - 60), chars[i].gameObject.transform.localPosition.y, chars[i].gameObject.transform.localPosition.z);
                        }
                    }
                    else
                    {
                        //let the textfields where they are and put the cursor in position
                        handle.transform.localPosition = new Vector3((handle.transform.localPosition.x + 60), handle.transform.localPosition.y, handle.transform.localPosition.z);
                    }

                    currChar = 1;
                }
                //going backwarts, letter already exists
                else if (chars[curLength].isActiveAndEnabled)
                {
                    handle.transform.localPosition = new Vector3(handle.transform.localPosition.x + axisRaw * 60, handle.transform.localPosition.y, handle.transform.localPosition.z);
                    for (int i = 0; i < alphabet.Length; i++)
                    {
                        if (chars[curLength].text.Equals(alphabet[i].ToString()))
                        {
                            currChar = i;
                        }
                    }
                }
            }
          

            //curLength++;
        }
        else if(Input.GetAxisRaw("NameSpace")==0&&isSideways)
        {
            isSideways = false;
        }

        if(isPressed)
        {
            blockPressed++;
            if(blockPressed > 20)
            {
                isPressed = false;
                blockPressed = 0;
            }
        }
        if(isSideways)
        {
            sidePressed++;
            if(sidePressed>20)
            {
                isSideways = false;
                sidePressed = 0;
            }
        }
    }

    public string playerName
    {
        get
        {
            for(int i=0;i<chars.Length;i++)
            {
                _playerName += chars[i].text;
            }
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
