using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    GameObject point1;
    GameObject point2;
    GameObject currentPoint;

    PlayerMovement player;
    TeleporterPoint teleporterPoint1;
    TeleporterPoint teleporterPoint2;
    int pointID;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        //teleporterPoint1 = GameObject.Find("TeleporterP1").GetComponent<TeleporterPoint>();
        //teleporterPoint2 = GameObject.Find("TeleporterP2").GetComponent<TeleporterPoint>();

        GetChildGameobjects();
    }

    private void GetChildGameobjects()
    {
        teleporterPoint1 = gameObject.transform.GetChild(0).GetComponent<TeleporterPoint>();
        teleporterPoint2 = gameObject.transform.GetChild(1).GetComponent<TeleporterPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        TeleportPlayer();
        Debug.Log("pointId: " + pointID);
    }

    private void TeleportPlayer()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("TeleportPlayer");

            if (pointID == 1 && teleporterPoint1.GetPlayerDetected())
            {
                player.TeleportPlayer(teleporterPoint2.transform.position);
                Debug.Log("TeleporterP1");
            }
            else if(pointID == 2 && teleporterPoint2.GetPlayerDetected())
            {
                Debug.Log("TeleporterP2" );
                player.TeleportPlayer(teleporterPoint1.transform.position);
            }
        }
    }

    public void SetPointId(int pointID)
    {
        this.pointID = pointID;
    }


}
