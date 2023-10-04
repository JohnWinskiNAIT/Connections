using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Connections : MonoBehaviour
{
    public List<GameObject> connectionPoints = new List<GameObject>();

    GameObject activeConnectionPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        float distance = 10000.0f;
        float newDistance;
        GameObject connect1 = null;
        GameObject connect2 = null;

        Connections otherSet = other.gameObject.GetComponent<Connections>();
        if(otherSet != null)
        {
            for (int i = 0; i < connectionPoints.Count; i++)
            {
                for (int j = 0; j < otherSet.connectionPoints.Count; j++)
                {
                    newDistance = Vector3.Distance(connectionPoints[i].transform.position, otherSet.connectionPoints[j].transform.position);
                    
                    if (newDistance < distance)
                    {
                        connect1 = connectionPoints[i];
                        //connect2 = otherSet.connectionPoints[j];
                        distance = newDistance;
                    }
                }
            }

            if (connect1 != null)
            {
                if (activeConnectionPoint != connect1)
                {
                    if (activeConnectionPoint != null)
                    {
                        activeConnectionPoint.SetActive(false);
                    }
                    activeConnectionPoint = connect1;
                    connect1.SetActive(true);
                }                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Connections otherSet = other.gameObject.GetComponent<Connections>();

        if (otherSet != null)
        {
            for (int i = 0; i < connectionPoints.Count; i++)
            {
                for (int j = 0; j < otherSet.connectionPoints.Count; j++)
                {
                    connectionPoints[i].SetActive(false);
                    otherSet.connectionPoints[j].SetActive(false);
                }
            }
        }
    }
}
