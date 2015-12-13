using UnityEngine;
using System.Collections;

public class MusicBall : MonoBehaviour {

    /* Side A (pond)
     *  piano, drum, guitar, clarinet 
     * Side B (casino)
     */
    public string note;
    public string[] soundsA; //pond
    public string[] soundsB; //casino

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
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y <= -10)
            this.transform.position = new Vector3(Random.Range(-4, 4),40, Random.Range(-4, 4));
        if (note == null) {
            //TODO: initialize with the side that this ball is on
        }
	}

    void Initialize(bool sideA) {



        if (sideA) {

        } else {

        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Flipper_1")
            Debug.Log("hi");
        if (col.gameObject.name == "Pinball" || col.gameObject.name == "Bumper")
        {
            Music_Controller musicSource = GameObject.FindObjectOfType<Music_Controller>();
            musicSource.increaseVolume(musicSource.instruments[0]);
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10))); 
        }
    }
}
