using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	
	
	public bool estadoJogo = false;
	public GUIStyle guistyle;
	public GUIStyle guistyleTempo;
	float timeacumulator;
	/*float contagemRegressiva = 4f;
	public bool inicio = false;*/
	
	void OnGUI()
    {
		/*if(inicio == false)
		{
			GUILayout.Label((int)contagemRegressiva + "", guistyleTempo);
		}*/
		//Timer
		if(estadoJogo == false /*&& inicio == true*/)
		{	
        	GUILayout.Label("Tempo:" + (int) timeacumulator);
		}
		//Mensagem de Vitória + Tempo
		if(estadoJogo == true)
		{
			GUILayout.Label("PARABENS!!! VOCE CHEGOU VIVO!", guistyle);
			GUILayout.Label("Tempo:" + (int) timeacumulator, guistyleTempo);
		}
			
    }	
	

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//contagemRegressiva -= Time.deltaTime;
		timeacumulator += Time.deltaTime;		
		//FreezeTime();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
    	{
			estadoJogo = true;
			Time.timeScale = 0;
		}
	}
	
	/*void FreezeTime()
	{
		if(contagemRegressiva <= 0)
		{
			Time.timeScale = 0;
			inicio = false;
		}
		if(contagemRegressiva > 0)
		{
			inicio = true;
			Time.timeScale = Time.deltaTime;
		}
	}*/
}
