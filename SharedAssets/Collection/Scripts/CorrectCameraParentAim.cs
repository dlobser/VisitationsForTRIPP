using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace ON{
public class CorrectCameraParentAim : MonoBehaviour {
    
    public float speed;
    public bool fixPosition;
    public float positionSpeed;

    bool present;
    bool prevPresent;

    public float minAngle = 45;

    Quaternion prevRotation;
    bool shouldBeMoving = false;

    public float moveTimer = 3;
    float timer;

    float rotateSpeed = 0;

    public Vector3 resetAxes;

    public float fadeSpeed = .2f;
    public GameObject fader;

    public Vector3 initPos;

	void Awake () {
        Init();
        
	}

	private void Init()
	{
        this.transform.localRotation = Quaternion.Inverse(Camera.main.transform.localRotation);
        if (fixPosition)
            this.transform.GetChild(0).localPosition = Camera.main.transform.localPosition * -1;
        initPos = Camera.main.transform.position;
	}

	private void OnEnable()
	{
        Init();
	}

    void FixAxes()
    {
        this.transform.localEulerAngles = new Vector3(
            resetAxes.x > 0 ? 0 : this.transform.localEulerAngles.x,
            resetAxes.y > 0 ? 0 : this.transform.localEulerAngles.y,
            resetAxes.z > 0 ? 0 : this.transform.localEulerAngles.z);
    }

    public void Fade(float a)
    {
        if (a >= 0)
        {
            fader.GetComponent<MeshRenderer>().enabled = true;
        }
        a = Mathf.Clamp(a, 0, 1);
        fader.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, a);
        if (a <= 0)
        {
            fader.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    IEnumerator F(bool down)
    {
        float counter = 0;
        while (counter < 1)
        {
            counter += Time.deltaTime / fadeSpeed;
            Fade(down?counter:1-counter);

            yield return null;
        }
        if (down)
        {
            shouldBeMoving = true;
            StartCoroutine(F(false));
        }
    }

    void Update () {

        FixAxes();

        present = XRDevice.isPresent;

        if(present!=prevPresent){
            Init();
        }

        if (Input.anyKeyDown)
            Init();

        if (Quaternion.Angle(Camera.main.transform.localRotation, prevRotation) > 20)
            Init();

        float angle = Quaternion.Angle(Quaternion.Inverse(this.transform.localRotation), Camera.main.transform.localRotation);
        float dist = Vector3.Distance(Camera.main.transform.position,initPos);
        if (!shouldBeMoving)
        {
            if (angle > minAngle || dist>.2f)
            {
                timer += Time.deltaTime;
                if (timer > moveTimer)
                {
                    StartCoroutine(F(true));
                    timer = 0;
                }

            }
            else
            {
                timer = 0;
            }
        }
        //if (shouldBeMoving)
        //{
        //    if (rotateSpeed < 1)
        //    {
        //        rotateSpeed += Time.deltaTime;
        //    }
        //    if (angle < 1)
        //    {
        //        rotateSpeed = 0;
        //        shouldBeMoving = false;
        //    }
        //}
        if (shouldBeMoving)
            this.transform.localRotation = Quaternion.Lerp(
                this.transform.localRotation, Quaternion.Inverse(Camera.main.transform.localRotation), 1); //rotateSpeed*(Time.deltaTime*speed*200f)/Mathf.Pow(angle,.5f));
        if (shouldBeMoving && fixPosition)
            this.transform.GetChild(0).localPosition = Vector3.Lerp(this.transform.GetChild(0).localPosition, Camera.main.transform.localPosition * -1, 1);// positionSpeed);
        prevPresent = present;
        prevRotation = Camera.main.transform.localRotation;
        shouldBeMoving = false;
    }
}


}