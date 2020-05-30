using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

	public class CopyXForms : MonoBehaviour {

	  	public Transform target;

		public bool copyPosition = true;
		public bool copyRotation = true;
		public bool copyScale = true;

		public bool copyXFormsFromMainCamera;
	   
		void Awake () {
			if (copyXFormsFromMainCamera && Camera.main!=null)
				target = Camera.main.transform;
            ForceUpdate();
		}

		public void ForceUpdate(){
            if (Camera.main != null && target == null && !copyXFormsFromMainCamera)
                target = Camera.main.transform;
            else if (copyXFormsFromMainCamera)
            {
                if (Camera.main != null)
                    target = Camera.main.transform;
            }
			if (target != null)
			{
				if(copyPosition)
					this.transform.position = target.position;
				if (copyRotation)
					this.transform.rotation = target.rotation;
				if (copyScale)
					this.transform.localScale = target.lossyScale;
			}
            else if(Camera.main != null)
            {
                target = Camera.main.transform;
            }


        }

		void FixedUpdate () {
			ForceUpdate ();
	    }
	}
}