using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlay : MonoBehaviour {
    public MovieTexture movTexture;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.mainTexture = movTexture;
        movTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (movTexture.isPlaying)
        {

        }
        else
        {
            SceneManager.LoadScene(1);
        }
	}
}
