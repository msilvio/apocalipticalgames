using UnityEngine;
using System.Collections;

public class ReloadBehaviour : MonoBehaviour {
	
	public Texture reloadBackground;
	public Texture reloadForeground;
	public float reloadTime;
	
	private float widthOffset;
	private float heightOffset;
	private float completion;
	private float actualTimer;
	
	void Awake() {
		actualTimer = reloadTime;
		
		widthOffset = reloadBackground.width/2;
		heightOffset = reloadBackground.height/2;
	}
	
	
	void Update() {
		if(actualTimer <= 0){
			Destroy(this.gameObject);
		}
		else{
			actualTimer -= Time.deltaTime;
			completion = 1.0f - (actualTimer / reloadTime);
		}
	}
	
	void OnGUI(){
		
		GUI.DrawTexture(new Rect(Screen.width/2 - widthOffset, (3*Screen.height/4) - heightOffset, reloadBackground.width, reloadBackground.height/8),reloadBackground);
		GUI.DrawTexture(new Rect(Screen.width/2 - widthOffset, (3*Screen.height/4) - heightOffset, reloadBackground.width * completion, reloadBackground.height/8),reloadForeground);
	}
}
