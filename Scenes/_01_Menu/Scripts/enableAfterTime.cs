using UnityEngine;
using System.Collections;

namespace ON{

public class enableAfterTime : MonoBehaviour {

	public float time;
	public GameObject sphere;
	float counter = 0;
    float init;
    public float speed = 1;

    private void Start()
    {
        init = sphere.transform.localScale.x;

    }
    void OnEnable () {
        StartCoroutine(timeUp());
    }
	
	
    IEnumerator timeUp(){
        counter = 0;
        while (counter < time) {
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
            counter += Time.deltaTime/speed;
            float s = Mathf.SmoothStep(0,1,counter) * init;
            sphere.transform.localScale = new Vector3(s, s, s);
            yield return null;
        }
    }
}


}