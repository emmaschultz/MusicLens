using UnityEngine;
using System.Collections;

// Class used for clicking on the "Change Tempo" button. Launches the sub-buttons of this button.
namespace CWRU.Common.HololensInput {
	public class ChangeTempoActions : UIUtility {
		
		public override void onClick () {
			bool areButtonsActive = base.isButtonGroupActive ("SubChangeTempo");
			//If the children are not active, set them active and animate them out
			if (!areButtonsActive) {
				base.toggleActive ("SubChangeTempo", !areButtonsActive);
				base.animateByTag ("SubChangeTempo", !areButtonsActive);
			// If the children are active, set them inactive and animate them in
			} else {
				base.toggleActive ("SubChangeTempo", false);
			}
		}

	}
}
