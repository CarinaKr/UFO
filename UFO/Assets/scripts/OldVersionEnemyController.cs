using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BackUpEnemyController : MonoBehaviour
{

    public Transform player;
    public float moveSpeed;

    private Ray targetRay;
    private RaycastHit targetHit;


    // Use this for initialization
    void Awake()
    {
        transform.LookAt(player.position);
        targetRay.origin = transform.position;
        targetRay.direction = transform.forward;
    }

    // Update is called once per frame
    /*Stand 21:30, 20.05.2017 AI-Pathfinding Algorithmus WIP
     * void Update () {
        transform.LookAt(player.position);
        targetRay.origin = transform.position;
        targetRay.direction = transform.forward;

        if (Physics.Raycast(targetRay, out targetHit))
        {
            if(targetHit.collider.tag == "Player")
            {
               transform.Translate(Vector3.forward*Time.deltaTime*moveSpeed, Space.Self);
            }
            else
            {
                List<Transform> waypoints = new List<Transform>();

                foreach (Transform child in targetHit.transform)
                {
                    waypoints.Add(child);
                }

                float[] distancesToWaypoints = new float[waypoints.Count];
                for(int i = 0; i < waypoints.Count; i++)
                {
                    targetRay.direction = waypoints[i].position - transform.position;
                    if(Physics.Raycast(targetRay, out targetHit))
                    {
                        if(targetHit.transform.tag == "Waypoint")
                        {
                            distancesToWaypoints[i] = targetHit.distance;
                        }
                        else
                        {
                            distancesToWaypoints[i] = Mathf.Infinity;
                        }
                    }
                }
            }
        }
    }*/

    List<Transform> endPoints = new List<Transform>();
    Dictionary<Transform, float> endPointDistances = new Dictionary<Transform, float>();

    void Update()
    {
        endPointDistances.Clear();
        endPoints.Clear();
        endPoints.Add(gameObject.transform);
        endPointDistances.Add(gameObject.transform, 0);
        LookForPlayer(gameObject.transform, 0);
    }


    private void LookForPlayer(Transform currentPosition, float currentDistance)
    {
        currentPosition.LookAt(player.position);
        targetRay.origin = currentPosition.position;
        targetRay.direction = currentPosition.forward;

        if (Physics.Raycast(targetRay, out targetHit))
        {
            if (targetHit.collider.tag == "Player")
            {
                //Maybe debug this?!
                if (endPointDistances.ContainsKey(targetHit.transform))
                {
                    float dist = endPointDistances[currentPosition] + targetHit.distance;
                    if (dist < endPointDistances[targetHit.transform])
                    {
                        endPointDistances[targetHit.transform] = dist;
                    }
                }
                else
                {
                    endPoints.Add(targetHit.transform);
                    endPointDistances.Add(targetHit.transform, (endPointDistances[currentPosition] + targetHit.distance));
                }
                //possible solution end


                //Algorithmus beenden, Spieler gefunden.


                Debug.DrawLine(currentPosition.position, targetHit.transform.position);
                Debug.Log("Player found");
                //endPointDistances.Remove(currentPosition);

                //bisher: vorzeitiger abbruch - spieler gefunden != kürzester weg gefunden


                //Ursprüngliche Idee: Spieler gefunden = bewegen.
                //transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
            }
            else
            {
                //jeden aktuell erreichbaren waypoint finden
                List<Transform> waypoints = new List<Transform>();

                foreach (Transform child in targetHit.transform)
                {
                    waypoints.Add(child);
                }

                //Distanz zu den aktuellen Waypoints speichern 
                float[] distancesToWaypoints = new float[waypoints.Count];
                for (int i = 0; i < waypoints.Count; i++)
                {
                    targetRay.direction = waypoints[i].position - currentPosition.position;
                    if (Physics.Raycast(targetRay, out targetHit))
                    {
                        if (targetHit.transform.tag == "Waypoint")
                        {
                            Debug.Log("Found WP: " + i + " Distance: " + targetHit.distance);
                            distancesToWaypoints[i] = targetHit.distance;
                        }
                        else
                        {
                            Debug.Log(targetHit.transform.tag);
                            distancesToWaypoints[i] = Mathf.Infinity;
                            Debug.Log(distancesToWaypoints[i]);
                        }
                        endPoints.Add(waypoints[i]);
                        if (!endPointDistances.ContainsKey(waypoints[i]))
                        {
                            endPointDistances.Add(waypoints[i], (endPointDistances[currentPosition] + distancesToWaypoints[i]));
                        }
                    }
                    Debug.DrawLine(currentPosition.position, waypoints[i].position);
                }
                //letzter punkt ist kein endpunkt mehr, sondern ein knotenpunkt
                endPointDistances.Remove(currentPosition);
            }

            //kürzesten finden und funktion mit dem wiederholen
            List<KeyValuePair<Transform, float>> lowestDistance = endPointDistances.OrderBy(pair => pair.Value).Take(5).ToList();
            /* for (int i = 0; i < 5; i++)
             {
                 Debug.Log(i+".: "+lowestDistance[i].Value);
             }*/
            Debug.DrawLine(currentPosition.position, lowestDistance[0].Key.position);
            if (lowestDistance[0].Key == player)
            {
                return;
            }
            else
            {
                LookForPlayer(lowestDistance[0].Key, lowestDistance[0].Value);
            }
        }
    }
}
