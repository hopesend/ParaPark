using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CM_MicInput : MonoBehaviour 
{
	public AudioSource audioSource;
	public bool debug;
	public bool muteMicrophone;
	public List<string> availableMicrophones;

	private AudioClip microphone;

	void Awake()
	{
		availableMicrophones = new List<string> ();
		debug = false;
		muteMicrophone = false;
		audioSource = this.gameObject.GetComponent<AudioSource> ();
	}

	void Start () 
	{
		availableMicrophones.Add ("Default");
		availableMicrophones.AddRange (Microphone.devices);

	}
	
	void Update () 
	{
		audioSource.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
	}
}
