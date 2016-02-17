using UnityEngine;
using System.Collections;

public class SupplyBox : MonoBehaviour {
	public float speed = 0.2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y>0.1f)
		{
			transform.position-= transform.up*Time.deltaTime*speed*2f;
			transform.Rotate(0.0f,8f*Time.deltaTime,0.0f );
		} else 
		{
			GameObject.Destroy(gameObject);
		}
	}
}
