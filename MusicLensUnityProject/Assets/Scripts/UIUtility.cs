using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CWRU.Common.Text;

// Class for all of the user interface aspects, such as showing, hiding, and animating buttons 
namespace CWRU.Common.HololensInput {
	public class UIUtility : HoloButton {

		// Fields
		bool textInitialized = false;
		public static Renderer[] meshRenderersForText;
		public static bool isTextColorBlack = false;

		public GameObject Metronome;
		public GameObject Tuner;
		public GameObject MainMenu;
		public GameObject SheetMusic;

		public HoloButton[] allUIHoloButtons;


		// Populates the allUIButtons with all of the HoloButtons in the scene. This is used for changing the color of the buttons.
		void Start () {
			HoloButton[] metronomeButtons = Metronome.GetComponentsInChildren<HoloButton> ();
			HoloButton[] tunerButtons = Tuner.GetComponentsInChildren<HoloButton> ();
			HoloButton[] mainMenuButtons = MainMenu.GetComponentsInChildren<HoloButton> ();
			HoloButton[] sheetMusicButtons = SheetMusic.GetComponentsInChildren<HoloButton> ();

			List<HoloButton> allButtons = new List<HoloButton>();
			for (int i = 0; i < metronomeButtons.Length; i++) {
				allButtons.Add (metronomeButtons[i]);
			}
			for (int j = 0; j < tunerButtons.Length; j++) {
				allButtons.Add (tunerButtons[j]);
			}
			for (int k = 0; k < mainMenuButtons.Length; k++) {
				allButtons.Add (mainMenuButtons[k]);
			}
			for (int l = 0; l < sheetMusicButtons.Length; l++) {
				allButtons.Add (sheetMusicButtons[l]);
			}
			allUIHoloButtons = allButtons.ToArray ();
		}
			
		public override void onClick () {

		}

		// Changes the color of the button group "tag" to the color "color"
		public void changeColorOfAllUIButtons(string color) {
			for (int i = 0; i < allUIHoloButtons.Length; i++) {
				// Don't change the color of the certain buttons (the theme options buttons and the main option buttons for the widgets)
				if(!allUIHoloButtons[i].name.Contains("OptionsButton") && allUIHoloButtons[i].name != "Red" &&
					allUIHoloButtons[i].name != "Blue" && allUIHoloButtons[i].name != "Gray" &&
					allUIHoloButtons[i].name != "Purple") {
					Renderer[] buttonMeshRenderer = allUIHoloButtons[i].GetComponents<Renderer>();
					Material newMat = Resources.Load(color, typeof(Material)) as Material;
					buttonMeshRenderer [0].sharedMaterial = newMat;
				}
			}
		}

		// Returns true if the mesh renderer on a group of buttons (defined by "tag") is enabled
		public bool isButtonGroupActive(string tag) {
			GameObject[] buttonGroup = GameObject.FindGameObjectsWithTag(tag);
			MeshRenderer[] buttonMeshRenderer = buttonGroup[0].GetComponents<MeshRenderer>();
			return buttonMeshRenderer [0].enabled;
		}

		// Animate open true for showing the buttons, false for hiding the buttons
		public void animateByTag(string tag, bool animateOpen) {
			GameObject[] buttonsToAnimate = GameObject.FindGameObjectsWithTag (tag);
			List<Animation> animationsToPlayOut = new List<Animation>();

			// Populates the list with the animations in each button
			for (int j = 0; j < buttonsToAnimate.Length; j++) {
				animationsToPlayOut.Add(buttonsToAnimate[j].GetComponents<Animation>()[0]); //Make separate one for animating up [1]
			}
				
			Animation[] animationsAsArrayOut = animationsToPlayOut.ToArray();

			// Loops through all the animations to play and plays them.
			for (int i = 0; i < animationsAsArrayOut.Length; i++) {
				if (animateOpen) {
					animationsAsArrayOut [i].Play (); //play animation 0 if animateOpen, animation 1 if !animateOpen
				} else {

				}
			}
		}


		///// PUT ALL OF THE ANIMATIONS INTO ONE ARRAY. CREATE SEPARATE ARRAYS FOR THE NAMES OF THE IN CLIPS AND THE OUT CLIPS USING CLIP.NAME. WHEN GOING TO PLAY, PLAY THE ANIMATION WITH THE CLIPNAME AS THE PARAMETER

