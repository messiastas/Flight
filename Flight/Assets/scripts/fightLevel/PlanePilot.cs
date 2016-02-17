using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlanePilot : MonoBehaviour {
	public GameObject body;
	public float maxSpeed;
	public float currentSpeed;
	public float rotationSpeed;
	public Rigidbody rb;
	public bool isAccelerated = false;
	public bool isBreaking = false;
	public float horizontalRotation = 0f;
	public float verticalRotation = 0f;
	public float healths = 150f;
	public int goatsLeft;

	public Text targetsLeft;
	
	protected Vector3 eulerAngleVelocity;
	protected bool HdirectionChanged = false;
	protected bool VdirectionChanged = false;
	protected Vector3 avoidDirection;
	protected bool isPlay = false;
	protected float lastTime = 0;
	protected bool isPaused = false;
	protected GameObject lastGoat = null;

	protected GUISystem gui;

	public delegate void StartMovingAction();
	public static event StartMovingAction OnMoved;
	public delegate void GetTargetAction();
	public static event GetTargetAction OnGetTarget;
	public delegate void GetDamageAction();
	public static event GetDamageAction OnGetDamage;

	// Use this for initialization
	public void StartLevel () {
		Debug.Log("PlanePilot started with "+ this.gameObject.name);
		Application.targetFrameRate = 60;
		gui = GameObject.FindGameObjectWithTag("GUISystem").GetComponent<GUISystem>();
		gui.setMaxHealths(healths);
		if(SharedVars.Inst.lastPlayedLevel == "firstLevel")
		{
			targetsLeft.text = "Goats left: "+ goatsLeft;
		} else if(SharedVars.Inst.lastPlayedLevel == "secondLevel")
		{
			targetsLeft.text = "Supplies picked: "+ goatsLeft;
		}

		//Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("pause"))
		{
			PauseSwitch();
		} else if (Input.GetButtonDown("restart"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;
		if(isPlay)
		{
			Vector3 MoveCamTo = transform.position-transform.forward*2f+Vector3.up*1.5f;
			float bias = 0.96f;
			Camera.main.transform.position =Camera.main.transform.position*bias + MoveCamTo*(1.0f-bias);
			Camera.main.transform.LookAt(transform.position+transform.forward*10f);

			HdirectionChanged = false;
			VdirectionChanged = false;
			horizontalRotation = 0f;
			verticalRotation = 0f;

			gui.VisiblePanel("",false);

			if(mouseX>Screen.width/2+Screen.height/8)
			{
				HdirectionChanged = true;
				horizontalRotation = -((mouseX-(Screen.width/2+Screen.height/8))/(Screen.width/2-Screen.height/8))*rotationSpeed;
				gui.VisiblePanel("right",true);
			} else if (mouseX<Screen.width/2-Screen.height/8)
			{
				HdirectionChanged = true;
				horizontalRotation = ((Screen.width/2-Screen.height/8 - mouseX)/(Screen.width/2-Screen.height/8))*rotationSpeed;
				gui.VisiblePanel("left",true);
			}
			
			if(mouseY>Screen.height/2+Screen.height/8)
			{
				VdirectionChanged = true;
				verticalRotation = -((mouseY-(Screen.height/2))/(Screen.height/2))*rotationSpeed*0.5f;
				gui.VisiblePanel("up",true);
			} else if (mouseY<Screen.height/2-Screen.height/8)
			{
				VdirectionChanged = true;
				verticalRotation = ((Screen.height/2 - mouseY)/(Screen.height/2))*rotationSpeed*0.5f;
				gui.VisiblePanel("down",true);
			}

			//ROTATE all plane
			transform.Rotate(verticalRotation*Time.deltaTime,-horizontalRotation*Time.deltaTime,0.0f ,Space.Self);
			transform.rotation = Quaternion.Euler(CalcEulerSafe(transform.rotation.eulerAngles.x), CalcEulerSafe(transform.rotation.eulerAngles.y), CalcEulerSafe(transform.rotation.eulerAngles.z));
			//Rotate body (no effect on movement direction)
			//body.transform.rotation = Quaternion.Euler(CalcEulerSafe(body.transform.rotation.eulerAngles.x), CalcEulerSafe(body.transform.rotation.eulerAngles.y), CalcEulerSafe(body.transform.rotation.eulerAngles.z));
			//Debug.Log(body.transform.rotation.eulerAngles.z + "   " + transform.rotation.eulerAngles.z);
			if(horizontalRotation!=0 && (Mathf.Abs(body.transform.rotation.eulerAngles.z)<45f || Mathf.Abs(body.transform.rotation.eulerAngles.z)>315f))//if((body.transform.rotation.eulerAngles.z<45f && horizontalRotation>0) || (body.transform.rotation.eulerAngles.z>315f && horizontalRotation<0))
			{
				body.transform.Rotate(0.0f,0.0f, horizontalRotation*rotationSpeed*0.025f*Time.deltaTime);
			}
			//Debug.Log(horizontalRotation);

			//Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x,Camera.main.transform.rotation.eulerAngles.y,horizontalRotation*50f), Time.deltaTime*2f);
			//transform.Rotate(verticalRotation,0.0f, horizontalRotation);

			//SET ROTATION IN SAFE AMOUNT WITH ORIENTATION
			transform.rotation = Quaternion.Euler(CalcEulerSafe(transform.rotation.eulerAngles.x), CalcEulerSafe(transform.rotation.eulerAngles.y), CalcEulerSafe(0f));
			body.transform.rotation = Quaternion.Euler(CalcEulerSafe(body.transform.rotation.eulerAngles.x), CalcEulerSafe(body.transform.rotation.eulerAngles.y), CalcEulerSafe(body.transform.rotation.eulerAngles.z));

			//NORMALIZE HORIZONTAL ROTATION 
			if(!HdirectionChanged)
			{
				//if(transform.rotation.z>0.02f && transform.rotation.z<0.5f)
				//{
				//	transform.Rotate(0.0f,0.0f,rotationSpeed*Time.deltaTime);
				//} else if(transform.rotation.z<-0.02f && transform.rotation.z>-0.5f)
				//{
				//	transform.Rotate(0.0f,0.0f,rotationSpeed*Time.deltaTime);
				//}
				//Rotate body (no effect on movement direction)
				if(body.transform.rotation.eulerAngles.z>1f && body.transform.rotation.eulerAngles.z<60f)
				{
					body.transform.Rotate(0.0f,0.0f,-rotationSpeed*0.5f*Time.deltaTime);
				} else if(body.transform.rotation.eulerAngles.z<360f && body.transform.rotation.eulerAngles.z>200f)
				{
					body.transform.Rotate(0.0f,0.0f,rotationSpeed*0.5f*Time.deltaTime);
				}
			}
			//body.transform.rotation = Quaternion.Euler(CalcEulerSafe(body.transform.rotation.eulerAngles.x), CalcEulerSafe(body.transform.rotation.eulerAngles.y), CalcEulerSafe(body.transform.rotation.eulerAngles.z));
			//Debug.Log(body.transform.rotation.z);

			//GOING UP IF LEFT/RIGHT ROTATION IS HIGH ENOUGH
			//if(Mathf.Abs(transform.rotation.z)>0.4f)//0.25f
			//{
			//	transform.Rotate(Mathf.Lerp(0,-Mathf.Abs(transform.rotation.z),1),0.0f, 0.0f);
			//}

			//CHECK ACCELERATION AND BREAKING
			if (Input.GetMouseButton(0))
			{
				//Turbo ();
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

			//GRAVITY
			currentSpeed-=transform.forward.y *Time.deltaTime*maxSpeed;

			//MIN. SPEED
			if(currentSpeed<maxSpeed/10f) currentSpeed = maxSpeed/10f;

			//MOVEMENT
			transform.position+= transform.forward*Time.deltaTime*currentSpeed;

			//CHECK TERRAIN COLLISION
			//float currentTerrainHeight = 0f;//Terrain.activeTerrain.SampleHeight(transform.position);
			//if(currentTerrainHeight>transform.position.y)
			//{
			//	transform.position = new Vector3(transform.position.x,currentTerrainHeight+0.1f,transform.position.z);
			//	GetHit();
			//}


		} else if(!isPaused)
		{
			float currentTime = Time.realtimeSinceStartup;
			if(currentTime-lastTime>0.2f)
			{
				lastTime = currentTime;
				gui.ChangeVisiblePanel("");
			}
			if(mouseX<Screen.width/2+Screen.height/8 && mouseX>Screen.width/2-Screen.height/8 && mouseY<Screen.height/2+Screen.height/8 && mouseY>Screen.height/2-Screen.height/8)
			{
				isPlay = true;
				if(OnMoved != null)
					OnMoved();
			}
			
		}
	}

	public void PauseSwitch()
	{
		isPlay = false;
		isPaused = !isPaused;
		gui.VisiblePanel("", false);
		if(isPaused)
		{
			Time.timeScale = 0.0f;

		} else
		{
			Time.timeScale = 1.0f;
		}
	}
	
	void Breaking()
	{
		if(currentSpeed>maxSpeed/10f)
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

	public float CalcEulerSafe(float num)
	{
		if (num >= -180 && num <= 180)
			return num;
		num = num % 360;
		if (num > 0)
			num -= 360;
		else
			num += 360;
		return num;
	}

	public void OnTriggerEnter(Collider other)
	{

		//Debug.Log("detect Some" + " " + other.gameObject.tag);
		if(other.gameObject.tag == "goat" && lastGoat!=other.gameObject)
		{
			//Debug.Log("detect player");
			Destroy(other.gameObject);
			goatsLeft--;
			targetsLeft.text = "Goats left: "+ goatsLeft;
			lastGoat=other.gameObject;
			if(OnGetTarget != null)
				OnGetTarget();

		} else if(other.gameObject.tag == "cube")
		{
			GetHit(1f);
			if(OnGetDamage != null)
				OnGetDamage();
			//Application.LoadLevel(Application.loadedLevel);
			
		} else if(other.gameObject.tag == "supply" && lastGoat!=other.gameObject)
		{
			//Debug.Log("detect player");
			Destroy(other.gameObject);
			goatsLeft++;
			targetsLeft.text = "Supplyes picked: "+ goatsLeft;
			lastGoat=other.gameObject;
			//if(OnGetTarget != null)
			//	OnGetTarget();
			
		}
	}

	protected void GetHit(float damage = 1f)
	{
		healths-=damage;
		gui.MoveHealth(damage);
		if(healths<=0f)
		{
			PauseSwitch();
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
