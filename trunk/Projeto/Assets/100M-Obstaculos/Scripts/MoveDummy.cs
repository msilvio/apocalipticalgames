using UnityEngine;
using System.Collections;

public class MoveDummy : MonoBehaviour {
	public float movespeed = 2.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(Vector3.right * movespeed * Time.deltaTime);
		
	}
}
