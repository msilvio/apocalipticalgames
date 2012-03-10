using UnityEngine;
using System.Collections;

public class Jump_detector : MonoBehaviour {
	
	bool active = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDetector() {
		
		//	active = true;
	}
	
	void OnTriggerEnter(Collider other)
	{
		active = ReactionCommandCreator.reactionCommand.ArrayCheck();
		if 	((other.tag == "Player") && active)
		{
		other.rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);	
		other.transform.animation.Play("jump_pose");
		}
		else if(other.tag != "Player")
		{
		other.rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
		}
	}
	
}
