using UnityEngine;
using System.Collections;
using MidiJack;
// Before running, make sure that we have a MIDI input and 
// no other programs (this includes other Unity scenes)
// that may redirect the input!

// http://slides.com/robinbaumgarten/arduino-unity3d#/0/16
// https://github.com/charlottepierce/UnityMidiControl
// http://malformedbits.com/2015/03/05/Unity-MIDI-Control.html
// http://malformedbits.com/2015/03/05/Unity-MIDI-Control.html

public class Pinball_Sound_Controller : MonoBehaviour {

	AudioSource[] audios;

	// Use this for initialization
	void Start () {
		Debug.Log (gameObject.name);
		Debug.Log (this);
		// http://answers.unity3d.com/questions/984213/control-audio-volume-of-multiple-audio-sources-on.html
		// http://answers.unity3d.com/questions/306684/how-to-change-volume-on-many-audio-objects-with-sp.html
		audios = GetComponents<AudioSource>();
		// Assume every audio source has one element.
		Debug.Log (audios[0]);
	}

	// Update is called once per frame
	void Update () {
		/*
		for (int key = 0; key < 127; key++) {
		// TODO
		}
		*/
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

		if (MidiMaster.GetKeyUp (48)) {
			increaseVolume();
		} else if (MidiMaster.GetKeyUp (49)) {
			decreaseVolume();
		}
	}

	void increaseVolume()
	{
		Debug.Log ("Increase volume.");
		float increaseAmount = 0.2F;
		audios[0].volume = audios[0].volume + increaseAmount;
	}

	void decreaseVolume()
	{
		Debug.Log ("Decrease volume.");
		float decreaseAmount = 0.1F;
		audios [0].volume = audios [0].volume - decreaseAmount;
	}
}
