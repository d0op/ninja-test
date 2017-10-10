using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public AudioSource _pickupsound;
	public GameObject _newcoin;
	public Transform _coinspawn;
	public float _coinspawntime;
	public Text _scoretext;
	private int _score;


	public GameObject _herospawn;


	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		rb =  GetComponent<Rigidbody2D>();
		directions.Add ("right", 0);
		directions.Add ("left", 180);
		_score = 0;
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
		}else if (Input.GetKey (KeyCode.W) && canJump){
			playerjump ();
		}else if  (Input.GetButton ("Jump") && canJump) {
			playerBackflip ();
		}else if (Input.GetButton("Fire2") && Time.time > nextFire){
			playercaststar ();
		}else if (Input.GetKey (KeyCode.R)){
			gameObject.transform.position = _herospawn.transform.position;
		}



	}

	//SPAWN E KAOZ JUST NU SKA IN I EGEN SNIPPET TAR MAN NY INNAN SPAWNTID KLAR STACKAS MED SAMMA POS
	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "ground"){
			canJump = true;
		}

		if (col.gameObject.tag == "coin"){
			_pickupsound.Play ();
			_score = _score + 10;
			_scoretext.text = "Score: " + _score.ToString();
			col.gameObject.SetActive(false);
			Invoke("CreateCoin", _coinspawntime);
		}
	}
		
	void CreateCoin(){
		Instantiate (_newcoin,_coinspawn.position * Random.Range(1,10),_coinspawn.rotation);
	}

	void playercaststar(){
		_animator.SetTrigger ("playerCaststar");
		nextFire = Time.time + fireRate;
		Instantiate(_star, shotSpawn.position, shotSpawn.rotation);
	}

	void playerjump(){
		
		rb.AddForce(jumpHeight, ForceMode2D.Impulse);
		canJump = false;

		Debug.Log ("playerjump: " + "", gameObject);
	}

	void playerBackflip(){
		
		float usex = -2;

		if (gameObject.transform.rotation.x > 0) {
			usex = 2;
		}

		Vector2 jumpback = new Vector2 (usex,jumpHeight.y + 1);
		rb.AddForce(jumpback, ForceMode2D.Impulse);
		canJump = false;

	}


	void playeratk(){
		_animator.SetTrigger ("playerAtks");
		_audio2.Play();

		Debug.Log ("playeratk" + "", gameObject);
	}

	void moveplayer(string direction){

		transform.Translate (Vector3.right * speed * Time.deltaTime); 
		_animator.SetTrigger ("playerRuns");

		if(transform.rotation != Quaternion.Euler(0, directions[direction], 0)){
			transform.rotation = Quaternion.Euler(0, directions[direction], 0);
		}

		Debug.Log ("moveplayer: " + direction, gameObject);

	}
}

