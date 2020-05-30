using UnityEngine;
using System.Collections;

public class spiral : MonoBehaviour {
	public GameObject thing;
	public int amount = 100;
	public float xy = 5;
	public float z = 1;
	public float freq = .5f;
	public float randAmount = .2f;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < amount; i++) {
			GameObject t = Instantiate(thing,new Vector3(Random.value*randAmount+Mathf.Sin (i*freq)*xy,Random.value*randAmount+Mathf.Cos (i*freq)*xy,i*z),
			            Quaternion.identity) as GameObject;
			t.transform.Rotate(0,0,Random.value*360);
            t.transform.parent = this.transform;
		}
        this.transform.Translate(Vector3.forward * -3);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < amount; i++)
        {
            Transform t = this.transform.GetChild(i);
            t.Rotate(0, 0, Random.value * 360);
        }
    }
}
