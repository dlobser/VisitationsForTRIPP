using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSceneObjectToStartButton : MonoBehaviour
{
    bool init = false;
    int which = 0;

    void Start()
    {
        which = GetComponent<Interactable_Reticle>().interactable.Length - 1;
        GetComponent<Interactable_Reticle>().interactable[which] = FindObjectOfType<UI_Manager>();
    }

    void Update()
    {
        if (GetComponent<Interactable_Reticle>().interactable[which] == null && !init)
        {
            init = true;
            GetComponent<Interactable_Reticle>().interactable[which] = FindObjectOfType<UI_Manager>();

        }
    }
}
