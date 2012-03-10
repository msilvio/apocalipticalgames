/*
 * @Author: Luiz Pericolo 
 * 
 * 
 */

using UnityEngine;
using System.Collections;

public class IntroScript : MonoBehaviour {
	public Texture introTexture;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		doGameStart();
	}
	
	void doGameStart(){
		if(Input.GetKeyDown(KeyCode.P)){
			Application.LoadLevel("TiroEsportivo");
		}
		else if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
	
	void OnGUI(){
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),introTexture,ScaleMode.StretchToFill);
	}
}
