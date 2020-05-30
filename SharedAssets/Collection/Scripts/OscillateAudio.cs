using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class OscillateAudio : MonoBehaviour
{

    AudioSource audio;

    public float speed;
    Vector4 counter;

    public Vector3 pitchOscillate1;
    public Vector3 pitchOscillate2;
    public Vector3 volumeOscillate1;
    public Vector3 volumeOscillate2;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        counter.Set(counter.x + Time.deltaTime * pitchOscillate1.z,
                    counter.y + Time.deltaTime * pitchOscillate2.z,
                    counter.z + Time.deltaTime * volumeOscillate1.z,
                    counter.w + Time.deltaTime * volumeOscillate2.z);
        float p1 = map(Mathf.Sin(counter.x), -1, 1, pitchOscillate1.x, pitchOscillate1.y);
        float p2 = map(Mathf.Sin(counter.y), -1, 1, pitchOscillate2.x, pitchOscillate2.y);
        float v1 = map(Mathf.Sin(counter.z), -1, 1, volumeOscillate1.x, volumeOscillate1.y);
        float v2 = map(Mathf.Sin(counter.w), -1, 1, volumeOscillate2.x, volumeOscillate2.y);

        audio.pitch = p1 + p2;
        audio.volume = v1 + v2;


    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}


}