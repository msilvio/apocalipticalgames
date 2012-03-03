using UnityEngine;
using System.Collections;

public class CameraTrack : MonoBehaviour {
		#region atributos
	
	/// <summary>
	/// Alvo, do tipo transform = posicao
	/// </summary>
	public Transform alvo;
	
	/// <summary>
	/// altura da camera
	/// </summary>
	public float altura=30.0f;
	
	
	/// <summary>
	/// distancia da camera pra tras
	/// </summary>
	public float distancia=5.0f;
	
	/// <summary>
	/// velocidade de atraso da camera
	/// </summary>
	public float velocidade = 2.0f;
	
	#endregion
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		// pega a posicao do alva (bola) e ajusta a posicao da camera (alvo da camera)
		Vector3 posicao = alvo.position + new Vector3(0, altura, -distancia);
		
		// Lerp leva de uma posicao (atual) a outra (nova) com tempo (atraso)
		transform.position = Vector3.Lerp(transform.position, posicao, Time.deltaTime * velocidade);
		
		
	}
}
