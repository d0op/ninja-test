using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour {

	private Animator _animator;
	//private AudioSource _audio;
	public AudioSource _audio2;
	private Rigidbody2D rb;
	public float speed;
	public Vector2 jumpHeight;
	private Dictionary <string, int> directions = new Dictionary<string,int> ();
	private bool canJump = true;
	public GameObject _star;
	private float nextFire;
	public Transform shotSpawn;
	public float fireRate;


	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		rb =  GetComponent<Rigidbody2D>();
		directions.Add ("right", 0);
		directions.Add ("left", 180);
		//_audio = GetComponent<AudioSource>();
	}
		
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.A)) {
			moveplayer ("left");
		}else if (Input.GetKey (KeyCode.D)) {
			moveplayer ("right");
		}else if (Input.GetButton ("Fire1")) {
			playeratk ();
		}else if (Input.GetButton ("Jump") && canJump) {
			playerjump ();
		}else if (Input.GetButton("Fire2") && Time.time > nextFire)
		{
			playercaststar ();
		}
	}

	void playercaststar(){
		_animator.SetTrigger ("playerCaststar");
		nextFire = Time.time + fireRate;
		Instantiate(_star, shotSpawn.position, shotSpawn.rotation);
	}

	void playerjump(){
		rb.AddForce(jumpHeight, ForceMode2D.Impulse);
		canJump = false;

		//Debug.Log ("playerjump: " + "", gameObject);
	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("OnCollisionEnter", gameObject);
		if (col.gameObject.tag == "ground"){
			canJump = true;
		}
	}



	void playeratk(){
		_animator.SetTrigger ("playerAtks");
		_audio2.Play();

		//Debug.Log ("playeratk" + "", gameObject);
	}

	void moveplayer(string direction){

		transform.Translate (Vector3.right * speed * Time.deltaTime); 
		_animator.SetTrigger ("playerRuns");

		if(transform.rotation != Quaternion.Euler(0, directions[direction], 0)){
			transform.rotation = Quaternion.Euler(0, directions[direction], 0);
		}

		//Debug.Log ("moveplayer: " + direction, gameObject);

	}
}

