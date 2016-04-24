using UnityEngine;
using System.Collections;
using CWRU.Common.Text;

// Class for the view that shows what the current tempo is
namespace CWRU.Common.HololensInput {
	public class SubdivisionSelection : HoloButton {

		//public int subdivision = 0;
		public bool isSelected = false;

		// Use this for initialization
		void Start () {
			if (this.gameObject.name == "DownBeat") {
				isSelected = true;
			}
		}
	
		// Update is called once per frame
		void Update () {
	
		}

		public override void onClick () {
			SubdivisionSelection[] sds = GameObject.FindObjectsOfType<SubdivisionSelection> ();
			for (int i = 0; i < sds.Length; i++) {
				sds [i].isSelected = false;
			}
				
			this.isSelected = true;
		}

		public override void onHover () {
		}

		public override void onEndHover () {
		}
	}
}
