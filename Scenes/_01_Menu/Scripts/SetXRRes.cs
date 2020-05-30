using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SetXRRes : MonoBehaviour {
    public float res;

	void Start () {
        XRSettings.eyeTextureResolutionScale = res;

	}
	
}
