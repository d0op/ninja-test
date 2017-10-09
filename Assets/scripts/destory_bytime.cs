using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destory_bytime : MonoBehaviour {

	public float _alivetime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Object.Destroy(gameObject, _alivetime);
	}
}
