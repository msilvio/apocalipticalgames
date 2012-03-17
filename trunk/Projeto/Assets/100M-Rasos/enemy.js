#pragma strict
var target : Transform; //the enemy's target
var moveSpeed = 3; //move speed
var rotationSpeed = 3; //speed of turning

var myTransform : Transform; //current transform data of this enemy

var object : GameObject;

var perseguirJogador : boolean;// = false;

function Awake()
{
    myTransform = transform; //cache transform data for easy access/preformance
    
    perseguirJogador = false;
}

function Start()
{
     target = GameObject.FindWithTag("Player").transform; //target the player

}

function Update () 
{
	//OnTriggerEnter();
	
	if(perseguirJogador == true)
	{
		//Debug.Log("entrei no if do trigger enter");
    	//rotate to look at the player
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
        Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
        //move towards the player
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
	
	}
}

function OnTriggerEnter (other : Collider) {

	//Debug.Log("entrei no trigger enter");
	
	if (other.gameObject.tag == "Player")
    {
    	perseguirJogador = true;
	}    	

}

function OnTriggerExit (other : Collider) {

	if (other.gameObject.tag == "Player")
    {
    	perseguirJogador = false;
	}    	

}

function OnCollisionEnter (other : Collision){
	
	if (other.gameObject.tag == "Player")
    {
    	Destroy(other.gameObject);
    	Time.timeScale = 0;
	} 
}