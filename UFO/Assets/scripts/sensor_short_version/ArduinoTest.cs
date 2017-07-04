using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using UnityEngine.UI;

public class ArduinoTest : MonoBehaviour
{
    public float tiltSpeed;
    public Transform targetPoint;
    public Boolean isController;

    public Transform rotator;
    public Transform centerPoint;
    [SerializeField]
    private static String comName = "COM7";

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

    public float neigung;

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

        StartCoroutine(UpdateGyro());
        //baseline();
    }

    void Update()
    {
        if (zBaselined)
        {
            float oldTiltX = tiltX;
            float oldTiltZ = tiltZ;

            Vector3 targetDir = targetPoint.position - transform.position;
            //tiltZ =(float) Math.Tan(zX * 9) * targetDir.magnitude;
            double rad = zX * 9 * (Math.PI / 180);
            tiltZ =  -1 * (float) (Math.Tan(rad) * 4.21f);
            Debug.Log("Tan: " + Math.Tan(rad));
            Debug.Log("Länge: " + targetDir.magnitude);
            Debug.Log("TiltZ: " + tiltZ);
            //tiltZ = -1 * (zX / zMaxArduinoX) * maxTiltUfoX;  //zX wird max. ca 10 -> zX=1 == 9° Kippung des Sensors

            targetDir += transform.right * tiltZ;
            var newDir = Quaternion.LookRotation(targetDir, -1 * (transform.position - centerPoint.transform.position));
            float step = tiltSpeed * Time.deltaTime;
            Debug.DrawRay(rotator.transform.position, targetDir * 5, Color.red);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDir, step);
            float zMove = tiltZ * Time.deltaTime * moveSpeed;

            rotator.RotateAround(centerPoint.position, Vector3.up, zMove);
        }
        //double rad = neigung * 9 * (Math.PI / 180);
        //tiltZ = (float)Math.Tan(rad) * 4.21f;
        //Vector3 targetDir = targetPoint.position - transform.position;
        //targetDir += transform.right * tiltZ;
        //var newDir = Quaternion.LookRotation(targetDir, -1 * (transform.position - centerPoint.transform.position));
        //float step = tiltSpeed * Time.deltaTime;
        //Debug.DrawRay(rotator.transform.position, targetDir * 5, Color.red);
        //transform.rotation = Quaternion.Slerp(transform.rotation, newDir, step);
        //float zMove = tiltZ * Time.deltaTime * moveSpeed;

        //rotator.RotateAround(centerPoint.position, Vector3.up, zMove);
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
            if (!isController)
            {
                if (sp.IsOpen)
                {
                    try
                    {
                        zIn = sp.ReadLine();
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
                        //Debug.Log("Daten: " + zIn);
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
            yield return new WaitForSeconds(0.25f);
        }
    }
}
