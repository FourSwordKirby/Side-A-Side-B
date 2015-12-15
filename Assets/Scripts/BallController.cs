using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MidiJack;

public class BallController : MonoBehaviour {

	private int keyCount = 128;
	private long lastPressed;

	public string[] soundsA; //pond
	public string[] soundsB; //casino
	private Table table;
	private List<GameObject> pinballs;
	private int tableTrigger;

	public List<Texture> textureA;
	public List<Texture> textureB;
	public GameObject pinballPrefab;

	private GameObject roulette;
	private GameObject flower;

	private bool rotating;
	private float rotation;
	private GameObject rouletteCenter;
	private GameObject flowerCenter;

    // Use this for initialization
	void Start () {
		soundsA = new string[4];
		soundsA[0] = "Clarinet";
		soundsA[1] = "Guitar";
		soundsA[3] = "Piano";
		
		soundsB = new string[6];
		soundsB[0] = "Db4";
		soundsB[1] = "Eb4";
		soundsB[2] = "F4";
		soundsB[3] = "Ab4";
		soundsB[4] = "Bb4";
		soundsB[5] = "Db5";

		table = GameObject.Find ("Table").GetComponent<Table> ();
		pinballs = new List<GameObject> ();

		roulette = GameObject.Find ("Roulette_Center");
		flower = GameObject.Find ("Trumpet");

		flowerCenter = GameObject.Find ("Puddle_Smooth");
		rouletteCenter = GameObject.Find ("Wheel_Roulette");

		tableTrigger = 0;
    }
    
    // Update is called once per frame
    void Update () {

		if (!table.IsFlipping ()) {
			// FOR USE WITH MIDI INPUT. Switch with keyboard input if necessary.
			for (int key = 0; key < keyCount; key++)
			{
				if (MidiMaster.GetKey(key) > 0 && System.DateTime.Now.Ticks - lastPressed > 2000000)
				{
					int note;

					lastPressed = System.DateTime.Now.Ticks;

					//gets the note
					if (table.IsOnSideA()) {
						note = key % 3;
					} else {
						switch(key % 12) {
							case 3:
								note = 1;
								break;
							case 5:
								note = 2;
								break;
							case 8:
								note = 3;
								break;
							case 10:
								note = 4;
								break;
							default:
								note = Random.Range (0,1) == 0 ? 0 : 5;
								break;
						}
					}
					GameObject newball = Instantiate(pinballPrefab);
					MusicBall musicball = newball.GetComponent<MusicBall>();
					/* Pond */
					if (table.IsOnSideA()) { 
						musicball.Initialize(true, null, note);
						newball.transform.position = new Vector3(roulette.transform.position.x, roulette.transform.position.y + 4, roulette.transform.position.z);
						newball.GetComponent<MeshRenderer>().material.SetTexture(note, textureA[note]);
					}
					/* Casino */
					else {
						musicball.Initialize(false, soundsB[note], 0);
						newball.transform.position = new Vector3(flower.transform.position.x, flower.transform.position.y + 4, flower.transform.position.z);
						newball.GetComponent<MeshRenderer>().material.SetTexture(note, textureB[note]);
					}
					pinballs.Add(newball);
				}
			}
			
			// FOR USE WITH KEYBOARD INPUT. Switch with MIDI input if necessary.
			string keyPressed = Input.inputString;
			if (keyPressed != "") {
				int key = (int)keyPressed[0]; 
				int rand;

				GameObject newball = Instantiate(pinballPrefab);
				MusicBall musicball = newball.GetComponent<MusicBall>();
					/* Pond */
				if (table.IsOnSideA()) { 
					rand = Random.Range(0,3);
					musicball.Initialize(true, null, rand);
					newball.transform.position = new Vector3(roulette.transform.position.x, roulette.transform.position.y + 4, roulette.transform.position.z);
					newball.GetComponent<MeshRenderer>().material.SetTexture(rand, textureA[rand]);
				}
					/* Casino */
				else {
					rand = Random.Range(0,6);
					musicball.Initialize(false, soundsB[rand], 0);
					newball.transform.position = new Vector3(flower.transform.position.x, flower.transform.position.y + 4, flower.transform.position.z);
					newball.GetComponent<MeshRenderer>().material.SetTexture(rand, textureB[rand]);
				}
				pinballs.Add(newball);
			}
		}

		if (rotating) {
			if (table.IsOnSideA()) {
				rouletteCenter.transform.RotateAround(rouletteCenter.transform.position,
				                                      new Vector3(0,1,0), 4);
				rotation += 4f;
			} else {
				flowerCenter.transform.RotateAround(flowerCenter.transform.position,
				                                    new Vector3(0,1,0), 4);
				rotation += 4f;
			}

			if (rotation >= 180) {
				rotation = 0;
				rotating = false;
			}
		}

		if (tableTrigger >= 6) {
			tableTrigger = 0;
			rotating = false;
			rotation = 0;
			table.flipTable();
		}
	}

	public void RemoveBall(GameObject ball) {
		pinballs.Remove (ball);
		Destroy (ball);
	}
	
	public void TriggerTableFlip(GameObject ball) {
		++tableTrigger;
		rotating = true;
		rotation = 0;
		RemoveBall (ball);
	}

	public void ClearBalls() {
		GameObject[] pins = pinballs.ToArray();
		Debug.Log ("pinballs when trying to flip " + pins.Length);
		pinballs.Clear ();
		foreach (GameObject pinball in pins) {
			Destroy (pinball);
		}
	}
}
