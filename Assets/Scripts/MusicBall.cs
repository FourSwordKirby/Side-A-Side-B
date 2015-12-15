using UnityEngine;
using System.Collections;

public class MusicBall : MonoBehaviour {

    public string note;
	private Music_Controller musicSource;
	private BallController ballController;
	private bool sideA;
	private int sound; //instrument

	// Use this for initialization
	void Awake () {
		musicSource = GameObject.FindObjectOfType<Music_Controller>();
		ballController = GameObject.FindObjectOfType<BallController> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y <= -10) {
			ballController.RemoveBall(gameObject);
		}
	}

    public void Initialize(bool onSideA, string musicNote, int instrument) {
		note = musicNote;

		//only balls on side B need to play their own sound
		if (!onSideA) {
			GetComponent<AudioSource> ().clip = (AudioClip)Resources.Load (musicNote);
			GetComponent<AudioSource> ().Play ();
		} else {
			sound = instrument;
            musicSource.increaseVolume(musicSource.instruments[sound]);
		}

		//give this thing a force
        int xmod;
        int zmod;
        if (Random.Range(0, 2) == 0)
            xmod = 1;
        else
            xmod = -1;

        if (Random.Range(0, 2) == 0)
            zmod = 1;
        else
            zmod = -1;

    		gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (xmod * Random.Range (2,4), 3, zmod * Random.Range (2,4)));
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Pinball" || col.gameObject.name == "Bumper" || col.gameObject.name.StartsWith("Railing")) {
			if (sideA) {
				musicSource.increaseVolume (musicSource.instruments [sound]);
			} else {
				if (GetComponent<AudioSource> ().isPlaying) {
					GetComponent<AudioSource> ().Stop ();
				}
				GetComponent<AudioSource> ().Play ();
			}
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10))); 
		} else if (col.gameObject.name == "TableTrigger") {
			ballController.TriggerTableFlip(gameObject);
		}
    }
}
