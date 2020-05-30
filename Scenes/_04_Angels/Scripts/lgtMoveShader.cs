using UnityEngine;
using System.Collections;

public class lgtMoveShader : MonoBehaviour {

	private GameObject light1;
	private GameObject light2;


	// Use this for initialization
	void Start () {
		light1 = GameObject.Find ("Light1");
		light2 = GameObject.Find ("camGroup/lights/Light2");

	}
	
	// Update is called once per frame
	void Update () {
//		int ch = transform.childCount;
//		for (int i = 0; i < ch; i++) {
//			Transform child = transform.GetChild(i);

			GetComponent<Renderer>().sharedMaterial.SetVector("_Lgt1", light1.transform.position);
			GetComponent<Renderer>().sharedMaterial.SetVector("_Lgt2", light2.transform.position);

//		}
	}
}
