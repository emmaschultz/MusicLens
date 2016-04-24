using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Class for the "Change Theme" button in the main menu. When this button is clicked, 
// the buttons with different colors reflecting themes come out or go back into the "Change Themes" menu button.
namespace CWRU.Common.HololensInput {
	public class ChangeTheme : UIUtility {

		// Hide the color option buttons on launch
		void Start () {
			base.toggleActive ("ColorOptions", false);
		}

		public override void onClick () {
			bool areButtonsActive = base.isButtonGroupActive ("ColorOptions");
			//If the children are not active, set them active and animate them out
			if (!areButtonsActive) {
				base.toggleActive ("ColorOptions", true);
				base.animateByTag ("ColorOptions", true);
			//If the children are active, set them inactive and animate them in
			} else {
				base.animateByTag ("SubChangeStyleMenu", false);
				base.toggleActive ("ColorOptions", false); //In the future, make these !children active when you do animate up
			}
		}
	}
}