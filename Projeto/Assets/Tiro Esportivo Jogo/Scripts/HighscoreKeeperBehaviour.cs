/*
 * @Author: Luiz Pericolo 
 * 
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HighscoreKeeperBehaviour : MonoBehaviour {
	
	
	public string scoreArrayStr;
	public string nameArrayStr;
	
	
	private string[] names;
	private int[] scores;
	
	private List<string> scoreNames;
	private List<int> scoreValues;
	private int nPlayers;
	private int lastRoundScore;
	
	void Awake(){
		GameObject.DontDestroyOnLoad(this.gameObject);
		scores = new int[10];
		names = new string[10];
		
		scoreNames = new List<string>();
		scoreValues = new List<int>();
		
		nPlayers = 0;
		
		loadStuff(scoreNames, scoreValues);
	}
	
	void loadStuff(List<string> names, List<int> scores){
		names.AddRange(PlayerPrefsX.GetStringArray(nameArrayStr));
		scores.AddRange(PlayerPrefsX.GetIntArray(scoreArrayStr));
		
		Debug.Log("# Jogadores: "+names.Count);
	}
	
	public void flush(){
		PlayerPrefsX.SetIntArray(scoreArrayStr,this.scoreValues.ToArray());
		PlayerPrefsX.SetStringArray(nameArrayStr,this.scoreNames.ToArray());
	}
	
	
	
	public int getLastRoundScore(){
		return this.lastRoundScore;
	}
	
	public void setLastRoundScore(int score){
		this.lastRoundScore = score;
	}
	
	/*public string[] getNames(){
		return this.names;
	}*/
	
	public List<string> getNames(){
		return this.scoreNames;
	}
	
	/*public int[] getScores(){
		return this.scores;
	}*/
	
	public List<int> getScores(){
		return this.scoreValues;
	}
	
	
	
	public void setNewScore(string name, int score){
		
		int placeIdx = 0;
		
		if(scoreValues.Count == 0){
			scoreValues.Add(score);
			scoreNames.Add(name);

			return;
		}
		
		
		for(int i=0; i< scoreValues.Count; i++){
			if(scoreValues[i] > score){
				placeIdx++;
			}
		}
		
		this.scoreNames.Insert(placeIdx,name);
		this.scoreValues.Insert (placeIdx,score);
		
		//this.scoreNames.Add(name);		
		//this.scoreValues.Add(score);
		
		nPlayers = scoreNames.Count;
	}
	
	public int getIndex(){
		return this.nPlayers;
	}
	
	public void setPlayers(int players){
		this.nPlayers = players;
		
		for(int i=0; i < nPlayers; i++){
			Debug.Log("Name: "+names[i]+" Score: "+scores[i]);
		}
	}
	
	
	
}
