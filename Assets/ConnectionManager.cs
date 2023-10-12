using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] GameObject originCP, destinationCP, connect1, connect2, closestObject, controlledObject;

    [SerializeField] List<GameObject> targetObjects;

    float distance;
    float newDistance;

    // Start is called before the first frame update
    void Start()
    {
        distance = 10000.0f;
    }

    // Update is called once per frame
    void Update()
    {
        controlledObject.transform.position = transform.position;
        
        distance = 10000.0f;

        connect1 = null;
        connect2 = null;

        for (int i = 0; i < targetObjects.Count; i++)
        {
            newDistance = Vector3.Distance(controlledObject.transform.position, targetObjects[i].transform.position);

            if (newDistance < distance)
            {

                closestObject = targetObjects[i];
                distance = newDistance;
            }
            Debug.Log("ClosestObject = " + closestObject.name);
        }

        distance = 10000.0f;

        if (closestObject != null)
        {
            Connections masterset = controlledObject.GetComponent<Connections>();
            Connections otherSet = closestObject.GetComponent<Connections>();
            if (otherSet != null)
            {
                for (int i = 0; i < masterset.connectionPoints.Count; i++)
                {
                    for (int j = 0; j < otherSet.connectionPoints.Count; j++)
                    {
                        newDistance = Vector3.Distance(masterset.connectionPoints[i].transform.position, otherSet.connectionPoints[j].transform.position);

                        if (newDistance < distance)
                        {
                            connect1 = masterset.connectionPoints[i];
                            connect2 = otherSet.connectionPoints[j];
                            distance = newDistance;
                        }
                    }
                }

                if (connect1 != null)
                {
                    if (originCP != connect1)
                    {
                        if (originCP != null)
                        {
                            originCP.SetActive(false);
                        }
                        originCP = connect1;
                        connect1.SetActive(true);
                    }
                }
                if (connect2 != null)
                {
                    if (destinationCP != connect2)
                    {
                        if (destinationCP != null)
                        {
                            destinationCP.SetActive(false);
                        }
                        destinationCP = connect2;
                        connect2.SetActive(true);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool found = false;

        Rigidbody rb = other.GetComponentInParent<Rigidbody>();

        if (rb != null && rb.gameObject != controlledObject)
        {
            for (int i = 0; i < targetObjects.Count && !found; i++)
            {
                if (rb.gameObject == targetObjects[i])
                {
                    found = true;
                }
            }

            if (!found)
            {
                targetObjects.Add(rb.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Connections masterset = controlledObject.GetComponent<Connections>();
        Connections otherSet = other.gameObject.GetComponent<Connections>();

        if (otherSet != null)
        {
            for (int i = 0; i < masterset.connectionPoints.Count; i++)
            {
                for (int j = 0; j < otherSet.connectionPoints.Count; j++)
                {
                    masterset.connectionPoints[i].SetActive(false);
                    otherSet.connectionPoints[j].SetActive(false);
                }
            }
        }

        Rigidbody rb = other.GetComponentInParent<Rigidbody>();

        if (rb != null)
        {
            bool found = false;

            for (int i = 0; i < targetObjects.Count && !found; i++)
            {
                if (rb.gameObject == targetObjects[i])
                {
                    found = true;
                    targetObjects.RemoveAt(i);
                }
            }
        }
    }
}
