using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUISystem : MonoBehaviour {
	public Image panelUp;
	public Image panelLeft;
	public Image panelDown;
	public Image panelRight;
	public float healths;
	public float maxHealths;
	public Text healthBar;
	public Text targetsLeft;
	// Use this for initialization
	void Awake () {
		VisiblePanel("",false);
	}

	public void setMaxHealths(float maxH)
	{
		healths = maxH;
		maxHealths = maxH;
		healthBar.text = "Healths: " + healths;
	}

	public void VisiblePanel(string direction = "", bool isVisible = true)
	{
		switch (direction)
		{
		case "":
			panelUp.enabled = isVisible;
			panelLeft.enabled = isVisible;
			panelDown.enabled = isVisible;
			panelRight.enabled = isVisible;
			break;
		case "up":
			panelUp.enabled = isVisible;
			break;
		case "down":
			panelDown.enabled = isVisible;
			break;
		case "left":
			panelLeft.enabled = isVisible;
			break;
		case "right":
			panelRight.enabled = isVisible;
			break;
		}
	}

	public void ChangeVisiblePanel(string direction = "")
	{
		switch (direction)
		{
		case "":
			panelUp.enabled = !panelUp.enabled;
			panelLeft.enabled = !panelLeft.enabled;
			panelDown.enabled = !panelDown.enabled;
			panelRight.enabled = !panelRight.enabled;
			break;
		case "up":
			panelUp.enabled = !panelUp.enabled;
			break;
		case "down":
			panelDown.enabled = !panelDown.enabled;
			break;
		case "left":
			panelLeft.enabled = !panelLeft.enabled;
			break;
		case "right":
			panelRight.enabled = !panelRight.enabled;
			break;
		}
	}
	// Update is called once per frame
	void Update () {

	}

	public void MoveHealth(float ddH=0)
	{
		healths -=ddH;
		healthBar.text = "Healths: " + healths;
		//mask.transform.localScale.Set(mask.transform.localScale.x-ddH/maxHealths,mask.transform.localScale.y,mask.transform.localScale.z);
		//masked.transform.localScale.Set(masked.transform.localScale.x+ddH/maxHealths,masked.transform.localScale.y,masked.transform.localScale.z);
		//mask.transform.position =new Vector2(mask.transform.position.x-dH,mask.transform.position.y);
		//masked.transform.position =new Vector2(masked.transform.position.x+dH,masked.transform.position.y);
		//Debug.Log(mask.transform.localScale.x + "   "+ healths);
		//if(mask.transform.position.x<=-110f)
		//{
		//	Time.timeScale = 0.0f;
		//}
	}
}
