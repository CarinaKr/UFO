using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using UnityEngine.UI;

public class ArduinoTest : MonoBehaviour {

    public Text[] text;
    public Text buttonText;
    public float moveSpeed;
    public float maxTiltUfoX, maxTiltUfoZ;
    public float maxTiltBoardX, maxTiltBoardZ;
    public float tiltTolerance;

    SerialPort sp = new SerialPort("COM6", 9600);
    private float zBaseX, zBaseY, zBaseZ;
    private float zX, zY, zZ;
    private String zIn;
    private String[] zValues;
    private bool zBaselined;
    private float zMaxArduinoX,zMaxArduinoY;


	// Use this for initialization
	void Start () {
        sp.Open();
        sp.ReadTimeout = 1;
        zMaxArduinoX = maxTiltBoardX / 9;
        zMaxArduinoY = maxTiltBoardZ / 9;
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                zIn = sp.ReadLine();
                text[0].text = "Gesamt" + zIn + "end of text";

                zValues = zIn.Split(';');
                zX = float.Parse(zValues[0])- zBaseX ;
                zY = float.Parse(zValues[1])-zBaseY ;
                zZ = float.Parse(zValues[2])-zBaseZ ;

                text[1].text = "X: " + ( zX);
                text[2].text = "Y: " + ( zY);
                text[3].text = "Z: " + ( zZ);

                /*text[1].text = "X: " + (zX);
                text[2].text = "Y: " + (zY);
                text[3].text = "Z: " + (zZ);*/

                sp.BaseStream.Flush();
            }
            catch (System.Exception)
            {

            }
        }

        if (zBaselined)
        {
            float tiltX = (zX / zMaxArduinoX) * maxTiltUfoX;  //zX wird max. ca 10 -> zX=1 == 9° Kippung des Sensors
            float tiltZ = (zY / zMaxArduinoY) * maxTiltUfoZ;  //zY wird max ca 10
            

            transform.localEulerAngles = new Vector3(-1*tiltZ, 0, tiltX);


            float x = tiltX * Time.deltaTime * moveSpeed;
            float z = tiltZ * Time.deltaTime * moveSpeed;

            transform.Translate(-1*x, 0, -1*z, Space.World);
        }
        
    }

    public void baseline()
    {
        zBaseX = zX;
        zBaseY = zY;
        zBaseZ = zZ;
        buttonText.text = "X: " + zBaseX + " Y: " + zBaseY + " Z: " + zBaseZ;
        zBaselined = true;
    }

    
}
