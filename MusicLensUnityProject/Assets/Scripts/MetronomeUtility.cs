using UnityEngine;
using System.Collections;
using CWRU.Common.HololensInput;

// Utility class called on by the four different types of metronomes for various metronome fields and actions such as BPM and playing clicks.
public class MetronomeUtility : MonoBehaviour {

	// Fields
	private int timeSignatureNumerator = 4;
	private int timeSignatureDenominator = 4;
	public int BPM;
	public int subdivisions = 0;
	public bool isPlaying;
	public AudioSource hardClick;
	public AudioSource softClick;

	void Update() {

	}

	// Gets the BPM from the text on the UI button showing the tempo
	public int getBPM() {
		TempoViewActions BPMView = GameObject.Find ("TempoView").GetComponent<TempoViewActions> ();
		return BPMView.tempo;
	}

	// Returns whether or not the metronome is playing
	public bool getIsPlaying() {
		return isPlaying;
	}

	// Returns the type of subdivisions the user has chosen
	public int getSubdivisions() {
		string name = "";
		SubdivisionSelection[] sds = GameObject.FindObjectsOfType<SubdivisionSelection> ();

		for (int i = 0; i < sds.Length; i++) {
			if (sds [i].isSelected) {
				name = sds [i].gameObject.name;
			}
		}

		if (name == "DownBeat") {
			return 0;
		} else if (name == "EighthNote") {
			return 1;
		} else if (name == "Triplets") {
			return 2;
		} else if (name == "Sixteenth") {
			return 3;
		} else {
			return -1;
		}
	}

	// Plays the harder click (downbeat)
	public void playHardClick() {
		hardClick.Play ();
	}

	// Plays the softer click (upbeats/subdivisions)
	public void playSoftClick() {
		softClick.Play ();
	}

	// Calculates the interval between each tick by using the BPM and time signature
	public double getIntervalBetweenClicks() {
		return (60.0/BPM) * getFactorRelativeToFourFour(timeSignatureDenominator);
	}

	// Calculates the coefficient that the interval should be multiplied by if the denominator is different from 4
	private double getFactorRelativeToFourFour(int denominator) {
		return 4.0 / denominator;
	}

	public int CalcDownBeats() {
		if (timeSignatureDenominator <= 4) {
			return timeSignatureNumerator;
		} else if (timeSignatureDenominator == 8 || timeSignatureDenominator == 16) {
			// 6/8 is 2 downbeats, 7/8 is 3 down beats, etc.
			return (int)System.Math.Ceiling(timeSignatureNumerator / 3F);
		} else {
			//TODO: Add support for 32nd and 64th note time signatures
			throw new System.Exception("Invalid time signature.");
		}
	}

}
