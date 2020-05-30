using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class RiverMaker : MonoBehaviour
{
    public GameObject section;
    public int sectionAmount;
    public float randomRotation;
    public Transform riverParent;
    public GameObject follower;
    List<GameObject> sections;
    float counter = 0;
    public float speed;
    int which ;
    public GameObject sparkle;
    River.Section sec;
    River.Section nextSec;
         
    void Start()
    {
        sections = new List<GameObject>();
        GameObject a = Instantiate(section);
        a.GetComponent<River.Section>().geo.GetComponent<PatchSurface>().Make();
        a.GetComponent<River.Section>().geo.transform.parent = riverParent;
        sections.Add(a);
        sec = sections[which].GetComponent<River.Section>();
        for (int i = 1; i < sectionAmount; i++)
        {
            //GameObject b = Instantiate(section);

            //b.GetComponent<River.Section>().root.transform.position = a.GetComponent<River.Section>().end.transform.position;
            //b.GetComponent<River.Section>().root.transform.rotation = a.GetComponent<River.Section>().end.transform.rotation;
            //float rand = Random.Range(-randomRotation, randomRotation);
            //b.GetComponent<River.Section>().middle1.transform.localEulerAngles = new Vector3(0, rand, 0);
            //b.GetComponent<River.Section>().middle2.transform.localEulerAngles = new Vector3(0, rand, 0);
            //b.GetComponent<River.Section>().geo.transform.parent = riverParent;
            //sections.Add(b);
            //a = b;
            a = AddSection(a);
        }
        nextSec = sections[2].GetComponent<River.Section>();

    }

    GameObject AddSection(GameObject a){
        GameObject b = Instantiate(section);

        b.GetComponent<River.Section>().root.transform.position = a.GetComponent<River.Section>().end.transform.position;
        b.GetComponent<River.Section>().root.transform.rotation = a.GetComponent<River.Section>().end.transform.rotation;
        float rand = Random.Range(-randomRotation, randomRotation);
        b.GetComponent<River.Section>().middle1.transform.localEulerAngles = new Vector3(0, rand, 0);
        b.GetComponent<River.Section>().middle2.transform.localEulerAngles = new Vector3(0, rand, 0);
        b.GetComponent<River.Section>().geo.transform.position = a.GetComponent<River.Section>().end.transform.position;
        b.GetComponent<River.Section>().geo.transform.rotation = a.GetComponent<River.Section>().end.transform.rotation;

        b.GetComponent<River.Section>().geo.GetComponent<PatchSurface>().Make();

        b.GetComponent<River.Section>().geo.transform.position += riverParent.transform.position;
        b.GetComponent<River.Section>().geo.transform.parent = riverParent;
        sections.Add(b);
        return b;
        //a = b;
    }

    void Update()
    {
        Transform[] r = new Transform[] { sec.root.transform, sec.middle1.transform, sec.end.transform };
        Transform[] rn = new Transform[] { nextSec.root.transform, nextSec.middle1.transform, nextSec.end.transform };

        follower.transform.position = cSpline(r , counter);
        Vector3 look = cSpline(rn, counter);
        follower.transform.LookAt(look);
        if(sparkle!=null){
            sparkle.transform.localPosition = look;
        }
        counter += Time.deltaTime * speed;
        if(counter>1){
            counter = 0;
            which++;
            AddSection(sections[sections.Count - 1].gameObject);
            sec = sections[which].GetComponent<River.Section>();
            StartCoroutine(ScaleUp(sections[sections.Count - 1].gameObject.GetComponent<River.Section>().geo.gameObject,true));
            nextSec = sections[which+2].GetComponent<River.Section>();

            if (which > 2)
            {
                StartCoroutine(ScaleUp(sections[which-1].gameObject.GetComponent<River.Section>().geo.gameObject, false));

                Destroy(sections[which - 2].gameObject);
                Destroy(riverParent.transform.GetChild(1).gameObject);
            }
        }
        riverParent.transform.position = follower.transform.position *-1;
        Camera.main.transform.parent.parent.parent.rotation = 
           Quaternion.Lerp(Camera.main.transform.parent.parent.parent.rotation,follower.transform.rotation,Time.deltaTime);
        //riverParent.transform.parent.transform.localEulerAngles = follower.transform.localEulerAngles *- 1;

    }

    IEnumerator ScaleUp(GameObject g, bool up){

        float count = 0;
        while(count<1){
            count += Time.deltaTime * speed;
            float s = Mathf.Pow(count, 2);
            s = up ? s : 1 - s;
            //g.transform.localScale = new Vector3(s, s, s);
            g.GetComponent<SetChildSpriteTransparency>().SetTransparency(s);
            yield return null;
        }

    }

    Vector3 cSpline(Transform[] a, float t)
    {
        Vector3 A = Vector3.Lerp(a[0].position, a[1].position, t);
        Vector3 B = Vector3.Lerp(a[1].position, a[2].position, t);
        //Vector3 C = Vector3.Lerp(a[2].position, a[3].position, t);
        Vector3 AA = Vector3.Lerp(A, B, t);
        //Vector3 BB = Vector3.Lerp(B, C, t);
        return AA;//Vector3.Lerp(AA, BB, t);
    }
    Vector3 cSplineL(Transform[] a, float t)
    {
        Vector3 A = Vector3.Lerp(a[0].localPosition, a[1].localPosition, t);
        Vector3 B = Vector3.Lerp(a[1].localPosition, a[2].localPosition, t);
        //Vector3 C = Vector3.Lerp(a[2].position, a[3].position, t);
        Vector3 AA = Vector3.Lerp(A, B, t);
        //Vector3 BB = Vector3.Lerp(B, C, t);
        return AA;//Vector3.Lerp(AA, BB, t);
    }
}


}