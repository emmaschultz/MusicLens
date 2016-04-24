using UnityEngine;
using System.Collections;

// Class for controlling the sub buttons of the "Change Style" button of the main metronome buttons
namespace CWRU.Common.HololensInput {
	public class MetroUIChangeStyle : UIUtility {

		// Expands or hides the buttons with different options for the style of the metronome
		public override void onClick () {
			bool areButtonsActive = base.isButtonGroupActive ("SubChangeStyleMenu");
			//If the children are not active, set them active and animate them out
			if (!areButtonsActive) {
				base.toggleActive ("SubChangeStyleMenu", !areButtonsActive);
				base.animateByTag ("SubChangeStyleMenu", !areButtonsActive);
			//If the children are active, set them inactive and animate them in
			} else {
				base.toggleActive ("SubChangeStyleMenu", false); //In the future, make these !children active when you do animate up
			}
		}
	}
}
