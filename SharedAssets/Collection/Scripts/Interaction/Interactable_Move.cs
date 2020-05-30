using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class Interactable_Move : Interactable
{
    //explodes an object when clicked

    public Vector3 moveTo;

    public override void HandleHover()
    {
        this.transform.position = moveTo;
    }
	
}


}