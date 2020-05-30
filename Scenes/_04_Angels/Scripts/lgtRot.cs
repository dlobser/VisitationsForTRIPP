using UnityEngine;
using System.Collections;

namespace ON{

public class lgtRot : MonoBehaviour {

	public float speed = 1;
	public float distance = 100;
	public float increase = 1.01f;
	
	private float count;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;
		increase = 1 + (Mathf.Sin (count * .04f) * .002f);
		speed *= increase;
		Transform kid = transform.GetChild (0);
		kid.transform.localPosition = new Vector3 (Mathf.Sin (count*speed)*22, Mathf.Cos (count*speed)*22, 9+Mathf.Cos (count*speed*Mathf.PI*1.2f)*2);
//		transform.localEulerAngles = new Vector3 (0, 0, distance+count * speed*distance);
//		transform.localScale = new Vector3(Mathf.Sin(count),1,1);
	}
}


}