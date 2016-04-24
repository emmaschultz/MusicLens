using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Class used for launching the main menu buttons when the "Main Menu" button is clicked
namespace CWRU.Common.HololensInput {
	public class MainMenuControl : UIUtility {

		// Hide main menu buttons on start. We don't want them showing until a user double clicks on the widget.
		void Start () {
			base.toggleActive ("MainMenuSub", false);
		}

		// Update is called once per frame
		void Update () {

		}

		public override void onClick () {
			bool areButtonsActive = base.isButtonGroupActive ("MainMenuSub");
			//If the children are not active, set them active and animate them out
			if (!areButtonsActive) {
				base.toggleActive ("MainMenuSub", !areButtonsActive);
				base.animateByTag ("MainMenuSub", !areButtonsActive);
			//If the children are active, set them inactive and animate them in
			} else {
				base.toggleActive ("MainMenuSub", false); 
			}
			// Makes the themeing option buttons invisible 
			base.toggleActive ("ColorOptions", false);
		}
	}
}