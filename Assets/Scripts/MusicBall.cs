using UnityEngine;
using System.Collections;

public class MusicBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y <= -10)
            this.transform.position = new Vector3(Random.Range(-4, 4),40, Random.Range(-4, 4));
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Flipper_1")
            Debug.Log("hi");
        if (col.gameObject.name.StartsWith("Flipper") || col.gameObject.name == "Pinball" || col.gameObject.name == "Bumper")
        {
            Music_Controller musicSource = GameObject.FindObjectOfType<Music_Controller>();
            musicSource.increaseVolume(musicSource.instruments[0]);
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10))); 
        } else if(col.gameObject.GetComponent<FlipperController>() != null){
            this.gameObject.GetComponent<Rigidbody>().AddForce(col.gameObject.GetComponent<Rigidbody>().angularVelocity); 
        }
    }
}
