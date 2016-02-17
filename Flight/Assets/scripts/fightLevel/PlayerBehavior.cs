using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	public GameObject body;
	public float maxSpeed;
	public float currentSpeed;
	public float rotationSpeed;
	public Rigidbody rb;
	public bool isAccelerated = false;
	public bool isBreaking = false;
	public float horizontalRotation = 0f;
	public float verticalRotation = 0f;

	protected Vector3 eulerAngleVelocity;
	protected bool HdirectionChanged = false;
	protected bool VdirectionChanged = false;
	protected Vector3 avoidDirection;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		avoidDirection = transform.forward;

	}
	
	// Update is called once per frame
	void FixedUpdate() {
		//float horizontalRotation = 0f;
		//float verticalRotation = 0f;
		HdirectionChanged = false;
		VdirectionChanged = false;

		if(Input.mousePosition.x>Screen.width/2+Screen.height/8)
		{
			HdirectionChanged = true;
			horizontalRotation = ((Input.mousePosition.x-Screen.width/2)/(Screen.width/2))*rotationSpeed;
		} else if (Input.mousePosition.x<Screen.width/2-Screen.height/8)
		{
			HdirectionChanged = true;
			horizontalRotation = -((Screen.width/2 - Input.mousePosition.x)/(Screen.width/2))*rotationSpeed;
		}

		if(Input.mousePosition.y>Screen.height/2+Screen.height/8)
		{
			VdirectionChanged = true;
			verticalRotation = -((Input.mousePosition.y-(Screen.height/2))/(Screen.height/2))*rotationSpeed;
		} else if (Input.mousePosition.y<Screen.height/2-Screen.height/8)
		{
			VdirectionChanged = true;
			verticalRotation = ((Screen.height/2 - Input.mousePosition.y)/(Screen.height/2))*rotationSpeed;
		}
		float h = horizontalRotation * Time.deltaTime;
		float v = verticalRotation * Time.deltaTime;
		if (Input.GetMouseButton(0))
		{
			Turbo ();
			//Breaking ();
			//isAccelerated = false;
		} else if(currentSpeed<maxSpeed)
		{
			currentSpeed = Mathf.Lerp(0f,maxSpeed,1);
			//currentSpeed+=0.5f;
			isAccelerated = true;
			isBreaking = false;
		} else if(currentSpeed>maxSpeed)
		{
			Breaking ();
		} else 
		{
			isAccelerated = false;
			isBreaking = false;
		}
		

		if(HdirectionChanged)
		{
			rb.AddTorque(transform.up * h, ForceMode.Force);
		} else 
		{
			//Vector3 VelH = new Vector3(0,rb.angularVelocity.y,0);
			//Debug.Log(VelH);
			//rb.AddTorque(VelH * (-.05f*rotationSpeed), ForceMode.Force);

			rb.AddTorque(rb.angularVelocity * (-.05f*rotationSpeed), ForceMode.Force);
		}

		if(VdirectionChanged)
		{
			rb.AddTorque(transform.right * v, ForceMode.Force);
		} else 
		{
			//Vector3 VelV = new Vector3(rb.angularVelocity.x,0,0);
			//Debug.Log(VelV);
			//rb.AddTorque(VelV * (-.05f*rotationSpeed), ForceMode.Force);
			rb.AddTorque(rb.angularVelocity * (-.05f*rotationSpeed), ForceMode.Force);
		}

		//Vector3 VelF = new Vector3(0,0,rb.angularVelocity.z);


		if(!HdirectionChanged && !VdirectionChanged)
		{
			if(rb.transform.eulerAngles.z>5 && rb.transform.eulerAngles.z<175)
			{
				rb.AddTorque(-transform.forward * rotationSpeed*Time.deltaTime, ForceMode.Force);
			} else if (rb.transform.eulerAngles.z<355 && rb.transform.eulerAngles.z>185)
			{
				rb.AddTorque(transform.forward * rotationSpeed*Time.deltaTime, ForceMode.Force);
			}
		}
		//rb.transform.eulerAngles.z = Mathf.Lerp(rb.transform.eulerAngles.z,0,1);
		//if(rb.transform.eulerAngles.z
		//rb.AddTorque(VelF * (-.05f*rotationSpeed), ForceMode.Force);

		rb.AddForce(transform.forward * currentSpeed,ForceMode.Force);

		Debug.Log(currentSpeed + " , " + isAccelerated + " , " + isBreaking);
	}

	void Breaking()
	{
		if(currentSpeed>5)
		{
			//currentSpeed = Mathf.Lerp(maxSpeed,5f,1);
			currentSpeed-=0.2f;
			isBreaking = true;
			isAccelerated = false;
		} else 
		{
			isBreaking = false;
		}
	}

	void Turbo()
	{
		if(currentSpeed<maxSpeed*1.25f)
		{
			currentSpeed+=0.2f;
			isBreaking = false;
			isAccelerated = true;
		}
	}
}
