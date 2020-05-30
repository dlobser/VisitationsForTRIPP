using UnityEngine;
using System.Collections;

public class tubeTunnel : MonoBehaviour {

	public GameObject tube;
	public GameObject tubeParent;
	public int amount = 100;
	public float depth = 4;

	private GameObject[] tubes;

	// Use this for initialization
	void Start () {
		tubes = new GameObject[amount];

		for (int i = 0; i < amount; i++) {
			tubes[i] = Instantiate(tube,new Vector3(Mathf.Sin(i*.33f)*3,Mathf.Cos (i*.54f)*3,i*depth),Quaternion.identity) as GameObject;
			tubes[i].transform.Rotate(0,0,i*90 + 45);

			for(int j = 0 ; j < 3 ; j++){
				tubes[i].transform.GetChild(0).transform.parent = transform;
			}
            Destroy(tubes[i]);
		}
		//combine ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void combine(){
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
		print (meshFilters.Length);
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		int i = 0;
		while (i < meshFilters.Length) {
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			meshFilters[i].gameObject.active = false;
			i++;
		}
		transform.GetComponent<MeshFilter>().mesh = new Mesh();
		transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
		transform.gameObject.active = true;
	}
}
