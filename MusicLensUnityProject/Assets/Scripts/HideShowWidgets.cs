using UnityEngine;
using System.Collections;
using CWRU.Common.Text;

// Class used for hiding a certain widget. For example, clicking a button that says "Hide Metronome" will hide the metronome, 
// then toggle the button to say "Show Metronome." When that button is clicked, the metronome shows and the text is set back to "Hide Metronome"
namespace CWRU.Common.HololensInput {
	public class HideShowWidgets : UIUtility {

		// All of the widget game objects, along with the text of "show/hide [widget]" in the main menu
		public GameObject metronomeWidget;
		public GameObject showHideMetronomeText;
		public GameObject tunerWidget;
		public GameObject showHideTunerText;
		public GameObject sheetMusicWidget;
		public GameObject showHideSheetMusicText;

		// Hide or show whichever widget cooresponds to the button clicked
		public override void onClick () {
			// Case for hiding or showing the metronome widget
			if (this.name.Equals ("HideMetronome")) {
				if (metronomeWidget.activeInHierarchy) {
					toggleMenuButtonForHidingWidgets (false, "Show Metronome", metronomeWidget, "MainMetronomeButtons", showHideMetronomeText, "HideMetronome3DText");
				} else {
					toggleMenuButtonForHidingWidgets (true, "Hide Metronome", metronomeWidget, "MainMetronomeButtons", showHideMetronomeText, "HideMetronome3DText");
				}
			// Case for hiding or showing the tuner widget
			} else if (this.name.Equals ("HideTuner")) {
				if (tunerWidget.activeInHierarchy) {
					toggleMenuButtonForHidingWidgets (false, "Show Tuner", tunerWidget, "none yet", showHideTunerText, "HideTuner3DText");
				} else {
					toggleMenuButtonForHidingWidgets (true, "Hide Tuner", tunerWidget, "none yet", showHideTunerText, "HideTuner3DText");
				}
			// Case for hiding or showing the sheet music widget
			} else if (this.name.Equals("HideSheetMusic")) {
				if (sheetMusicWidget.activeInHierarchy) {
					toggleMenuButtonForHidingWidgets (false, "Show Music", sheetMusicWidget, "MainSheetMusicButtons", showHideSheetMusicText, "HideSheetMusic3DText");
				} else {
					toggleMenuButtonForHidingWidgets (true, "Hide Music", sheetMusicWidget, "MainSheetMusicButtons", showHideSheetMusicText, "HideSheetMusic3DText");
				}
			}
		}

		// Changes the "Show [Widget]" text to "Hide [Widget]" of the buttons in the widget itself and the button in the main menu
		public void toggleMenuButtonForHidingWidgets(bool setActive, string textToDisplay, GameObject widget, string buttonsToHide, GameObject textToChange, string mainMenuButtonToHide) { 
			widget.SetActive (setActive);
			textToChange.GetComponent<HUITextController> ().SetText (textToDisplay);
			if (!showHideMetronomeText.GetComponentInParent<Renderer> ().enabled) {
				// Hides the "Show/Hide [Widget]" Text on the main menu buttons 
				hideMainMenuTextByTag(mainMenuButtonToHide);
			} else if (!showHideSheetMusicText.GetComponentInParent<Renderer> ().enabled) {
				
			}

			if (setActive) {
				// Todo: Remove once you have buttons on the tuner widget
				if (widget != tunerWidget) {
					toggleActive (buttonsToHide, false);
				}
			}
		}

		// Hides the text of the main menu buttons for "show/hide [widget]
		public void hideMainMenuTextByTag(string tag) {
			HUITextController textControl = GameObject.Find(tag).GetComponent<HUITextController>();
			// Needs to get renderers of the chidren objects (letters)
			Renderer[] rend = textControl.GetComponentsInChildren<Renderer> ();
			foreach(Renderer rends in rend) {
				rends.enabled = false;
			}
		}

	}
}
