using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateIfNotExisting : MonoBehaviour
{
    public SceneInfo scene;

    void OnEnable()
    {
        if (FindObjectOfType<SceneInfo>() == null)
            Instantiate (scene);
    }

    void Update()
    {
        
    }
}