		// animate open true for showing the buttons, false for hiding the buttons
		/*public void animateByTag(string tag, bool animateOpen) {
			GameObject[] buttonsToAnimate = GameObject.FindGameObjectsWithTag (tag);

			List<Animation> animations = new List<Animation>();

			List<string> animationsToPlay = new List<string>();
			//List<string> animationsToPlayIn = new List<string>();

			// Populates the list with the animations in each button
			for (int j = 0; j < buttonsToAnimate.Length; j++) {
				Animation tempAnimation = buttonsToAnimate [j].GetComponent<Animation> ();
				//Debug.Log (tempAnimation.clip.name);
				animations.Add (tempAnimation);

				int count = 0;
				foreach (AnimationState clip in tempAnimation) {
					//Debug.Log (clip.name);
					Debug.Log (animateOpen);
					Debug.Log (tempAnimation.GetClipCount());
					COMMENT OUTif (tempAnimation.GetClipCount() > 1 && count == 0) {
						Debug.Log ("ANIMATIONS TO PLAY IN" + clip.name);
						animationsToPlayIn.Add (clip.name);
						count++;
					} else {
						Debug.Log ("ANIMATIONS TO PLAY OUT" + clip.name);
						animationsToPlayOut.Add (clip.name);
					}COMMENT OUT

					if (tempAnimation.GetClipCount() > 1 && !animateOpen && count == 0) {
						//Debug.Log ("Here1");
						animationsToPlay.Add (clip.name);
						count++;
					} else if (animateOpen && count > 0) {
						//Debug.Log ("Here2");
						animationsToPlay.Add (clip.name);
					} else if (tempAnimation.GetClipCount() == 1) {
						//Debug.Log ("Here3");
						animationsToPlay.Add(clip.name);
					}
				}
			}

			//for(int i = 0; i < animationsToPlayOut

			Animation[] animationsAsArray = animations.ToArray ();

			string[] animationsToPlayArray = animationsToPlay.ToArray();
			//string[] animationsAsArrayIn = animationsToPlayIn.ToArray ();

			for (int i = 0; i < animationsAsArray.Length; i++) {
				//if (animateOpen) {
				//Debug.Log(animationsToPlayArray[i]);
					animationsAsArray [i].Play (animationsToPlayArray[i]); //play animation 0 if animateOpen, animation 1 if !animateOpen
					//Debug.Log(animationsAsArrayOut[i].GetClipCount());
				//} else {
					//Play closing animation
					//Debug.Log(animationsAsArrayIn.Length);
					//if (animationsAsArrayIn.Length > 0) {
						//Debug.Log ("Playing animations in");
						//animationsAsArray [i].Play (animationsAsArrayIn [i]);
					//}
				//}
			}
		}*/

		// Set MeshRenderers and BoxColliders of menu buttons active or inactive
		// Set visible is true if you are setting them visible, false if setting them invisible
		public void toggleActive(string tag, bool setVisible) {
			GameObject[] button = GameObject.FindGameObjectsWithTag(tag);
			for (int i = 0; i < button.Length; i++) {
				MeshRenderer[] buttonMeshRenderer = button[i].GetComponents<MeshRenderer>();
				BoxCollider[] buttonBoxCollider = button[i].GetComponents<BoxCollider>();
				buttonMeshRenderer[0].enabled = setVisible;
				buttonBoxCollider [0].enabled = setVisible;
				Renderer[] rend = button [i].GetComponentsInChildren<Renderer> ();
				for (int j = 0; j < rend.Length; j++) {
					rend [j].enabled = setVisible;
				}
			}
			toggleTextActive (tag, setVisible);
		}

		// Sets text of menu buttons active or inactive
		private void toggleTextActive(string tag, bool setVisible) {
			GameObject[] texts = GameObject.FindGameObjectsWithTag(tag);
			for (int i = 0; i < texts.Length; i++) {
				Renderer[] buttonMeshRenderer = texts[i].GetComponents<Renderer>();
				buttonMeshRenderer[0].enabled = setVisible;
			}
		}
			
		public override void onHover ()
		{
			//throw new System.NotImplementedException ();
		} 

		public override void onEndHover ()
		{
			//throw new System.NotImplementedException ();
		}
	}
}
