using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchSurface : MonoBehaviour {

 
    public Transform[] LineA;
    public Transform[] LineB;
    public bool liveUpdate = false;
    public int uDivs, vDivs;

    public GameObject tree;
    public int treeAmount;

    List<Transform> trees;

	void Start () {

    }

    public void Make(){
        if(trees==null)
            trees = new List<Transform>();
        GameObject g = new GameObject();
        g.AddComponent<MeshFilter>();
        g.AddComponent<MeshRenderer>();
        g.GetComponent<MeshRenderer>().sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial; 
        g.GetComponent<MeshFilter>().mesh = Grid.Generate(uDivs, vDivs, pSurface);
        g.transform.parent = this.transform;
        for (int i = 0; i < treeAmount; i++)
        {
            GameObject t = Instantiate(tree);

            float s = Random.Range(.8f, 1.8f);
            Vector3 line_a = cSpline(LineA, (float)i / (float)treeAmount);
            Vector3 line_b = cSpline(LineB, (float)i / (float)treeAmount);

            Vector3 a = (line_a - line_b)+line_a;
            Vector3 b = (line_b - line_a)+line_b;

            t.transform.localPosition = Vector3.Lerp(line_a, a, Random.value * .25f);// cSpline(LineA, (float)i / (float)treeAmount) + new Vector3(Random.Range(0, 0), 0, 0);
            t.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-5, 5));
            t.transform.localScale = new Vector3(s, s, s);
            t.transform.parent = this.transform;
            trees.Add(t.transform.GetChild(0));

            t = Instantiate(tree);
            t.transform.localScale = new Vector3(s, s, s);
            t.transform.localPosition = Vector3.Lerp(line_b, b, Random.value*.25f);//cSpline(LineB, (float)i / (float)treeAmount) - new Vector3(Random.Range(0, 0), 0, 0);
            t.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-5, 5));
            t.transform.parent = this.transform;
            trees.Add(t.transform.GetChild(0));

        }
    }


    void Update () {
        if (liveUpdate)
        {
            Destroy(GetComponent<MeshFilter>().mesh);
            GetComponent<MeshFilter>().mesh = Grid.Generate(uDivs, vDivs, pSurface);
        }
        for (int i = 0; i < trees.Count; i++)
        {
            trees[i].LookAt(Camera.main.transform.position);
            trees[i].localEulerAngles = Vector3.Scale(trees[i].localEulerAngles, Vector3.up);
        }
       

	}

    Vector3 cSpline(Transform[] a, float t){
        Vector3 A = Vector3.Lerp(a[0].position, a[1].position, t);
        Vector3 B = Vector3.Lerp(a[1].position, a[2].position, t);
        //Vector3 C = Vector3.Lerp(a[2].position, a[3].position, t);
        Vector3 AA = Vector3.Lerp(A, B, t);
        //Vector3 BB = Vector3.Lerp(B, C, t);
        return AA;//Vector3.Lerp(AA, BB, t);
    }

    Vector3 pSurface(float u, float v){
        return Vector3.Lerp(
            cSpline(LineA, u),
            cSpline(LineB, u),
            v);
    }

    Vector3 BezierSurface(float u, float v){
        return Vector3.Lerp(
        CatmullRomSpline.GetSplinePos(LineA, u),
        CatmullRomSpline.GetSplinePos(LineB, u),
            v);
    }
}
