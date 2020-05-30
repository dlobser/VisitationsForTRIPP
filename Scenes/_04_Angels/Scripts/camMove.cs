using UnityEngine;
using System.Collections;

public class camMove : MonoBehaviour {

	public float speed = 1;
	public float distance = 100;

	private float count;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;
		transform.localPosition = new Vector3 (0, 0, distance+Mathf.Sin (count * speed)*distance*.8f);
	}
}
