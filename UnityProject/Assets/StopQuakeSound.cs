using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopQuakeSound : MonoBehaviour {

    AudioSource QuakeSound;
    GameObject QuakeAudioSource;
	// Use this for initialization
	void Start () {
        QuakeAudioSource = GameObject.Find("/Music&SFX/Player/QuakeSound/");
        QuakeSound = QuakeAudioSource.GetComponent<AudioSource>();
        QuakeSound.Stop();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
