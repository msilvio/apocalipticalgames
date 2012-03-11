/*
 * @Author: Luiz Pericolo 
 * 
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControllerBehaviour : MonoBehaviour {

	public int targetScore;
	public Transform[] enemyPrefabs;
	public Transform targetPrefab;
	public float spawnTime;
	public float decreasingFactor;
	public AudioClip gameOverSound;
	public Texture healthHeart;
	public Texture ammoTexture;
	public float firstPositionHeartX;
	public float firstPositionAmmoX;
	
	private GameObject[] spawnPoints;
	private GameObject[] objectivePoints;
	private float actualTimer;
	private int roundScore;
	private float factor = 1.0f;
	private int spawnPos;
	private int targetOrZombie;
	private int zombieType;
	private GameObject player;
	private GameObject zombie;
	private ShooterBehaviour playerScript;
	private Transform target;
	private Texture crosshairTexture;
	private int lastSpawnPos = -1;
	private EnemyBehaviour zombieScript;
	private bool continueSpawning;
	private AudioSource audioSource;
	
	private GameObject highscoreGO;
	private HighscoreKeeperBehaviour highscoreScript;
	
	GUIStyle style = new GUIStyle();
	
	void Awake(){
		this.continueSpawning = true;
		
	}
	
	void Start() {
		
		spawnPoints = GameObject.FindGameObjectsWithTag("SpawningPos");
		player = GameObject.FindGameObjectWithTag("MainCamera");
		playerScript = player.GetComponent<ShooterBehaviour>();
		objectivePoints = GameObject.FindGameObjectsWithTag("Objective");
		
		highscoreGO = GameObject.FindGameObjectWithTag("Highscore");
		highscoreScript = highscoreGO.GetComponent<HighscoreKeeperBehaviour>();
		
		audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
		
		
		if(audioSource == null){
			Debug.Log("No Audio Source");	
		}
		
		
		sortArrayByPosX(spawnPoints);
		sortArrayByPosX(objectivePoints);
		
		firstPositionHeartX = 0.0f;
		
		actualTimer = spawnTime;
	}
	
	
	void Update() {
		if(continueSpawning){
			doTimer();
			checkGameOver();
		}
		
	}
	
	void doTimer(){
		if(actualTimer <= 0){
			
			spawnPos = Random.Range(0,4);
			
			while(spawnPos == lastSpawnPos){
				spawnPos = Random.Range(0,4);
			}
			
			lastSpawnPos = spawnPos;
			
			targetOrZombie = Random.Range(0,2);
			if(targetOrZombie == 0){
				target = (Transform) Instantiate(targetPrefab, spawnPoints[spawnPos].transform.position, Quaternion.AngleAxis(180, this.transform.right	));
				target.transform.position += new Vector3(0, Random.Range(1.5f, 5f), 0);
				target.rigidbody.velocity = target.transform.forward * 10;
			}
			else{
				zombieType = Random.Range(0,2);
				Instantiate(enemyPrefabs[zombieType], spawnPoints[spawnPos].transform.position, Quaternion.AngleAxis(180, this.transform.up));
			}
			resetTime();
		}
		else{
			actualTimer -= Time.deltaTime;
		}
	}
	
	void resetTime(){
		this.factor-= this.decreasingFactor;
		this.actualTimer = spawnTime;
		this.actualTimer *= factor;
	}
	
	void checkGameOver(){
		if(playerScript.getnLives() == 0){
			//game over kthxbby
			playerScript.playerDied();
			
			this.continueSpawning = false;
			this.actualTimer = spawnTime;
			highscoreScript.setLastRoundScore(this.getRoundScore());
			
			playAndWait(gameOverSound, audioSource, gameOverSound.length);
			
			
			Application.LoadLevel("gameOverScore");
		}
	}
	
	void playAndWait(AudioClip clip, AudioSource source, float time){
		
		audioSource.PlayOneShot(clip);
		
		
		while(time > 0){
			time -= Time.deltaTime;
			Debug.Log("Falta: "+time);
		}
		
		Debug.Log("Parou de tocar");	
		return;
	}
	
	public int getTargetScore(){
		return targetScore;
	}
	
	public void addTargetPoint(){
		this.roundScore += targetScore;
	}
	
	public void addToScore(int inc){
		this.roundScore += inc;
	}
	
	public int getRoundScore(){
		return this.roundScore;
	}
	
	public void resetScore(){
		this.roundScore = 0;
	}
	
	public void resetLevel(){
		this.continueSpawning = true;
		this.playerScript.reset();
		Start();
	}
	
	public float getNewPositionHeart(int i){
		return firstPositionHeartX + (i * healthHeart.width);
	}
	
	public float getNewPositionAmmo(int i){
		return firstPositionAmmoX + (i * ammoTexture.width);
	}
	
	private float getHalfHeightScreen(){
		return Screen.height / 2;
	}
	
	private float getHalfWidthScreen(){
		return Screen.width / 2;
	}
	
	private float getPositionAmmoY(){
		return Screen.height - ammoTexture.height;
	}
	
	private float getPositionHeartY(){
		return 0;
	}
	
	
	void OnGUI(){
		GUI.Label(new Rect(0, getHalfHeightScreen(), 100, 20),"Score: " + this.roundScore);
		//GUI.Label(new Rect(0, 25, 100, 20),"Ammo: " + playerScript.getAmmo());
		//GUI.Label(new Rect(0, 50, 100, 20), "Lives: " + playerScript.getnLives());
		
		for(int i = 0; i < playerScript.getnLives(); i++){
			GUI.DrawTexture(new Rect(getNewPositionHeart(i), getPositionHeartY(), healthHeart.width, healthHeart.height), healthHeart);
			//posicaoXInicial = posicaoXInicial + guiTexture.pixelInset.width;	
		}
		
		for(int j = 0; j < playerScript.getAmmo(); j++){
			GUI.DrawTexture(new Rect(getNewPositionAmmo(j), getPositionAmmoY(), ammoTexture.width, ammoTexture.height), ammoTexture);	
			//posicaoXInicial = posicaoXInicial + guiTexture.pixelInset.width;	
		}
		
		if(playerScript.getAmmo() <= 0){
			style.fontStyle = FontStyle.Bold;
			style.fontSize = 26;
			GUI.Label(new Rect(getHalfWidthScreen(), getHalfHeightScreen(), 200, 40),"RELOAD", style);
		}
	}
	
	void sortArrayByPosX(GameObject[] array){

		System.Array.Sort(array, delegate(GameObject gO1, GameObject gO2) {
					if(gO1.transform.position.x < gO2.transform.position.x){
						return -1;
					}else if(gO1.transform.position.x == gO2.transform.position.x){
						return 0;
					}else{
						return 1;
					}
			});
	}
}
