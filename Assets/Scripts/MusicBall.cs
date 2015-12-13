﻿using UnityEngine;
using System.Collections;

public class MusicBall : MonoBehaviour {

    public string note;
	private Music_Controller musicSource;
	private bool sideA;

	// Use this for initialization
	void Start () {
		musicSource = GameObject.FindObjectOfType<Music_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y <= -10)
            this.transform.position = new Vector3(Random.Range(-4, 4),40, Random.Range(-4, 4));
	}

    public void Initialize(bool onSideA, string musicNote) {
		sideA = onSideA;
		note = musicNote;

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Flipper_1")
            Debug.Log("hi");
        if (col.gameObject.name == "Pinball" || col.gameObject.name == "Bumper")
        {
            musicSource.increaseVolume(musicSource.instruments[0]);
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10))); 
        }
    }
}
