using UnityEngine;
using System.Collections;
using MidiJack;

public class Music_Controller : MonoBehaviour {

	public AudioSource[] instruments;
	int instrumentCount = 0; // Will be modifed on start.
	int keyCount = 128;

	// Use this for initialization
	void Start ()
	{
		AudioSource[] allTracks = this.GetComponentsInChildren<AudioSource> ();
		int l = allTracks.Length;

		// Filter instruments by tag inefficiently.
		string tagValue = "Instrument";
		for (int i = 0; i < l; i++)
		{
			instrumentCount += (allTracks[i].tag == tagValue)? 1 : 0;
		}
		instruments = new AudioSource[instrumentCount];
		int instrumentIndex = 0;
		for (int i = 0; i < l; i++)
		{
			AudioSource track = allTracks[i];
			if (track.tag == tagValue)
			{
				instruments[instrumentIndex] = track;
				instrumentIndex++;
			}
		}

		// Set initial volume of instruments to 0.
		for (int i = 0; i < instrumentCount; i++)
		{
			instruments[i].volume = 0;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// DO NOT TOGGLE THIS.
		for (int i = 0; i < instrumentCount; i++)
		{
			decreaseVolume (instruments[i]);
		}
	}

	void changeVolume(AudioSource audio, float amount)
	{
		float currentVolume = audio.volume;
		float newVolume = currentVolume + amount;
		if (newVolume > 1F) {
			newVolume = 1F;
		} else if (newVolume < 0F) {
			newVolume = 0F;
		}
		audio.volume = newVolume;
//		Debug.Log ("Changed " + audio + " to " + newVolume);
	}
	
	float increaseAmount = 0.08F;
	public void increaseVolume(AudioSource audio)
	{
		changeVolume (audio, increaseAmount);
		//		Debug.Log ("Increasing " + audio);
	}
	
	float decreaseAmount = 0.005F;
	public void decreaseVolume(AudioSource audio)
	{
		changeVolume (audio, -decreaseAmount);
		//		Debug.Log ("Decreasing " + audio);
	}
}
