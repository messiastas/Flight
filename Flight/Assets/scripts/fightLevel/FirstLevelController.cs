using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FirstLevelController : MonoBehaviour {
	public GameObject player;
	protected GUISystem gui;
	protected PlanePilot planePilot;

	public Text grandpaVoice;
	// Use this for initialization
	void Start () {
		planePilot = player.GetComponent<PlanePilot>();
		planePilot.StartLevel();
		PlanePilot.OnMoved +=onMove;
		PlanePilot.OnGetTarget +=onTarget;
		PlanePilot.OnGetDamage +=onDamage;
		StartCoroutine(GrandPaSaying("Okay, little shit, try to hold the wheel in the center and not to kill us.", 4));
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(planePilot.transform.rotation.x);
		if(planePilot.transform.rotation.x<-0.3f)
		{
			StartCoroutine(GrandPaSaying("Hey Martin, you are not Icarus, stay close to the surface! Our cargo sheep can't fly in the open space.", 4));
		}
	}

	void onMove()
	{
		StartCoroutine(GrandPaSaying("Okay, now fly close enough to the goats, so our magnetic drive can get them", 4));
	}

	void onTarget()
	{
		Debug.Log(planePilot.goatsLeft);
		switch (planePilot.goatsLeft)
		{
		case 4:
			StartCoroutine(GrandPaSaying("You are not that stupid I thought, that's what we need, continue.", 4));
			break;
		case 3:
			StartCoroutine(GrandPaSaying("Tht's right kid, another goat with us!", 4));
			break;
		case 2:
			StartCoroutine(GrandPaSaying("Got'ya! stupid  goat. Just two more and I will buy you fresh lemonade.", 4));
			break;
		case 1:
			StartCoroutine(GrandPaSaying("Okay, you're too good for lemonade, I will take you with me to the orbital hunt next month!", 4));
			break;
		case 0: 
			StartCoroutine(GrandPaSaying("The last one! Good job, Martin, time to turn autopilot on and go home!", 4));
			SharedVars.Inst.resultFirstLevel = "succes";
			StartCoroutine(EndLevel(4,0));
			break;
		}
	}

	void onDamage()
	{
		if(planePilot.healths>120)
		{
			StartCoroutine(GrandPaSaying("Shit, it's old, but useful! Be careful with our single ship.", 4));
		} else if(planePilot.healths>90)
		{
			StartCoroutine(GrandPaSaying("You just own me all ypur pocket money, little bastard!", 4));
		} else if(planePilot.healths>50)
		{
			StartCoroutine(GrandPaSaying("We'd better crash now, cause I will kill you if we land!", 4));
		} else if(planePilot.healths>0)
		{
			StartCoroutine(GrandPaSaying("I don't want to die, really! Blame that moment when I asked for your help!", 4));
		} else 
		{
			SharedVars.Inst.resultFirstLevel = "fail";
			StartCoroutine(EndLevel(1,0));
		}
	}


	IEnumerator GrandPaSaying(string speech, float seconds) {
		//grandpaVoice.text = speech;
		yield return new WaitForSeconds(seconds);
		//if(grandpaVoice.text==speech)
			//grandpaVoice.text = "";
	}
	IEnumerator EndLevel(float seconds, int nextScene) {
		yield return new WaitForSeconds(seconds);
		Application.LoadLevel(nextScene);
	}
}
