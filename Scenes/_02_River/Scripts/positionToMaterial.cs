using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class positionToMaterial : MonoBehaviour
{

    public string channel;
    public Material material;
    public Transform target;
    public float wVec;
    Vector4 vector;

    void Start()
    {
        
    }

    void Update()
    {
        vector.Set(target.position.x, target.position.y, target.position.z, wVec);
        material.SetVector(channel, vector);
    }
}
