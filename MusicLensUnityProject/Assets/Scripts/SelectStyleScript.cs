using UnityEngine;
using System.Collections;

// Class for changing the type of metronome displayed
namespace CWRU.Common.HololensInput {
	public class SelectStyleScript : UIUtility {

		// The different types of metronomes
		public GameObject classic;
		public GameObject ball;
		public GameObject redDot;
		public GameObject lightFlash;

		// When a metronome type button is clicked, the displayed metronome is changed to the one selected
		public override void onClick () {
			// Sets all of the metronome types inactive 
			classic.SetActive (false);
			ball.SetActive (false);
			redDot.SetActive (false);
			lightFlash.SetActive (false);

			// Sets active the metronome type selected
			if (this.name.Equals ("Classic")) {
				classic.SetActive (true);
			} else if (this.name.Equals ("Ball")) {
				ball.SetActive (true);
			} else if (this.name.Equals ("Red Dot")) {
				redDot.SetActive (true);
			} else if (this.name.Equals ("Light Flash")) {
				lightFlash.SetActive (true);
			}
		}
	}
}
