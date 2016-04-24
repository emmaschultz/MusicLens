using UnityEngine;
using System.Collections;

// Class for clicking one of the color theme buttons. Sets all of the UI buttons to the color selected
namespace CWRU.Common.HololensInput {
	public class ButtonColors : UIUtility {

		public override void onClick () {
			// Changes the colors of all the buttons relative to which color option was selected
			changeColorsOfUIButtons ("Button" + gameObject.name);
		}

		// Sets all of the UI buttons to the given color
		private void changeColorsOfUIButtons(string color) {
			changeColorOfAllUIButtons (color);
		}
	}
}
