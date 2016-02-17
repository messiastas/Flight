using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public GameObject player;

	protected Vector3 defaultPosition;
	protected Quaternion defaultRotation;
	protected bool isAcceleration = false;
	// Use this for initialization
	void Start () {
		defaultPosition = this.transform.localPosition;
		defaultRotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(player.GetComponent<PlayerBehavior>().isBreaking)
		{
			this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,this.transform.localPosition.z+0.01f);
		} else if(player.GetComponent<PlayerBehavior>().isAccelerated)
		{
			this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,this.transform.localPosition.z-0.02f);
		} else 
		{
			if(defaultPosition.z-this.transform.localPosition.z>0.02f)
			{
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,this.transform.localPosition.z+0.02f);
			} else if(this.transform.localPosition.z-defaultPosition.z>0.02f)
			{
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,this.transform.localPosition.z-0.02f);
			}
		}
		//float hRot = player.GetComponent<PlayerBehavior>().horizontalRotation/player.GetComponent<PlayerBehavior>().rotationSpeed;
		//if(hRot!=0f)
		//{
		//	this.transform.RotateAround(player.transform.localPosition,Vector3.up,hRot*0.25f);
		//} else 
		//{
			//this.transform.rotation.
		//}
	}

}
