using UnityEngine;
using System.Collections;

public class toneCopies : MonoBehaviour {

	public int amount = 10;
	public AudioClip clip;
	public float spacing = 200;

	private GameObject[] tones;

	private float[] volume;
	private float[] pitch;

	private float count = 0;

	// Use this for initialization
	void Start () {
		tones = new GameObject[amount];
		pitch = new float[amount];
		for (int i = 0; i < amount; i++) {
			tones[i] = Instantiate(new GameObject()) as GameObject;
			tones[i].transform.localPosition = new Vector3(Mathf.Sin (i)*3.0f,0,-5+(i*1.0f/amount)*spacing);
			tones[i].AddComponent<AudioSource>();
			tones[i].GetComponent<AudioSource>().clip = clip;
			AudioClip cl = tones[i].GetComponent<AudioSource>().clip;
			tones[i].GetComponent<AudioSource>().loop=true;
			tones[i].GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Custom;
			tones[i].GetComponent<AudioSource>().dopplerLevel = 0;
			tones[i].GetComponent<AudioSource>().maxDistance = 6;
			tones[i].GetComponent<AudioSource>().volume = .5f;
			tones[i].GetComponent<AudioSource>().Play();
//			tones[i].audio.clip.

		}

	}
	
	// Update is called once per frame
	void Update () {
		count += Time.deltaTime*.2f;
		for (int i = 0; i < amount; i++) {
			tones[i].GetComponent<AudioSource>().pitch = (1.2f+(Mathf.Sin(i+count)*.5f));
		}
//		count += Time.deltaTime*.2f;
	
	}
}
