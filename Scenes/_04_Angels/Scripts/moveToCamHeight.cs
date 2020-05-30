using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class moveToCamHeight : MonoBehaviour {
    public float speed;
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(
            Mathf.Lerp(this.transform.position.x, Camera.main.transform.position.x,speed),
            Mathf.Lerp(this.transform.position.y, Camera.main.transform.position.y,speed),
            this.transform.position.z);

    }
}


}