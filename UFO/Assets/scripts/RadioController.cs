using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RadioController : MonoBehaviour {

    public Text radioChannelText;
    public AudioClip[] channels;

    private int channel;
    private AudioSource aSource;

	// Use this for initialization
	void Awake () {
        channel = 0;
        aSource = GetComponent<AudioSource>();
        aSource.clip = channels[channel];
        aSource.Play();
        //aSource.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Input.GetButtonDown("PreviousChannel"));
        if (Input.GetButtonDown("PreviousChannel"))
        {
            //prev. ch
            channel--;
            if(channel < 0 )
            {
                channel = channels.Length;
            }
            SwapChannel(channel);
            Debug.Log("Prev");
        }
        else if (Input.GetButtonDown("NextChannel"))
        {
            //next ch.
            channel++;
            channel %= channels.Length+1;
            SwapChannel(channel);
            Debug.Log("Next");
        }
	}

    void SwapChannel(int channel)
    {
        if(channel != channels.Length)
        {
            aSource.Stop();
            aSource.clip = channels[channel];
            aSource.Play();
        }
        else
        {
            aSource.Stop();
        }
    }
}
