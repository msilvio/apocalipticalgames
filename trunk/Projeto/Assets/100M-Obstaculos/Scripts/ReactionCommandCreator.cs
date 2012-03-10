using UnityEngine;
using System.Collections;

public class ReactionCommandCreator : MonoBehaviour
{
	string[] ReactionCheck = new string[4];
	string[] ReactionCombo;
	public Texture up, down, left, right;
	int i= 0;
	int numCommands;
	
	public static ReactionCommandCreator reactionCommand;
	
	void Awake()
	{
		reactionCommand = this;	
	}
	
	public void DynamicCreator ()
	{
		ReactionCombo = new string[4];
		int random = Random.Range (2, 4);
		numCommands = random;
		for (int i = 0; i < random; i++) {
			int random2 = Random.Range (0, 4);
			switch (random2) {
			case 0:
				ReactionCombo [i] = "Left";
				break;
			case 1:
				ReactionCombo [i] = "Right";
				break;
			case 2:
				ReactionCombo [i] = "Up";
				break;
			case 3:
				ReactionCombo [i] = "Down";
				break;
			}	
			Debug.Log (ReactionCombo[i]);
		}
	}
	
	public void CheckCommands ()
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			ReactionCheck [i] = "Left";
			i++;
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			ReactionCheck [i] = "Right";
			i++;
		}
		else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			ReactionCheck [i] = "Up";
			i++;
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			ReactionCheck [i] = "Down";
			i++;
		}

		//Debug.Log (ReactionCheck [i]);
	}
	
	public bool ArrayCheck()
	{
		//return(ReactionCheck == ReactionCombo);
		
		for (int i=0; i<4; i++)
		{
			if (ReactionCheck[i] != ReactionCombo[i]) return false;
		}
		
		return true;
	}
	
	void Start()
	{
		
		//DynamicCreator();
		
	}
	
	void Update()
	{
		
		CheckCommands();
		
	}
	
	void OnDetector()
	{
		i = 0;
		ReactionCheck = new string[4];
		Debug.Log("WUT");
		DynamicCreator();	
	}
	
	void OnGUI()
	{
		for (int z = 0; z < numCommands; z++)
		{
			switch (ReactionCombo[z])
			{
			case "Left":
				GUI.DrawTexture(new Rect(Screen.width/2 + 80*z, Screen.height*(3/4), left.width, left.height), left);
				break;
			case "Right":
				GUI.DrawTexture(new Rect(Screen.width/2 + 80*z, Screen.height*(3/4), left.width, left.height), right);
				break;
			case "Up":
				GUI.DrawTexture(new Rect(Screen.width/2 + 80*z, Screen.height*(3/4), left.width, left.height), up);
				break;
			case "Down":
				GUI.DrawTexture(new Rect(Screen.width/2 + 80*z, Screen.height*(3/4), left.width, left.height), down);
				break;
			}
		}
	}
	
}
