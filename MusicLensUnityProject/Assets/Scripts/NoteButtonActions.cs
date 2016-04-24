using UnityEngine;
using System.Collections;
using System;

// Class for clicking on the note buttons reflecting what note for the tuning fork to play
namespace CWRU.Common.HololensInput {
	public class NoteButtonActions : UIUtility {

		// Fields
		public GameObject tuningFork;
		private Vector3 defaultButtonSize;
		private Vector3 biggerButtonSize;
		public bool isButtonBig = false;
		public GameObject parent;
		public bool makeSmall = false;

		// Use this for initialization
		void Start () {
			defaultButtonSize = this.transform.localScale;
			biggerButtonSize = new Vector3(.4f,.4f,.4f);

			// C note is selected by default
			if (this.gameObject.name == "CNoteButton") {
				this.transform.localScale = new Vector3(.4f,.4f,.4f);
				isButtonBig = true;
			}
		}
	
		// Makes any large button that needs to be small small by scaling it down using a Lerp
		void Update () {
			if (this.makeSmall) {
				StartCoroutine (CR_DoScale (biggerButtonSize, defaultButtonSize, .2f));
				this.makeSmall = false;
			}
		}

		// On click, the tuning fork vibrates and plays the selected tone 
		// Also increases the size of the note button selected and decreases the size of the previous buttons selected
		public override void onClick () {
			// Plays the vibration animation
			Animator animator = tuningFork.GetComponent<Animator> ();
			animator.Play ("TuningForkVibrate");

			// Finds an already expanded button if there is one
			findBigButton ();

			// Only makes the button bigger if it is not already big
			if (!isButtonBig) {
				StartCoroutine (CR_DoScale (defaultButtonSize, biggerButtonSize, .2f));
				isButtonBig = true;
			}

			// Plays the pitch of the note clicked
			playPitch ();

		}

		//Plays a pitch based on which GameObject this is (what button was clicked)
		private void playPitch() {
			PlayPitches playPitches = this.gameObject.GetComponentInParent<PlayPitches> ();
			if (this.gameObject.name == "ANoteButton") {
				playPitches.playANote ();
			} else if (this.gameObject.name == "ASharpNoteButton") {
				playPitches.playASharpNote ();
			} else if (this.gameObject.name == "BNoteButton") {
				playPitches.playBNote ();
			} else if (this.gameObject.name == "CNoteButton") {
				playPitches.playCNote ();
			} else if (this.gameObject.name == "CSharpNoteButton") {
				playPitches.playCSharpNote ();
			} else if (this.gameObject.name == "DNoteButton") {
				playPitches.playDNote ();
			} else if (this.gameObject.name == "DSharpNoteButton") {
				playPitches.playDSharpNote ();
			} else if (this.gameObject.name == "ENoteButton") {
				playPitches.playENote ();
			} else if (this.gameObject.name == "FNoteButton") {
				playPitches.playFNote ();
			} else if (this.gameObject.name == "FSharpNoteButton") {
				playPitches.playFSharpNote ();
			} else if (this.gameObject.name == "GNoteButton") {
				playPitches.playGNote ();
			} else if (this.gameObject.name == "GSharpNoteButton") {
				playPitches.playGSharpNote ();
			}
		}

		// Scales the button from one size (start) to another size (end) 
		IEnumerator CR_DoScale(Vector3 start, Vector3 end, float totalTime) {
			float t = 0;
			do {
				this.transform.localScale = Vector3.Lerp (start, end, t / totalTime);
				yield return null;
				t += Time.deltaTime;
			} while (t < totalTime); 
			this.transform.localScale = end;
			yield break;
		}

		// Returns button that is already big (selected)
		private GameObject findBigButton() {
			NoteButtonActions[] listOfNoteButtonActions = parent.GetComponentsInChildren<NoteButtonActions> ();
			for (int i = 0; i < listOfNoteButtonActions.Length; i++) {
				if (listOfNoteButtonActions [i].isButtonBig && listOfNoteButtonActions [i] != this) {
					listOfNoteButtonActions [i].isButtonBig = false;
					listOfNoteButtonActions [i].makeSmall = true;
					return listOfNoteButtonActions [i].gameObject;
				}
			}
			return null;
		}

	}
}
