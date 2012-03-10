using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
		public float movespeed = 3.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			transform.Translate(Vector3.right * movespeed * Time.deltaTime);
	}
}
