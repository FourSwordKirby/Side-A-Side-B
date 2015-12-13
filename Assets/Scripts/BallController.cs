using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallController : MonoBehaviour {

	public string[] soundsA; //pond
	public string[] soundsB; //casino
	private Table table;
	private List<GameObject> pinballs;

	public Texture[] textureA;
	public Texture[] textureB;
	public GameObject pinballPrefab;

    // Use this for initialization
	void Start () {
		soundsA = new string[4];
		soundsA[0] = "Clarinet";
		soundsA[1] = "Drum";
		soundsA[2] = "Guitar";
		soundsA[3] = "Piano";
		
		soundsB = new string[6];
		soundsB[0] = "Db4";
		soundsB[1] = "Eb4";
		soundsB[2] = "F4";
		soundsB[3] = "Ab4";
		soundsB[4] = "Bb4";
		soundsB[5] = "Db5";

		table = GameObject.Find ("Table").GetComponent<Table> ();
    }
    
    // Update is called once per frame
    void Update () {

		if (!table.IsFlipping ()) {
			// FOR USE WITH MIDI INPUT. Switch with keyboard input if necessary.
			/*
			for (int key = 0; key < keyCount; key++)
			{
				if (MidiMaster.GetKey(key) > 0)
				{
					int i = key % instrumentCount;
					AudioSource instrument = instruments[i];
					increaseVolume (instrument);
				}
			}
			*/
			
			// FOR USE WITH KEYBOARD INPUT. Switch with MIDI input if necessary.
			string keyPressed = Input.inputString;
			if (keyPressed != "") {
				int key = (int)keyPressed[0]; 
				int rand;

				GameObject newball = Instantiate(pinballPrefab);
				MusicBall musicball = newball.GetComponent<MusicBall>();
				newball.transform.position = new Vector3(Random.Range(-4, 4),40, Random.Range(-4, 4));
					/* Pond */
				if (table.IsOnSideA()) { 
					rand = Random.Range(0,4);
					musicball.Initialize(true, soundsA[rand]);
				}
					/* Casino */
				else {
					rand = Random.Range(0,6);
					musicball.Initialize(false, soundsB[rand]);
				}
			}
		}
	}

	void ClearBalls() {
		while (pinballs.Count > 0) {
			pinballs.Remove (pinballs[0]);
			Destroy(pinballs[0]);
		}
	}
}
