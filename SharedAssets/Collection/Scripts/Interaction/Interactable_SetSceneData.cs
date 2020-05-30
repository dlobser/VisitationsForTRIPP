using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_SetSceneData : Interactable
{

    SceneInfo sceneInfo;
    public bool setTime;
    public bool setScene;
    public bool setVO;
    public float time;
    public int scene;

    public GameObject[] buttons;

    //public override void HandleHover()
    //{
    //    if(clicked>.5f){
    //        HandleTrigger();
    //    }
    //}

	public override void HandleTrigger()
	{
        base.HandleTrigger();
        if (sceneInfo == null)
        {
            sceneInfo = FindObjectOfType<SceneInfo>();
        }
        if (setVO)
        {
            sceneInfo.useVO = !sceneInfo.useVO;
        }
        if (setTime)
        {
            //print("Setting Time");
            sceneInfo.time = time;
        }
        if (setScene)
        {
            int c = 0;
            foreach (GameObject g in buttons)
            {
                if (g.transform.GetChild(0).gameObject.activeInHierarchy)
                    c++;
            }


            if (c > 1 && this.transform.parent.GetChild(0).gameObject.activeInHierarchy) {
                sceneInfo.scenes[scene] = sceneInfo.scenes[scene] ? false : true;
               
            }
            else if (!this.transform.parent.GetChild(0).gameObject.activeInHierarchy)
            {
                sceneInfo.scenes[scene] = sceneInfo.scenes[scene] ? false : true;

            }
            else
            {
                this.transform.parent.GetChild(1).GetComponent<Interactable_EnableDisable>().Swap();
                this.transform.parent.GetChild(0).gameObject.SetActive(true);

            }
        }
    


	}
}
