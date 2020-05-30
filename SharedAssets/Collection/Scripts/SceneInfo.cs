using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInfo : MonoBehaviour
{
    public float time;
    public List<bool> scenes;
    public List<bool> initScenes;
    public List<string> sceneNames;
    public float counter;
    public int whichScene;
    int maxScenes;
    public bool useVO;

    void Start()
    {

        if (scenes.Count < 1)
        {
            scenes = new List<bool>();
            for (int i = 0; i < 10; i++)
            {
                scenes.Add(false);
            }
        }
        else
        {
            initScenes = new List<bool>();
            for (int i = 0; i < 10; i++)
            {
                initScenes.Add(scenes[i]);
            }
        }
        maxScenes = SceneManager.sceneCountInBuildSettings - 1;
        maxScenes = maxScenes > GetSceneCount() ? GetSceneCount() : maxScenes;
    }

    public int GetScene(int which)
    {
        int c = 0;
        int w = 0;
        for (int i = 0; i < scenes.Count; i++)
        {
            if (scenes[i])
            {
                if(c==which)
                    w = i;
                c++;
            }
        }
        return w;
    }

    public int GetNextScene()
    {
        //print(whichScene + " , " + maxScenes);
        //if (whichScene + 1 > GetSceneCount())
        //{
        //    whichScene = 1;
        //}
        //else
        //{
            //bool lastOne = false; ;
        for (int i = whichScene + 1; i < scenes.Count; i++)
        {
            
            if (i == scenes.Count - 1)
            {
                whichScene = 1;
            }
            else if (scenes[i])
            {
                whichScene = i;
                break;
            }
        }
        //}
        return whichScene;
    }

    public int GetSceneCount()
    {
        int o = 0;
        for (int i = 0 ; i < scenes.Count; i++)
        {
            if (scenes[i])
            {
                o++;
            }
        }
        return o;
    }

    //public bool SceneExists(string s)
    //{
    //    bool o = false;
    //    for (int i = 0; i < scenes.Count; i++)
    //    {
    //        if (scenes[i])
    //        {
    //            o++;
    //        }
    //    }
    //    return o;
    //}

    void Update()
    {
        
    }
}
