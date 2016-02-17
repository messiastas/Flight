using UnityEngine;
using System.Collections;

public class Goat : MonoBehaviour {
	protected Vector2 targetPoint;
	// Use this for initialization
	void Awake () {
		//targetPoint = new Vector2(Random.Range(1,100),Random.Range(1,100));

	}
	
	// Update is called once per frame
	void Update () {
		//if(Vector2.Distance(targetPoint,new Vector2(transform.position.x,transform.position.z))>0.25f)
		//{
		//	transform.LookAt(new Vector3(targetPoint.x,transform.position.y,targetPoint.y));
			//transform.position+= transform.forward*Time.deltaTime*1f;
			//transform.position = new Vector3(transform.position.x,Terrain.activeTerrain.SampleHeight(transform.position),transform.position.z);
		//}
	}
}
