using UnityEngine;
using System.Collections;

public class PointScore : MonoBehaviour {

	public float timer = 2.5f;
	public float score = 15;
	public GUISkin pointSkin;
	public GUISkin pointSkinShadow;
	
	
	private Vector3 pointPosition;
	private float posY;
	private Vector3 screenPos;
	private bool okToGo;

	void Awake () {
		okToGo = false;
		pointPosition = this.transform.position;
		posY = Screen.height / 2;
	}
	
	
	void Update () {
		if(okToGo) doTimer();
	}
	
	void doTimer(){
		if(timer <= 0){
			Destroy(this.gameObject);
		}else{
			timer -= Time.deltaTime;
			posY -= 0.2f;
		}
	}
	
	void OnGUI(){
		
		screenPos = Camera.mainCamera.WorldToScreenPoint(pointPosition);
		GUI.skin = pointSkinShadow;
		GUI.Label(new Rect(screenPos.x + 8, posY-2, 80, 70), "+"+score.ToString());
	}
	
	public void setScore(int score){
		this.score = score;
	}
	
	public void releaseGo(){
		this.okToGo = true;
	}
	
}
