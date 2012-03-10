/*
 * @Author: Luiz Pericolo 
 * 
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HigscoreController : MonoBehaviour {
	
	public Texture highscoreTexture;
	
	private string name ="<Your Name>";
	private int lastRoundScore;
	
	private int nPlayers;
	//private int[] scores;
	//private string[] names;
	
	private List<int> scores;
	private List<string> names;
	
	private int height = 150;
	private int heightOffset = 15;
	private bool scoreSet;
	
	private GameObject scoreKeeper;
	private HighscoreKeeperBehaviour scoreKeeperScript;
	
	void Start () {
		
		scores = new List<int>();
		names = new List<string>();
		
		scoreSet = false;
		
		scoreKeeper = GameObject.FindGameObjectWithTag("Highscore");
		scoreKeeperScript = scoreKeeper.GetComponent<HighscoreKeeperBehaviour>();
		
		scores = scoreKeeperScript.getScores();
		names = scoreKeeperScript.getNames();
		nPlayers = scoreKeeperScript.getIndex();
		lastRoundScore = scoreKeeperScript.getLastRoundScore();
		
		
	}

	
	
	void Update () {
		checkInputs();
	}
	
	void checkInputs(){
		if(Input.GetKeyDown(KeyCode.R) && name != "" && scoreSet){
			name = "";
			
			Application.LoadLevel("TiroEsportivo");
		}
		else if(Input.GetKeyDown(KeyCode.M)){
			Application.LoadLevel("IntroScene");
		}
	}
	
	
	void OnGUI(){
		
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), highscoreTexture, ScaleMode.StretchToFill);
		name = GUI.TextArea(new Rect(50, 50, 100, 25),name);
		GUI.Label(new Rect(170, 50, 150, 25), lastRoundScore.ToString());
		
		if(GUI.Button(new Rect(50, 95, 70, 25), "Submit!") && !scoreSet){
			scoreSet = true;
			scoreKeeperScript.setNewScore(name,lastRoundScore);
			
			scores = scoreKeeperScript.getScores();
			names = scoreKeeperScript.getNames();
					
			nPlayers = names.Count;
			scoreKeeperScript.flush();
		}
		
		for(int i=0; i<names.Count; i++){
			GUI.Label(new Rect(1200,getHeight(i),150, 25), names[i]);
			GUI.Label(new Rect(1400,getHeight(i),150, 25), "....... "+scores[i].ToString());
			
		}
		
	
	}
	
	int getHeight(int i){
		return height + (i*heightOffset);
	}
}

