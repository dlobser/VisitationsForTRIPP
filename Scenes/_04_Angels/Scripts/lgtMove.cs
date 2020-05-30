using UnityEngine;
using System.Collections;

public class lgtMove : MonoBehaviour {

	float speed = 1;
	float pos;
//	public float distance = 100;
	public float startPosition;
	public float endPosition;
	public float power;
//	public float increase = 1.01f;

	public float minSpeed;
	public float maxSpeed;

	public float transitionSpeed;
	
	private float count;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		speed = map ((Mathf.Abs(Mathf.Cos (Time.timeSinceLevelLoad * transitionSpeed))-1)*-1, 0, 1, minSpeed, maxSpeed);
		count += Time.deltaTime * Mathf.Pow(speed,power);

		pos = map (Mathf.Cos (count ), -1, 1, startPosition, endPosition);
//		increase = 1 + (Mathf.Sin (count * .03f) * .003f);
//		speed *= increase;
//		Debug.Log(speed);
		transform.localPosition = new Vector3 (0, 0, pos);
	}

	public static float map(float s, float a1, float a2, float b1, float b2) {
		return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
	}
}
