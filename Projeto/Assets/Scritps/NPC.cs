using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
	
	bool morto = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (morto == false)
		{
			animation.Play("dance");
		}
		else if (morto == true)
		{
			// Destroy(this.gameObject);
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		morto = true;
		animation.Play("die");
			
    }
}
