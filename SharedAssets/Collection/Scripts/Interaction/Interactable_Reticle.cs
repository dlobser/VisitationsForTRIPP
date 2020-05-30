using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class Interactable_Reticle : Interactable
{
    public GameObject reticle;
    GameObject thisReticle;
    public Interactable[] interactable;
    bool interactableTriggered = false;
    public bool deactivateOnTrigger = false;
    public bool swap;
    public bool swapper;
    public float returnSpeed = 1;

	public override void HandleHover(){
		base.HandleHover();

        if (thisReticle == null){
            thisReticle = reticle;// Instantiate(reticle,this.transform);
        }

        if(!interactableTriggered && hoverCounter>=hoverTime){
            if (interactable != null)
                foreach (Interactable i in interactable)
                {
                    if (!i.gameObject.activeInHierarchy)
                        i.gameObject.SetActive(true);
                    i.HandleTrigger();
                }
            interactableTriggered = true;
        }

        //thisReticle.transform.position = Camera.main.transform.position;
        //thisReticle.transform.LookAt(this.transform.position);
        if (hoverCounter < hoverTime)
        {
            thisReticle.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetFloat("_Percent", swap ?  hoverCounter/hoverTime : 1 - (hoverCounter / hoverTime) );
            if (deactivateOnTrigger)
                thisReticle.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            if(deactivateOnTrigger)
                thisReticle.transform.GetChild(0).gameObject.SetActive(false);
            //if (swap)
                //swapper = !swapper;
        }
    }

    public override void HandleTrigger()
    {
        base.HandleTrigger();
        if (interactable != null)
            foreach (Interactable i in interactable)
            {
                if (!i.gameObject.activeInHierarchy)
                    i.gameObject.SetActive(true);
                i.HandleTrigger();
            }
        interactableTriggered = true;
    }

    public override void HandleExit()
    {
        base.HandleExit();
        if (thisReticle == null)
        {
            thisReticle = reticle;// Instantiate(reticle,this.transform);
        }
        thisReticle.transform.GetChild(0).gameObject.SetActive(false);
        hoverCounter = 0;
    }

    private void OnDisable()
    {
        hoverCounter = 0;
    }

    public override void HandleEnter()
    {
        base.HandleExit();
        if (thisReticle == null)
        {
            thisReticle = reticle;// Instantiate(reticle,this.transform);
        }
        thisReticle.transform.GetChild(0).gameObject.SetActive(true);
    }

    public override void HandleWaiting(){
		base.HandleWaiting();
        if (thisReticle != null)
        {
            if (hoverCounter >= 0)
            {
                hoverCounter -= returnSpeed * Time.deltaTime;
                //if(deactivateOnTrigger)
                    //thisReticle.transform.GetChild(0).gameObject.SetActive(true);
                thisReticle.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetFloat("_Percent", swap ? (hoverCounter / hoverTime) : (1 - (hoverCounter / hoverTime)));
            }
            else
            {
                //thisReticle.SetActive(false);
                if (deactivateOnTrigger)
                    thisReticle.transform.GetChild(0).gameObject.SetActive(false);
                interactableTriggered = false;
            }
                //Destroy(thisReticle);
        }
	}

}


}