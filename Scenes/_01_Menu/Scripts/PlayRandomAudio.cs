using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class PlayRandomAudio : MonoBehaviour
{
    //public AudioClip[] clips;
    public List<AudioClip> clipList;

    public float timeBetweenAudio;
    float timeBetweenCounter;

    AudioSource audi;
    public int which = 0;

    public float audioLength;
    public float lengthCounter;

    bool waiting = false;

    public string path;

    Object[] audioClips;

    public bool skipIntro = false;

    public void Skip()
    {
        which = 1;
        skipIntro = true;
    }

    void Start()
    {
        audi = GetComponent<AudioSource>();
        timeBetweenCounter = timeBetweenAudio * .9f;
        //audioClips = Resources.LoadAll(path, typeof( AudioClip));
        //Debug.Log("HI");
        //Debug.Log(audioClips.Length);
        //for (int i = 0; i < audioClips.Length; i++)
        //{
        //    clipList.Add(audioClips[i] as AudioClip);
        //}
        Shuffle(clipList);
    }

    void Update()
    {
        if(!waiting)
            timeBetweenCounter += Time.deltaTime;
        if (timeBetweenCounter > timeBetweenAudio)
        {
            audi.clip = clipList[which];
            audioLength = audi.clip.length;
            which++;
            if (which > clipList.Count-1)
            {
                Shuffle(clipList);
                which = skipIntro?1:0;
            }
            timeBetweenCounter = 0;
            waiting = true;
            //if (which == 1)
            //{
            //    this.transform.GetChild(0).gameObject.SetActive(false);
            //}
            audi.Play();
        }
        if (waiting)
        {
            if (lengthCounter < audioLength)
            {
                lengthCounter += Time.deltaTime;
                float vol = Mathf.Min(1, ((Mathf.Cos((lengthCounter / audioLength) * 6.28f) * -.5f) + .5f) * 120);
                //print();
                audi.volume = vol;
            }
            else
            {
                waiting = false;
                lengthCounter = 0;
            }
        }
    }

  

    public void Shuffle(List<AudioClip> list)
    {
        int n = list.Count;
        while (n > 2)
        {
            n--;
            int k = Random.Range(1, list.Count);
            AudioClip value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}


}