using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class FadeAudio : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource source;
    public Vector2 pauseTime;
    public Vector2 musicFadeTime;
    bool play;

    void Start()
    {
        StartCoroutine(PlayAudio());
    }


    IEnumerator PlayAudio(){
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
        float count = 0;
        while(count<musicFadeTime.x){
            count += Time.deltaTime;
            source.volume = count / musicFadeTime.x;
            yield return null;
        }
        yield return new WaitForSeconds(source.clip.length-musicFadeTime.x-musicFadeTime.y);
        count = 0;
        while (count < musicFadeTime.y)
        {
            count += Time.deltaTime;
            source.volume = 1-(count / musicFadeTime.x);
            yield return null;
        }
        yield return new WaitForSeconds(Random.Range(pauseTime.x, pauseTime.y));
        StartCoroutine(PlayAudio());
    }
}


}