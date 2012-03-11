/*
 * @Author: Luiz Pericolo 
 * 
 * 
 */

using UnityEngine;
using System.Collections;


public enum PlayerState{
	OK,
	TAKING_DAMAGE,
	RELOADING,
	ZOOMED,
	DEAD
}

public class ShooterBehaviour : MonoBehaviour {

	
	//Camera Stuff
	public float zoomedFov = 25;
	public float defaultFov = 60;
	public Texture normalCrosshair;
	public Texture zoomedCrosshair;
	private Camera camT;
	Renderer[] renderers;
	public Transform audioSource;
	
	
	
	//For the Shooting and Score
	public int ammo;
	public AudioClip bulletHit;
	public AudioClip fireRifle;
	public AudioClip crowdCheer;
	private int maxAmmo = 10;
	
	//On Screen Stuff
	private Transform scoreP;
	private PointScore pointScript;
	public Transform scorePrefab;
	public Transform reloadPrefab;
	
	private EnemyBehaviour zombieScript;
	
	//Game Controller watches over you!
	private Transform gameController;
	private GameControllerBehaviour controllerScript;
	
	//Player positioning and Movement
	public int maxLives = 10;
	private int nLives;
	private GameObject[] shootPostsT;
	private int postId = 0;
	private int shootingPosIdx = 4;
	private PlayerState pState;
	private PlayerState lastState;
	
	//Raycast stuff
	private RaycastHit hit;
	private GameObject hitGameObj;
	public float normalRange = 100;
	public float scopedRange = 250;

	
	void Start () {
		nLives = 5;
		this.camT = Camera.mainCamera;
		this.pState = PlayerState.OK;
		this.shootPostsT = GameObject.FindGameObjectsWithTag("ShootingPost");
		
		
		sortArrayByPosX(shootPostsT);
		
		gameController = GameObject.FindGameObjectWithTag("GameController").transform;
		controllerScript = gameController.GetComponent<GameControllerBehaviour>();
		
		this.transform.position = shootPostsT[postId].transform.position;	
		renderers = this.gameObject.GetComponentsInChildren<Renderer>();
		
	}
	
	
	void Update () {
		doMovement();
		doInputs();
		checkReloadOver();
		checkDeath();
	}
	
	void checkDeath(){
		if(this.pState == PlayerState.DEAD){
			Destroy(this.gameObject.GetComponent<MouseLook>());
		} 
	}
	
	
	
	void doMovement(){
		if(pState != PlayerState.DEAD){
			if(Input.GetKeyDown(KeyCode.A) && (postId > 0) ){
				postId--;
				this.transform.position = shootPostsT[postId].transform.position;
			}else if(Input.GetKeyDown(KeyCode.D) && (postId < shootingPosIdx)){
				postId++;
				this.transform.position = shootPostsT[postId].transform.position;
			}	
		}
		
	}
	
	void doInputs(){
		if(Input.GetButtonDown("Fire1") && this.ammo > 0 && (pState != PlayerState.RELOADING) && (pState != PlayerState.DEAD)){
			AudioSource.PlayClipAtPoint(fireRifle, this.camT.transform.position);
			
			if(Physics.Raycast(camT.transform.position, camT.transform.forward, out hit, getRange())){
				if(hit.collider.CompareTag("Zombie")){
					zombieScript = hit.collider.gameObject.GetComponent<EnemyBehaviour>();
					AudioSource.PlayClipAtPoint(bulletHit, hit.point);
					audioSource.audio.PlayOneShot(crowdCheer);
					scoreP = Instantiate(scorePrefab, hit.point, Quaternion.identity) as Transform;
					pointScript = scoreP.GetComponent<PointScore>();
					pointScript.setScore(zombieScript.getPoints());
					pointScript.releaseGo();
					controllerScript.addToScore(zombieScript.getPoints());
					zombieScript.setState(EnemyState.DEAD);
					
				}
				else if(hit.collider.CompareTag("Target")){
					AudioSource.PlayClipAtPoint(bulletHit, hit.point);
					audioSource.audio.PlayOneShot(crowdCheer);
					scoreP = Instantiate(scorePrefab, hit.point, Quaternion.identity) as Transform;
					pointScript = scoreP.GetComponent<PointScore>();
					pointScript.setScore(controllerScript.getTargetScore());
					controllerScript.addTargetPoint();
					pointScript.releaseGo();
					
					Destroy(hit.collider.gameObject);
					
				}
			}
			ammo--;
		}
		if(Input.GetButtonDown("Fire2")){
			
			
			if(this.pState == PlayerState.OK){
				this.lastState = this.pState;
				this.pState = PlayerState.ZOOMED;
				this.camT.fov = zoomedFov;
				
				foreach(Renderer r in renderers){
					r.enabled = false;
				}
				
				
				
			}
			else if(this.pState == PlayerState.ZOOMED){
				this.pState = PlayerState.OK;
				this.lastState = PlayerState.ZOOMED;
				this.camT.fov = defaultFov;
				
				foreach(Renderer r in renderers){
					r.enabled = true;
				}
				
				
				
			}
		}
		
		//Reload
		if(Input.GetKeyDown(KeyCode.R)){
			this.lastState = this.pState;
			this.pState = PlayerState.RELOADING;
			Instantiate(reloadPrefab, Vector3.zero, Quaternion.identity);
		}
	}
	
	void checkReloadOver(){
		if(GameObject.FindGameObjectWithTag("Reload") == null && this.pState == PlayerState.RELOADING){
			this.ammo = maxAmmo;
			this.pState = this.lastState;
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
	
	public int getAmmo(){
		return this.ammo;
		
	}
			
	public float getRange(){
		if(this.pState == PlayerState.OK){
			return normalRange;			
		}
		if(this.pState == PlayerState.ZOOMED){
					return scopedRange;
		}
		
		return 0;
	}
	
	public Texture getCrosshair(){
		if(pState == PlayerState.OK){
			return normalCrosshair;
		}
		else if(pState == PlayerState.ZOOMED){
			return zoomedCrosshair;	
		}
		
		return null;
		
		
	}
	
	public void reset(){
		nLives = maxLives;
	}
	
	public void loseLife(){
		if(this.nLives > 0){
			this.nLives --;
		}
	}
	
	public int getnLives(){
		return this.nLives;
	}
	
	public Camera getCamera(){
		return this.camT;
	}
	
	public void playerDied(){
		this.pState = PlayerState.DEAD;
	}
	
	void OnGUI(){
		if(pState == PlayerState.OK){
			GUI.DrawTexture(new Rect(0.5f*(Screen.width - normalCrosshair.width),0.5f*(Screen.height - normalCrosshair.height),normalCrosshair.width, normalCrosshair.height),normalCrosshair);	
		}
		else if(pState == PlayerState.ZOOMED){
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),zoomedCrosshair,ScaleMode.StretchToFill);
		}
		
	}
	
	
	
}
