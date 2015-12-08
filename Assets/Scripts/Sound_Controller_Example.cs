using UnityEngine;
using System.Collections;
using MidiJack;
// Before running, make sure that we have a MIDI input and 
// no other programs (this includes other Unity scenes)
// that may redirect the input!

// http://slides.com/robinbaumgarten/arduino-unity3d#/0/16
// https://github.com/charlottepierce/UnityMidiControl
// http://malformedbits.com/2015/03/05/Unity-MIDI-Control.html

// Attach this script to an AudioSource!
public class Sound_Controller_Example: MonoBehaviour {

	// http://docs.unity3d.com/ScriptReference/AudioSource.html
	// Change audio sources, not the global audio listener!
	AudioSource[] audios;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		// http://answers.unity3d.com/questions/984213/control-audio-volume-of-multiple-audio-sources-on.html
		// http://answers.unity3d.com/questions/306684/how-to-change-volume-on-many-audio-objects-with-sp.html
		audios = GetComponents<AudioSource>();
		audio = audios [0];
		// Assume every audio source has one element.
	}

	int keyCount = 128;
	// Update is called once per frame
	void Update () {
		/*
		// http://docs.unity3d.com/ScriptReference/AudioListener-volume.html
			if (MidiMaster.GetKeyUp (48)) {
				Debug.Log ("MUTE");
				Debug.Log ("Last volume:" + audio.volume);
				AudioListener.volume = 0.0F;
			} else if (MidiMaster.GetKeyUp (49)) {
				Debug.Log ("HALF");
				Debug.Log ("Last volume:" + audio.volume);
				AudioListener.volume = 0.5F;
			} else if (MidiMaster.GetKeyUp (50)) {
				Debug.Log ("FULL");
				Debug.Log ("Last volume:" + audio.volume);
				AudioListener.volume = 1F;
			}
			*/
		// There are two ways of getting a MIDI input via MidiJack:
		// The first is a simple on-off switch.
		/*
		for (int key = 0; key < keyCount; key++) {
			if (key % 2 == 0)
			{
				if (MidiMaster.GetKeyUp(key))
					increaseVolume();
			}
			else // if key % 2 == 1
			{
				if (MidiMaster.GetKeyUp(key))
					decreaseVolume(); 
			}

		}
		*/
		// The second is a continuous hold.

		for (int key = 0; key < keyCount; key++) {
			if (key % 2 == 0)
			{
				if (MidiMaster.GetKey(key) > 0)
					increaseVolume();
			}
			else // if key % 2 == 1
			{
				if (MidiMaster.GetKey(key) > 0)
					decreaseVolume(); 
			}	
		}
		/**/
	}

	void changeVolume(float amount)
	{
		Debug.Log ("Changing volume.");
		float currentVolume = audio.volume;
		float newVolume = currentVolume + amount;
		if (newVolume > 1F) {
			newVolume = 1F;
		} else if (newVolume < 0F) {
			newVolume = 0F;
		}
		audio.volume = newVolume;
	}

	float increaseAmount = 0.02F;
	void increaseVolume()
	{
		Debug.Log ("Increase volume.");
		changeVolume (increaseAmount);
	}

	float decreaseAmount = 0.01F;
	void decreaseVolume()
	{
		Debug.Log ("Decrease volume.");
		changeVolume (-decreaseAmount);
	}
}
