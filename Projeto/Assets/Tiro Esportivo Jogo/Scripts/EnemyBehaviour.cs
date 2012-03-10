using UnityEngine;
using System.Collections;


public enum EnemyState {

	IDLE, CHASING, FIGHTING, DEAD

}


public class EnemyBehaviour : MonoBehaviour {

	public float enemySpeed = 2.5f;
	public int pointsPerSlay;
	
	private Transform enemyObjective;	
	private EnemyState eState;
	private ShooterBehaviour playerScript;
	
	
	void Awake() {
		eState = EnemyState.CHASING;
		this.rigidbody.velocity = this.transform.forward * enemySpeed;
		this.playerScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShooterBehaviour>();
		
	}
	

	void LateUpdate() {	
		updateAnimation();
	}
	
	void updateAnimation(){
		switch(eState){
			case EnemyState.CHASING:
				if(!animation.IsPlaying("run")){
					animation.CrossFade("run");
				}
			break;
			
			case EnemyState.DEAD:
				if(!animation.IsPlaying("die")){
					Destroy(this.gameObject);
				}
			
			
			break;
		}
	}
	
	public void callAnimation(string animation){
		this.animation.CrossFade(animation);
	}
	
	public void setObjective(Transform objective){
		this.enemyObjective = objective;
	}
	
	public void setState(EnemyState state){
		switch(state){
			case EnemyState.DEAD:
				this.eState = EnemyState.DEAD;
				animation.Play("die");
				this.rigidbody.velocity = Vector3.zero;
			break;
		}
	}
	
	public int getPoints(){
		return pointsPerSlay;
	}
	
	
}
