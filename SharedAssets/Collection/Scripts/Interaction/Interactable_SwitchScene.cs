using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ON{

public class Interactable_SwitchScene : Interactable
{

    public string scene;

    public override void HandleHover()
    {
       
    }

	public override void HandleTrigger()
	{
        SceneManager.LoadScene(scene);
	}
}


}