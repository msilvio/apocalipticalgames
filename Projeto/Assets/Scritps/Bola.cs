using UnityEngine;
using System.Collections;

public class Bola : MonoBehaviour {
	
	public float velocidade = 6.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// cria um vetor de movimento
		Vector3 movimento =
			// segundo um eixo de entrada horizontal (.right é positivo ou negativo)
			(Input.GetAxis("Horizontal") * Vector3.right * velocidade) +
			// mais o eixo de entrada vertical (.forward é positivo ou negativo)
			(Input.GetAxis("Vertical") * Vector3.forward * velocidade);
		
		// adicona um aforça (aceleração no corpo rígido do objeto
		rigidbody.AddForce(movimento, ForceMode.Acceleration);
		
	}
	

	
	
	
	
}
