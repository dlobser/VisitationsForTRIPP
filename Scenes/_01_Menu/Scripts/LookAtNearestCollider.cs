using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class LookAtNearestCollider : MonoBehaviour
{
    Collider[] colliders;
    public GameObject target;
    public GameObject origin;
    public float minDist = 1;

    void Start()
    {
        origin = Camera.main.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (origin == null)
        {
            origin = Camera.main.transform.GetChild(0).gameObject;
        }
        colliders = FindObjectsOfType<Collider>();
        if (colliders.Length > 0)
        {
            target.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            float min = 1e6f;
            int which = 0;
            float dist;
            for (int i = 0; i < colliders.Length; i++)
            {
                dist = Vector3.Distance(origin.transform.position, colliders[i].transform.position);
                if (dist < min)
                {
                    which = i;
                    min = dist;
                }
            }
            target.transform.position = Vector3.Lerp(origin.transform.position, colliders[which].transform.position, .5f);
            dist = Vector3.Distance(origin.transform.position, colliders[which].transform.position);

            if (dist < minDist)
            {
                target.transform.LookAt(origin.transform.position);
                target.transform.localScale = new Vector3(1, 1, dist);
            }
            else
            {
                target.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

            }
        }
        else
        {
            target.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
    }
}


}