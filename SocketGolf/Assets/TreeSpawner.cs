using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour {
	public int treeCount = 0;
	public GameObject tree;
	public GameObject hole;
	public static float randomX;
	public static float randTree;
	// Use this for initialization

	IEnumerator Start () {
		List<float> randList = new List<float>{-90f, -84f, 20f, -43f, 47f, -50f, 0f, -10f, -28f, 19f, 40f, 62f, 54f, 81f, 6f, -15f, 41f, 25f, 60f, 94f, 20f, 57f, -31f, -40f, 29f, 40f, 85f, 73f, 2f, -99f};
		float[] randzs = new float[]{};
		float[] randxs = new float[]{};
		if (randTree == 0) {
			randzs = randList.ToArray ();
			randList.Sort ();
			randxs = randList.ToArray ();
		} else {
			randxs = randList.ToArray ();
			randList.Sort ();
			randzs = randList.ToArray ();
		}
		//yield return new WaitForSeconds(0.5f);
		yield return new WaitUntil (mapGenerated);
		for (int i = 0; i < treeCount; i++)
		{
			float randx = randxs[i];
			float randz = randzs [i];
			Vector3 findh = new Vector3 (randx, 0, randz);
			GameObject g = Instantiate (tree, findh, Quaternion.identity);
			RaycastHit h;
			float heightAboveGround = 0f;
			if (Physics.Raycast (g.transform.position, g.transform.TransformDirection (Vector3.down) ,out h, Mathf.Infinity))
			{
				heightAboveGround = h.distance;
				Debug.Log ("Tree height in the air " + heightAboveGround);
			}
			g.transform.position = new Vector3(g.transform.position.x, (g.transform.position.y - heightAboveGround), g.transform.position.z);

		}
		yield return new WaitForSeconds(0.5f);
		Vector3 fh = new Vector3 (randomX, 0, randomX-5);
		GameObject myhole = Instantiate (hole, fh, Quaternion.identity);
		myhole.transform.Rotate (new Vector3 (-90f, 0f, 0f));
		RaycastHit beam;
		float height = 0f;
		if (Physics.Raycast (myhole.transform.position, Vector3.down ,out beam, Mathf.Infinity))
		{
			height = beam.distance;
			Debug.Log ("Hole in the air " + height);
			//Debug.Log (Vector3.down.ToString());
		}
		myhole.transform.position = new Vector3(randomX, (myhole.transform.position.y - height), randomX-5);

	}
	bool mapGenerated()
	{
		return(MapGenerator.mapMade || SingleMapGenerator.mapMade);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
