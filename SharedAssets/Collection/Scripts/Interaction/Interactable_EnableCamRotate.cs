using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class Interactable_EnableCamRotate : Interactable
{
    //explodes an object when clicked

    public CorrectCameraParentAim cam;

    public override void HandleHover()
    {
        if(clicked>.5f){
            HandleTrigger();
        }
    }

	public override void HandleTrigger()
	{
		base.HandleTrigger();
        cam.enabled = true;
	}
}


}