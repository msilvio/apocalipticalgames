using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {
	public float movespeed = 3.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.animation.Play("run");
		transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
		
	}
}
