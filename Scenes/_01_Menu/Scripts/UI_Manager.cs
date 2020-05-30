using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : Interactable
{
    public Renderer ren;
    public float fadeSpeed;
    public SceneInfo sceneInfo;
    int sceneCount;

    public float counter;
    float alive = 0;
    bool counting = false;

    public PlayRandomAudio VOAudio;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Fade(1);
        StartCoroutine(Loaded());
        sceneCount = sceneInfo.GetSceneCount();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        alive += Time.deltaTime;
        if (counting)
        {
            counter += Time.deltaTime;
            //print(sceneCount);
            sceneCount = sceneInfo.GetSceneCount();
            if (counter > sceneInfo.time / (float)(sceneCount) || Input.GetButtonDown("Fire1"))
            {
                int s = sceneInfo.GetNextScene();
                //VOAudio.Skip();
                counter = 0;
                if (s == 1)
                {
                    counting = false;
                    sceneInfo.scenes = sceneInfo.initScenes;
                    sceneInfo.useVO = false;
                    VOAudio.which = 0;
                    VOAudio.skipIntro = false;
                    VOAudio.gameObject.SetActive(false);


                }
                StartCoroutine(Load(s));

            }
        }
    }



    void Awake()
    {
        AudioListener.volume = 0;
        ren.sharedMaterial.color = new Color(0, 0, 0, 1);

    }

    public void Fade(float a)
    {
        if (ren == null)
            ren = GetComponent<Renderer>();
        print(ren);
        if (a >= 0)
        {
            ren.enabled = true;
        }
        a = Mathf.Clamp(a, 0, 1);
        AudioListener.volume = (1 - a) * 1;
        ren.sharedMaterial.color = new Color(0, 0, 0, a);
        if (a <= 0)
        {
            ren.enabled = false;
        }
    }

    public override void HandleTrigger()
    {
        base.HandleTrigger();
        sceneInfo.GetNextScene();
        StartCoroutine(Load(sceneInfo.whichScene));
        counting = true;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade(1);
        StartCoroutine(Loaded());
        if (sceneInfo.useVO && sceneInfo.sceneNames[sceneInfo.whichScene]!="MenuWithUI")
        {
            VOAudio.gameObject.SetActive(true);
        }
        else
        {
            VOAudio.gameObject.SetActive(false);
        }

    }

    IEnumerator Load(int whichScene)
    {
        if (whichScene == 0)
        {
           
            counting = false;
            counter = 0;
        }
        float count = 0;
        while (count < fadeSpeed)
        {
            count += Time.deltaTime;
            Fade(count / fadeSpeed);
            yield return null;
        }

        SceneManager.LoadScene(sceneInfo.sceneNames[whichScene]);

    }

    IEnumerator Loaded()
    {
        float count = 0;
       
        Fade(1);
        count = 0;
        while (count < fadeSpeed)
        {
            count += Time.deltaTime;
            Fade(1 - (count / fadeSpeed));
            yield return null;
        }
    }
}

