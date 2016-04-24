using UnityEngine;
using System.Collections;

// Class for the exit button of each widgets' menu buttons
namespace CWRU.Common.HololensInput {
	public class UIExitButton : UIUtility {

		// Closes the menu buttons of the widget 
		public override void onClick () {
			if (this.gameObject.name == "Exit Button") {
				base.toggleActive ("MainMetronomeButtons", false);
			} else if (this.gameObject.name == "Exit Tuner") {
				base.toggleActive ("MainTunerButtons", false);
			} else if (this.gameObject.name == "Exit Music") {
				base.toggleActive ("MainSheetMusicButtons", false);
			}
		}
	}
}
