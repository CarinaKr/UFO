using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using UnityEngine.UI;

public class ArduinoTest : MonoBehaviour
{
    private float diffDeg;
    private int count;
    private float oldDeg;
    private float degree;
    public Boolean isController;

    public Transform rotator;
    public Transform centerPoint;
    [SerializeField]
    private static String comName = "COM6";

    public Text[] text;
    public Text buttonText;
    public float moveSpeed;
    public float maxTiltUfoX, maxTiltUfoZ;
    public float maxTiltBoardX, maxTiltBoardZ;
    public float tiltTolerance;

    SerialPort sp;
    private float zBaseX, zBaseY, zBaseZ;
    private float zX, zY, zZ;
    private String zIn;
    private String[] zValues;
    private bool zBaselined;
    private float zMaxArduinoX, zMaxArduinoY;
    private float tiltX;
    private float tiltZ;

    void Start()
    {
        if(!isController)
        {
            sp = new SerialPort(comName, 9600);
            sp.Open();
            sp.ReadTimeout = 1;
        }
        zMaxArduinoX = maxTiltBoardX / 9;
        zMaxArduinoY = maxTiltBoardZ / 9;

        //StartCoroutine(UpdateGyro());
        baseline();
    }

    void Update()
    {
        if (!isController)
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
        } 
        else
        {
            zX = Input.GetAxis("Horizontal");
        }
        if (zBaselined)
        {
            float oldTiltX = tiltX;
            float oldTiltZ = tiltZ;

            tiltZ = -1 * (zX / zMaxArduinoX) * maxTiltUfoX;  //zX wird max. ca 10 -> zX=1 == 9° Kippung des Sensors
            degree = zX * 9;

            //Calc diff between last and current rotation

            float temp = transform.rotation.eulerAngles.x - 270;
            //Debug.Log("Grad Gemessen: " + degree);

            //umrechnen 0-360° in -180 bis 180 grad
            float ufoRotZ = rotator.transform.eulerAngles.z;
            if(ufoRotZ > 180)
            {
                ufoRotZ = -1 * (360 - ufoRotZ);
            }
            //actual movement
            if (maxTiltUfoZ >= ufoRotZ && (-1 * maxTiltUfoZ) <= ufoRotZ)
            {
                //either use this float + inner if OR use the outcommented else if below. check which gives better results
                float diff = ufoRotZ > 0 ? maxTiltUfoZ - ufoRotZ : maxTiltUfoZ + ufoRotZ;
                if (ufoRotZ > 18 && degree > diff)
                {
                    degree = diff;
                }
                else if (ufoRotZ < -18 && degree < (-1 * diff))
                {
                    degree = -1 * diff;
                }
                diffDeg = degree - oldDeg; // <---- I dont know why. But with the Controller, this seems to work. 
                rotator.transform.Rotate(Vector3.forward, diffDeg * Time.deltaTime, Space.Self);
               // Debug.Log("Uforot: " + ufoRotZ + " Degrees: " + degree);
            }
            //else if (ufoRotZ < (-1*maxTiltUfoZ))
            //{
            //    rotator.transform.eulerAngles = new Vector3(0, 0, maxTiltUfoZ * -1);
            //}
            //else if (ufoRotZ > maxTiltUfoZ)
            //{
            //    rotator.transform.eulerAngles = new Vector3(0, 0, maxTiltUfoZ);
            //}

            //rotation to movement
            float xMove = tiltX * Time.deltaTime * moveSpeed;
            float zMove = tiltZ * Time.deltaTime * moveSpeed;
            //Debug.Log(oldDeg - degree);
            oldDeg = degree;
            rotator.RotateAround(centerPoint.position, Vector3.up, xMove);
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

    //IEnumerator UpdateGyro()
    //{
    //    while (true)
    //    {
    //        if (sp.IsOpen)
    //        {
    //            try
    //            {

    //                zIn = sp.ReadLine();
    //                //yield return new WaitForSeconds(0.0001f);
    //                text[0].text = "Gesamt" + zIn + "end of text";

    //                zValues = zIn.Split(';');
    //                zX = float.Parse(zValues[0]) - zBaseX;
    //                zY = float.Parse(zValues[1]) - zBaseY;
    //                zZ = float.Parse(zValues[2]) - zBaseZ;

    //                text[1].text = "X: " + (zX);
    //                text[2].text = "Y: " + (zY);
    //                text[3].text = "Z: " + (zZ);

    //                /*text[1].text = "X: " + (zX);
    //                text[2].text = "Y: " + (zY);
    //                text[3].text = "Z: " + (zZ);*/
    //                Debug.Log("Daten: " + zIn);
    //                sp.BaseStream.Flush();
    //            }
    //            catch (System.Exception)
    //            {

    //            }
    //        }

    //        if (zBaselined)
    //        {
    //            float oldTiltX = tiltX;
    //            float oldTiltZ = tiltZ;

    //            tiltZ = -1 * (zX / zMaxArduinoX) * maxTiltUfoX;  //zX wird max. ca 10 -> zX=1 == 9° Kippung des Sensors

    //            //Calc diff between last and current rotation
    //            float diffZ = (tiltZ - oldTiltZ) * -1;

    //            //actually rotate
    //            transform.Rotate(Vector3.up, diffZ, Space.Self);

    //            //rotation to movement
    //            //xMove = zRot * moveSpeed * Time.deltaTime;
    //            //zMove = xRot * moveSpeed * Time.deltaTime;

    //            //transform.localEulerAngles = new Vector3(-1*tiltZ, 0, tiltX);

    //            //rotation to movement
    //            float xMove = tiltX * Time.deltaTime * moveSpeed;
    //            float zMove = tiltZ * Time.deltaTime * moveSpeed;

    //            //rotator.Translate(-1*x, 0, -1*z, Space.World);
    //            //rotator.Translate(zMove, 0, -1 * xMove, Space.World);
    //            rotator.RotateAround(centerPoint.position, Vector3.up, xMove);
    //        }
    //        yield return new WaitForSeconds(0.000000000000000000001f);
    //    }
    //}
}
