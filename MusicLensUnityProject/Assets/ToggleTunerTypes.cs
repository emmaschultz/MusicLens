using UnityEngine;
using System.Collections;
using CWRU.Common.Text;

// Class for toggling between the standard tuner and the tuning fork
namespace CWRU.Common.HololensInput {
	public class ToggleTunerTypes : HoloButton {

		public GameObject TuningFork;
		public GameObject StandardTuner;
		// The text on the button which allows the user to toggle between the tuner types
		public GameObject TextForButton;

		public override void onClick() {
            BoxCollider collider = GameObject.FindGameObjectWithTag("TuningForkMenuButton").GetComponent<BoxCollider>();
            // If the tuning fork is active on click, enable the standard tuner, change the button text to "Tuning Fork"
            if (TuningFork.activeInHierarchy) {
				TuningFork.SetActive (false);
				StandardTuner.SetActive (true);
				TextForButton.GetComponentInChildren<HUITextController> ().SetText ("Tuning Fork");
                collider.size = new Vector3(1.5f, 3f, 1f);
                collider.center = new Vector3(0f, -1f, 0f);
			// If the standard tuner is active on click, enable the pitch fork, change the button text to "Standard"
			} else {
                collider.size = new Vector3(1f, 1f, 1f);
                collider.center = new Vector3(0f, 0f, 0f);

                StandardTuner.SetActive (false);
				TuningFork.SetActive (true);
				TextForButton.GetComponentInChildren<HUITextController> ().SetText ("Standard");
			}
		}

		public override void onHover ()
		{
			//
		}

		public override void onEndHover ()
		{
			//
		}
	}

}