using UnityEngine;
using System.Collections;

public class ObjectiveBehaviour : MonoBehaviour {
	
	public AudioClip playerDamage;
	
	private GameObject playerTransform;
	private ShooterBehaviour playerScript;
	private GameObject weapon;
	
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag("MainCamera");
		playerScript = playerTransform.GetComponent<ShooterBehaviour>();
		weapon = GameObject.FindGameObjectWithTag("Weapon");
		
		Physics.IgnoreCollision(weapon.collider, this.collider);
	}

	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Zombie")){
			playerScript.loseLife();
			Destroy(col.gameObject);
			AudioSource.PlayClipAtPoint(playerDamage, playerScript.getCamera().transform.position);
		}
		else if(col.gameObject.CompareTag("Target")){
			Destroy(col.gameObject);
		}
	}
}
