using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ON{

public class strobeSprite : MonoBehaviour {

	public Color colorA;
	public Color colorB;
	public Color pauseColor;

	Color oldColorA;
	Color oldColorB;

	Color mix;
	public float speed;
	public float pauseSpeed = 1;
	public SpriteRenderer spriteRenderer;
	public TextMesh textMesh;
	public Image image;
	public MeshRenderer meshRenderer;

	public bool pauseOnPlayingAudio = false;

	AudioSource[] audios;

	float lerpCounter = 0;

    public string colorChannelName;

	void Start () {
		audios = FindObjectsOfType<AudioSource> ();
		oldColorA = colorA;
		oldColorB = colorB;
	}

	bool CheckIfPlaying(){
		bool playing = false;
		for (int i = 0; i < audios.Length; i++) {
			if (audios [i].isPlaying)
				playing = true;
		}
		return playing;
	}
	
	void Update () {
		if (pauseOnPlayingAudio && !CheckIfPlaying () || !pauseOnPlayingAudio) {
			Strobe ();
			UnPauseStrobe ();
		}
		else if (pauseOnPlayingAudio && CheckIfPlaying ()) {
			PauseStrobe ();
			Strobe ();
		}

	}

	void PauseStrobe(){
		if (lerpCounter < pauseSpeed) {
			lerpCounter += Time.deltaTime;
			colorA = Color.Lerp (oldColorA, pauseColor, lerpCounter/pauseSpeed);
			colorB = Color.Lerp (oldColorB, pauseColor, lerpCounter/pauseSpeed);
		}
	}

	void UnPauseStrobe(){
		if (lerpCounter > 0) {
			lerpCounter -= Time.deltaTime;
			colorA = Color.Lerp (oldColorA, pauseColor, lerpCounter/pauseSpeed);
			colorB = Color.Lerp (oldColorB, pauseColor, lerpCounter/pauseSpeed);
		}
	}

	void Strobe(){
		if (spriteRenderer != null) {
			mix = Color.Lerp (colorA, colorB, (Mathf.Sin (Time.timeSinceLevelLoad * speed) + 1) * .5f);
			mix.a = spriteRenderer.color.a;
			spriteRenderer.color = mix;
		}
		if (image != null) {
			mix = Color.Lerp (colorA, colorB, (Mathf.Sin (Time.timeSinceLevelLoad * speed) + 1) * .5f);
			mix.a = image.color.a;
			image.color = mix;
		}
		if (meshRenderer != null) {
			mix = Color.Lerp (colorA, colorB, (Mathf.Sin (Time.timeSinceLevelLoad * speed) + 1) * .5f);
           
			mix.a = meshRenderer.sharedMaterial.color.a;
            if (colorChannelName.Length > 0)
                meshRenderer.sharedMaterial.SetColor(colorChannelName, mix);
            else
			    meshRenderer.sharedMaterial.color = mix;
		}
		if (textMesh != null) {
			mix = Color.Lerp (colorA, colorB, (Mathf.Sin (Time.timeSinceLevelLoad * speed) + 1) * .5f);
			mix.a = textMesh.color.a;
			textMesh.color = mix;
		}
	}
}


}