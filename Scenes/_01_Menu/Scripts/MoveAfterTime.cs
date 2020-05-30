using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAfterTime : MonoBehaviour
{
    public float time;
    public GameObject sphere;
    float counter = 0;
    Vector3 init;
    public float speed = 1;

    public Vector3 moveTo;

    private void Start()
    {
        init = sphere.transform.localPosition;

    }
    void OnEnable()
    {
        StartCoroutine(timeUp());
    }


    IEnumerator timeUp()
    {
        counter = 0;
        while (counter < time)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        sphere.SetActive(true);
        StartCoroutine(scaleUp());
    }

    IEnumerator scaleUp()
    {
        counter = 0;
        while (counter < 1)
        {
            counter += Time.deltaTime / speed;

            sphere.transform.localPosition = Vector3.Lerp(init, moveTo,Mathf.SmoothStep(0,1, counter));
            yield return null;
        }
    }
}
