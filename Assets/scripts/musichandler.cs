using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musichandler : MonoBehaviour {


	public AudioClip[] audioSources;
	private AudioSource current;
	private float nextsongdelay;

	// Use this for initialization
	void Start () {
		current = GetComponent<AudioSource> ();
		PlayNextTrack();
	}
	
	// Update is called once per frame
	void PlayNextTrack () {
		current.clip = audioSources[Random.Range(0, audioSources.Length)];
		current.Play ();
		Invoke( "PlayNextTrack", current.clip.length );
	}

	void LateUpdate(){
		if (Input.GetKey (KeyCode.Q)  && Time.time > nextsongdelay) {
			PlayNextTrack ();
			nextsongdelay = Time.time + 0.5f; 
		}
	}
}
