using UnityEngine;
using System.Collections;

public class ExplosionSound : MonoBehaviour {

    public AudioClip clip;

	// Use this for initialization
	void Start () {
        AudioSource asource = this.GetComponent<AudioSource>();
        asource.PlayOneShot(clip);
	}
}
