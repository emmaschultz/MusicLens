using UnityEngine;
using System.Collections;

// Class for playing pitches used for the tuning fork
[RequireComponent(typeof(AudioSource))]
public class PlayPitches : MonoBehaviour {

	// Audiosources for each note
	public AudioSource APitch;
	public AudioSource ASharpPitch;
	public AudioSource BPitch;
	public AudioSource CPitch;
	public AudioSource CSharpPitch;
	public AudioSource DPitch;
	public AudioSource DSharpPitch;
	public AudioSource EPitch;
	public AudioSource FPitch;
	public AudioSource FSharpPitch;
	public AudioSource GPitch;
	public AudioSource GSharpPitch;

	// Plays a certain note reflected in the name of the method
	public void playANote() { APitch.Play (); }
	public void playASharpNote() { ASharpPitch.Play (); }
	public void playBNote() { BPitch.Play (); }
	public void playCNote() { CPitch.Play (); }
	public void playCSharpNote() { CSharpPitch.Play (); }
	public void playDNote() { DPitch.Play (); }
	public void playDSharpNote() { DSharpPitch.Play ();}
	public void playENote() { EPitch.Play (); }
	public void playFNote() { FPitch.Play (); }
	public void playFSharpNote() { FSharpPitch.Play (); }
	public void playGNote() { GPitch.Play (); }
	public void playGSharpNote() { GSharpPitch.Play (); }
}
