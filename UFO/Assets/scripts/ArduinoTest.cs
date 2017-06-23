using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using UnityEngine.UI;

public class ArduinoTest : MonoBehaviour {

    public Transform rotator;

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
    private float tiltX;
    private float tiltZ;


    // Use this for initialization
    void Start () {
        sp.Open();
        sp.ReadTimeout = 1;
        zMaxArduinoX = maxTiltBoardX / 9;
        zMaxArduinoY = maxTiltBoardZ / 9;

        StartCoroutine(UpdateGyro());
    }


    public void baseline()
    {
        zBaseX = zX;
        zBaseY = zY;
        zBaseZ = zZ;
        buttonText.text = "X: " + zBaseX + " Y: " + zBaseY + " Z: " + zBaseZ;
        zBaselined = true;
    }

    IEnumerator UpdateGyro()
    {
        while (true)
        {
            if (sp.IsOpen)
            {
                try
                {

                    zIn = sp.ReadLine();
                    //yield return new WaitForSeconds(0.0001f);
                    text[0].text = "Gesamt" + zIn + "end of text";

                    zValues = zIn.Split(';');
                    zX = float.Parse(zValues[0]) - zBaseX;
                    zY = float.Parse(zValues[1]) - zBaseY;
                    zZ = float.Parse(zValues[2]) - zBaseZ;

                    text[1].text = "X: " + (zX);
                    text[2].text = "Y: " + (zY);
                    text[3].text = "Z: " + (zZ);

                    /*text[1].text = "X: " + (zX);
                    text[2].text = "Y: " + (zY);
                    text[3].text = "Z: " + (zZ);*/

                    sp.BaseStream.Flush();
                }
                catch (System.Exception)
                {

                }
            }
            yield return new WaitForSeconds(0.0001f);

            if (zBaselined)
            {
                float oldTiltX = tiltX;
                float oldTiltZ = tiltZ;

                tiltZ = -1 * (zX / zMaxArduinoX) * maxTiltUfoX;  //zX wird max. ca 10 -> zX=1 == 9° Kippung des Sensors
                tiltX = -1 * Input.GetAxis("Vertical") * maxTiltUfoX;    //XRotation von Controller
                                                                         //tiltZ = (zY / zMaxArduinoY) * maxTiltUfoZ;  //ZRotation von Arduino

                //Calc diff between last and current rotation
                float diffX = (tiltX - oldTiltX) * -1;
                float diffZ = (tiltZ - oldTiltZ) * -1;

                //actually rotate
                rotator.Rotate(Vector3.left, diffX, Space.World);
                transform.Rotate(Vector3.up, diffZ, Space.Self);

                //rotation to movement
                //xMove = zRot * moveSpeed * Time.deltaTime;
                //zMove = xRot * moveSpeed * Time.deltaTime;

                //transform.localEulerAngles = new Vector3(-1*tiltZ, 0, tiltX);

                //rotation to movement
                float xMove = tiltX * Time.deltaTime * moveSpeed;
                float zMove = tiltZ * Time.deltaTime * moveSpeed;

                //rotator.Translate(-1*x, 0, -1*z, Space.World);
                rotator.Translate(zMove, 0, -1 * xMove, Space.World);
            }
        }
    }
}
