using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

	public class CopyXFormsSelectiveLerp : MonoBehaviour {

	  	public Transform target;

		public bool copyPosition = true;
		public bool copyRotation = true;
		public bool copyScale = true;

        public Vector3 positionOffset;

		public Vector3 positionMultiplier = Vector3.one;
		public Vector3 rotationMultiplier = Vector3.one;
		public Vector3 scaleMultiplier = Vector3.one;

		public bool copyXFormsFromMainCamera;

		public int averageAmount = 1;
		int avg = 1;

		List<Quaternion> averageQuat;

		public float snapThreshhold = .05f;
	   
		void Start () {
			if (copyXFormsFromMainCamera)
				target = Camera.main.transform;
			averageQuat = new List<Quaternion> ();
			
			ForceUpdate (false);

            if (copyPosition)
                this.transform.position = Vector3.Lerp(this.transform.position, target.position + positionOffset, 1);
            if (copyRotation)
                this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, target.localEulerAngles, 1);
            if (copyScale)
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, target.localScale, 1);
		}

		public void ForceUpdate(bool add){
			//if (Vector3.Distance (this.transform.position, target.position) > snapThreshhold) {
			//	if (target != null)
			//	{
			//		if(copyPosition)
			//			this.transform.position = Vector3.Scale(target.transform.position,positionMultiplier);
			//		if (copyRotation)
			//			this.transform.localEulerAngles = Vector3.Scale(target.transform.rotation.eulerAngles,rotationMultiplier);
			//		if (copyScale)
			//			this.transform.localScale = Vector3.Scale(target.transform.localScale,scaleMultiplier);
			//	}
			//	averageQuat.Clear ();
			//}
			if (target != null)
			{
				if(copyPosition)
					this.transform.position = Vector3.Lerp(this.transform.position,target.position + positionOffset,1f/(float)averageAmount);
				if (copyRotation)
                    this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles,target.localEulerAngles,1 / averageAmount);
				if (copyScale)
                    this.transform.localScale = Vector3.Lerp(this.transform.localScale,target.localScale,1 / averageAmount);
			}
			else
				target = Camera.main.transform;
			//if (avg < averageAmount && add)
				//avg++;
		
		}

		

		void FixedUpdate () {
            if(Time.timeSinceLevelLoad>5)
			    ForceUpdate (true);
	    }
	}
}