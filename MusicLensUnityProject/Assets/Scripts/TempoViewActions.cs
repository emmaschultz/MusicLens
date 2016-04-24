using UnityEngine;
using System.Collections;
using CWRU.Common.Text;

// Class for the view that shows what the current tempo is
namespace CWRU.Common.HololensInput {
	public class TempoViewActions : ChangeTempoActions {

		// Tempo field
		public int tempo = 120;

		// Returns the tempo
		public int getTempo() {
			return this.tempo;
		}

		// Sets the tempo and text for the tempo displayed
		public void setTempo(int Value) {
			this.tempo = Value;
			GameObject.Find ("TempoView3DText").GetComponent<HUITextController> ().SetText(tempo.ToString());
		}
	}
}
