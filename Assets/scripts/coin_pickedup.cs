using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_pickedup : MonoBehaviour {

	public AudioSource _pickupsound;
	public GameObject _newcoin;
	public Transform _coinspawn;
	public float _coinspawntime;

	void Update(){
		
	}

	void OnCollisionEnter2D(Collision2D col){

		Debug.Log ("COLLAISION" + gameObject.active);
		_pickupsound.Play ();
		
		if (col.gameObject.tag == "Player"){
			_pickupsound.Play ();
			gameObject.SetActive(false);
			Invoke("CreateCoin", _coinspawntime);

		}
	}

	void CreateCoin(){
		Instantiate (_newcoin,_coinspawn.position,_coinspawn.rotation);
	}

}
