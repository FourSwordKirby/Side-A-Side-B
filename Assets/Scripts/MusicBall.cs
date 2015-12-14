using UnityEngine;
using System.Collections;

public class MusicBall : MonoBehaviour {

    public string note;
	private Music_Controller musicSource;
	private BallController ballController;
	private bool sideA;
	private GameObject centerPiece; //to rotate

	// Use this for initialization
	void Start () {
		musicSource = GameObject.FindObjectOfType<Music_Controller>();
		ballController = GameObject.FindObjectOfType<BallController> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y <= -10) {
			ballController.RemoveBall(gameObject);
		}
	}

    public void Initialize(bool onSideA, string musicNote) {
		sideA = onSideA;
		note = musicNote;

		//only balls on side B need to play their own sound
		if (!onSideA) {
			GetComponent<AudioSource> ().clip = (AudioClip)Resources.Load (musicNote);
			GetComponent<AudioSource> ().Play ();
			centerPiece = GameObject.Find ("Puddle_Smooth");
		} else {
			centerPiece = GameObject.Find ("Wheel_Roulette");
		}

		//give this thing a force
		gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (Random.Range (-4,4), 2, Random.Range (-4,4)));

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Pinball" || col.gameObject.name == "Bumper" || col.gameObject.name.StartsWith("Railing")) {
			if (sideA) {
				musicSource.increaseVolume (musicSource.instruments [0]);
				this.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (Random.Range (-10, 10), 0, Random.Range (-10, 10))); 
			} else {
				if (GetComponent<AudioSource> ().isPlaying) {
					GetComponent<AudioSource> ().Stop ();
				}
				GetComponent<AudioSource> ().Play ();
			}
		} else if (col.gameObject.name == "TableTrigger") {

			Debug.Log("Trigger");
			//rotate the center piece
			Quaternion start = centerPiece.transform.rotation;
			centerPiece.transform.RotateAround(centerPiece.transform.position,new Vector3(0,1,0),45);
			Quaternion end = centerPiece.transform.rotation;
			centerPiece.transform.rotation = start;
			centerPiece.transform.rotation = 
				Quaternion.Lerp (start, end, Time.deltaTime);

			//trigger
			ballController.TriggerTableFlip(gameObject);
		}
    }
}
