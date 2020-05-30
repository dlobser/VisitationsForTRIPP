using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ON{

public class AutoLoadNextScene : MonoBehaviour
{
    public int whichScene = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SceneManager.LoadScene(whichScene);
    }
}


}