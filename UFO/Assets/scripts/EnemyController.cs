using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Haben wir in NWI gelernt #DjikstraAlgorithmus
public class EnemyController : MonoBehaviour {

    //setup variables
    public Transform player;
    public float moveSpeed;

    private Ray targetRay;
    private RaycastHit targetHit;


	// Use this for initialization
	void Awake () {
        transform.LookAt(player.position);
        targetRay.origin = transform.position;
        targetRay.direction = transform.forward;
	}

    //List<Endpoints> wahrscheinlich nicht gebraucht, eventuell später nützlich?
    //List<Transform> endPoints = new List<Transform>();

    /* Dictionary storing Transforms (Waypoints) as keys and mapping a distance to them.
     * The idea is to always have every current possible endpoint and storing the distance from
     * the origin (enemy position) towards that endpoint as a cost. The path with the lowest
     * cost will always be the shortest path to the player. (Djikstra Algorithmus)
    */
    Dictionary<Transform, float> endPointDistances = new Dictionary<Transform, float>();

    /* On every Update, we have to clear the dictionary, to ensure there are no duplicate keys
     * Then we have to add the currents gameObjects transforms as the origin and start looking for the player
    */
    void Update()
    {
        endPointDistances.Clear();
        //endPoints.Clear();
        //endPoints.Add(gameObject.transform);
        endPointDistances.Add(gameObject.transform, 0);
        LookForPlayer(gameObject.transform, 0);
    }

   /*
    * Do everything
    */
    private void LookForPlayer(Transform currentPosition, float currentDistance)
    {
        /*
         * Turn towards the player and direct a raycast towards him
         */
        currentPosition.LookAt(player.position);
        targetRay.origin = currentPosition.position;
        targetRay.direction = currentPosition.forward;

        if (Physics.Raycast(targetRay, out targetHit))
        {
            //if it hits the player, add the player as endpoint and add the currentDistance to the path. 
            //problem: returning eventhough we might find a shorter path 
            if (targetHit.collider.tag == "Player")
            {
                
                //endPoints.Add(targetHit.transform);
                endPointDistances.Add(targetHit.transform, (endPointDistances[currentPosition] + targetHit.distance));
                //Algorithmus beenden, Spieler gefunden.
                
                //Debugging, Testing Rays
                Debug.DrawLine(currentPosition.position, targetHit.transform.position);
                Debug.Log("Player found");

                //endPointDistances.Remove(currentPosition);

                //early break condition
                return;
            }
            //else find every waypoint, of hit obstacle/asteroid
            else
            {
                //List for the waypoints
                List<Transform> waypoints = new List<Transform>();

                foreach (Transform child in targetHit.transform)
                {
                    waypoints.Add(child);
                }

                //Calc dist to waypoints of hit asteroid
                float[] distancesToWaypoints = new float[waypoints.Count];
                for (int i = 0; i < waypoints.Count; i++)
                {
                    //send a raycast from the currentPosition towards each of the waypoints around the asteroid
                    //and figure out the distance to it (either the distance or infinity)
                    //add a new endpoint to the dictionary, in case we dont have the waypoint as
                    //endpoint yet (otherwise, theres a shorter path to it already) 
                    //TO-DO: If they already exist, compare first, then override them
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
                      //  endPoints.Add(waypoints[i]);
                        if (!endPointDistances.ContainsKey(waypoints[i]))
                        {
                            endPointDistances.Add(waypoints[i], (endPointDistances[currentPosition] + distancesToWaypoints[i]));
                        }
                    }
                    Debug.DrawLine(currentPosition.position, waypoints[i].position);
                }
                //when we "leave" the current endpoint its no endpoint anymore
                endPointDistances.Remove(currentPosition);
            }

            //find the lowestDistance in the Dictionary
            List<KeyValuePair<Transform, float>> lowestDistance = endPointDistances.OrderBy(pair => pair.Value).Take(5).ToList();
           /* for (int i = 0; i < 5; i++)
            {
                Debug.Log(i+".: "+lowestDistance[i].Value);
            }*/
            Debug.DrawLine(currentPosition.position, lowestDistance[0].Key.position);
            //if the lowestDistance Transforms equal the players transforms, were done, else repeat the algorithm with lowestDistance transform + distance
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
