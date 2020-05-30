using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class Interactable_SkipAudio : Interactable
{
    PlayRandomAudio randomAudio;
    // Start is called before the first frame update
    void Start()
    {
        randomAudio = FindObjectOfType<PlayRandomAudio>();
    }

    public override void HandleTrigger()
    {
        base.HandleTrigger();
        FindObjectOfType<UI_Manager>().transform.GetChild(0).GetComponent<PlayRandomAudio>().Skip();
    }
}


}