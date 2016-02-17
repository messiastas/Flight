using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InterrigateController : MonoBehaviour {
	public Text buttonText;
	public Text log;

	public GameObject leftBrow;
	public GameObject rightBrow;

	public string currentAnswer;
	public string currentState = "firstFlight";

	protected bool needRotateLeftBrow = false;
	protected bool needRotateRightBrow = false;
	protected float angleForLeftBrow;
	protected float angleForRightBrow;
	protected float normalAngleForLeftBrow;
	protected float normalAngleForRightBrow;
	// Use this for initialization
	void Start () {
		//buttonText.text = "First flight?";
		normalAngleForLeftBrow = leftBrow.transform.rotation.eulerAngles.z;
		normalAngleForRightBrow = rightBrow.transform.rotation.eulerAngles.z;
		if (SharedVars.Inst.lastPlayedLevel=="firstLevel")
		{
			checkFirstLevelResults();
		}
	}

	void checkFirstLevelResults()
	{
		if(SharedVars.Inst.resultFirstLevel =="succes")
		{
			currentAnswer = "Since that task I understand that I'm borned as pilot.";
			currentState = "firstFlightSucces";
			buttonText.text = "I see. What about flight academy?";
			needRotateLeftBrow = true;
			angleForLeftBrow = -15f;
			needRotateRightBrow = true;
			angleForRightBrow = 5f;
		} else if(SharedVars.Inst.resultFirstLevel == "fail")
		{
			currentAnswer = "So I failed my first flight, crashed family cargo ship and almost killed myself and grandpa. Not what you'd expected, huh?";
			currentState = "firstFlightFail";
			buttonText.text = "Hm.. And how did you get into the  flight academy?";
			needRotateLeftBrow = true;
			angleForLeftBrow = -15f;
			needRotateRightBrow = true;
			angleForRightBrow = 5f;
		}
		log.text+="Martin: "+currentAnswer+"\n";
	}
	
	// Update is called once per frame
	void Update () {
		if(needRotateLeftBrow)
		{
			leftBrow.transform.rotation = Quaternion.Lerp(leftBrow.transform.rotation, Quaternion.Euler(leftBrow.transform.rotation.eulerAngles.x,leftBrow.transform.rotation.eulerAngles.y,angleForLeftBrow), Time.deltaTime*10);
		} 
		if(needRotateRightBrow)
		{
			rightBrow.transform.rotation = Quaternion.Lerp(rightBrow.transform.rotation, Quaternion.Euler(rightBrow.transform.rotation.eulerAngles.x,rightBrow.transform.rotation.eulerAngles.y,angleForRightBrow), Time.deltaTime*10);
		}
	
	}

	public void onQuestionClick(string command)
	{
		log.text+="Interrogator: "+buttonText.text+"\n";
		switch (currentState)
		{
		case "firstFlight":

			currentAnswer = "My first flight, huh? There were not many actions, I just learned basics on my grandPa's old cargo ship.";
			currentState = "firstFlightAnswer";
			buttonText.text = "And still we need it for the case.";
			needRotateLeftBrow = true;
			angleForLeftBrow = -15f;
			needRotateRightBrow = true;
			angleForRightBrow = 5f;
			break;
		case "firstFlightAnswer":
			currentAnswer = "Oookay, so that was farest planet from civilization, I was 12. Our goats escaped from their corral and spred over our rancho. Grandpa broke his leg before, so he told me to take control over our old slow cargo ship. He was supervising. " +
				"I needed to catch every single goat with magnetic drive. It's safe for goats, but they are not happy about it";
			currentState = "firstFlightConfirm";
			buttonText.text = "Let's go.";
			needRotateLeftBrow = true;
			angleForLeftBrow = 20f;
			needRotateRightBrow = true;
			angleForRightBrow = 13f;
			break;
		case "firstFlightConfirm":
			currentAnswer = "";
			SharedVars.Inst.lastPlayedLevel = "firstLevel";
			Application.LoadLevel(1);
			break;
		case "firstFlightSucces":
			
			currentAnswer = "I get there at 18, it was a place from my dreams. All interns got fast ships, and plenty of interesting tasks.";
			currentState = "academyAnswer";
			buttonText.text = "You learned to pick falling targets there?";
			needRotateLeftBrow = true;
			angleForLeftBrow = -15f;
			needRotateRightBrow = true;
			angleForRightBrow = 5f;
			break;
		case "firstFlightFail":			
			currentAnswer = "Just a  bribe from my family. I never wanted to be a pilot, but they forced me. Maybe they expected for my death during the flights." +
				" Anyway, I enjoyed years at the academy.";
			currentState = "academyAnswer";
			buttonText.text = "You learned to pick falling targets there?";
			needRotateLeftBrow = true;
			angleForLeftBrow = -15f;
			needRotateRightBrow = true;
			angleForRightBrow = 5f;
			break;
		case "academyAnswer":	
			currentAnswer = "Yep, one of the tasks was to pick up supplies boxes right in the air before they fall. ";
			currentState = "goToSecondLevel";
			buttonText.text = "Tell more about it.";
			needRotateLeftBrow = true;
			angleForLeftBrow = -15f;
			needRotateRightBrow = true;
			angleForRightBrow = 5f;
			break;
		case "goToSecondLevel":	

			currentAnswer = "";
			SharedVars.Inst.lastPlayedLevel = "secondLevel";
			Application.LoadLevel(2);
			break;
		}
		log.text+="Martin: "+currentAnswer+"\n";
	}
}
