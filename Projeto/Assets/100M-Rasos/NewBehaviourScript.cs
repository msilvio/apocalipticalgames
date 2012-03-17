using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	
	
	public bool estadoJogo = false;
	
	void OnGUI()
    {
        GUILayout.Label("Tempo:");		
    }	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
    	{
			Time.timeScale = 0;
		}
	}
}
