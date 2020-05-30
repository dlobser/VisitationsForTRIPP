using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class Interactable_PlaySound : Interactable
{
    public AudioSource sound;
    public AudioClip clip;
    public float randomizePitch = 0;

	public override void HandleEnter()
	{
        base.HandleEnter();
        if (clip != null)
            sound.clip = clip;
        sound.pitch = Random.Range(1 - randomizePitch, 1 + randomizePitch);
        sound.Play();
	}
    public override void HandleTrigger()
    {
        base.HandleTrigger();
        if (clip != null)
            sound.clip = clip;
        sound.pitch = Random.Range(1 - randomizePitch, 1 + randomizePitch);
        sound.Play();
    }
}

}