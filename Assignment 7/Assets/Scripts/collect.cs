using UnityEngine;
using System.Collections;

public class collect : MonoBehaviour {

	AudioSource audio;
	public AudioClip collectSound;

	// Use this for initialization
	void Start () {
		audio = GameObject.Find ("collect audio").GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (audio != null) {
			if (col.CompareTag ("Player")) {
				if (!audio.isPlaying) {
					audio.PlayOneShot (collectSound);
					StatsManager.acorns++;
					Destroy (gameObject);
				}
			}
		}
	}
}
