using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// Class used for launching the menu buttons of a widget when they are double clicked
namespace CWRU.Common.HololensInput {
	public class WidgetMenuButtons : UIUtility {

        public GameObject ClickToPlayText;

		void Start () {
			// Hides all of the buttons on start
			toggleActive ("MainMetronomeButtons", false);
			toggleActive ("SubChangeStyleMenu", false);
			toggleActive ("MainTunerButtons", false);
			toggleActive ("MainSheetMusicButtons", false);
		}

		void Awake() {
			// Calls OnDoubleTap method when a double click event fires
			GestureManager.RegisterDoubleTap(OnDoubleTap);
			// Calls OnAirTap method when a click event fires
			GestureManager.RegisterAirTap(OnAirTap);

            SpeechManager.RegisterKeyword("start metronome", OnVoiceCommand);
            SpeechManager.RegisterKeyword("pause metronome", OnVoiceCommand);
            SpeechManager.RegisterKeyword("show menu", OnVoiceCommand);
            SpeechManager.RegisterKeyword("start music", OnVoiceCommand);
            SpeechManager.RegisterKeyword("pause music", OnVoiceCommand);
            SpeechManager.RegisterKeyword("play tone", OnVoiceCommand);
        }

        private void OnVoiceCommand(object sender, SpeechManager.EventInfo e)
        {
            if(e.word == "start metronome")
            {
                MetronomeUtility m = GameObject.FindObjectOfType<MetronomeUtility>();
                m.isPlaying = true;
                // Will play the currently selected tone and vibrate the tuning fork
            }
            else if(e.word == "pause metronome")
            {
                MetronomeUtility m = GameObject.FindObjectOfType<MetronomeUtility>();
                m.isPlaying = false;
            }
            else if(e.word == "show menu")
            {
                RaycastHit hit;
                Camera cam = Camera.main;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000))
                {
                    // Case for clicking the options button for the metronome widget. Opens the menu buttons.
                    if (hit.collider.gameObject.name == "MetronomeOptionsButton" && !base.isButtonGroupActive("MainMetronomeButtons"))
                    {
                        // Variable that reflects whether or not the main metronome buttons are active
                        bool areButtonsActive = base.isButtonGroupActive("MainMetronomeButtons");
                        // Sets the main menu buttons as active if they are inactive or inactive if they are active
                        base.toggleActive("MainMetronomeButtons", !areButtonsActive);
                        // You always want to turn the sub menu buttons off when activating the main menu buttons.
                        // Either you are clicking the main menu buttons open and the sub ones should dissapear, or you're hiding the main menu buttons and therefore the sub buttons. 
                        base.toggleActive("SubChangeStyleMenu", false);
                        base.toggleActive("SubChangeTempo", false);
                        // Animates the main menu buttons, second parameter indicates if you are setting them visible or invisible
                        base.animateByTag("MainMetronomeButtons", !areButtonsActive);
                        // Case for clicking the options button for the tuner widget. Opens the menu buttons.
                    }
                    else if (hit.collider.gameObject.name == "TunerOptionsButton" && !base.isButtonGroupActive("MainTunerButtons"))
                    {
                        // Variable that reflects whether or not the main tuner buttons are active
                        bool areButtonsActive = base.isButtonGroupActive("MainTunerButtons");
                        // Sets the main menu buttons as active if they are inactive or inactive if they are active
                        base.toggleActive("MainTunerButtons", !areButtonsActive);
                        // Animates the main menu buttons, second parameter indicates if you are setting them visible or invisible
                        base.animateByTag("MainTunerButtons", !areButtonsActive);
                    }
                    else if (hit.collider.gameObject.name == "SheetMusicOptionsButton" && !base.isButtonGroupActive("MainSheetMusicButtons"))
                    {
                        // Variable that reflects whether or not the main tuner buttons are active
                        bool areButtonsActive = base.isButtonGroupActive("MainSheetMusicButtons");
                        // Sets the main menu buttons as active if they are inactive or inactive if they are active
                        base.toggleActive("MainSheetMusicButtons", !areButtonsActive);
                        // Animates the main menu buttons, second parameter indicates if you are setting them visible or invisible
                        base.animateByTag("MainSheetMusicButtons", !areButtonsActive);
                    }
                }
            }
            else if(e.word == "start music")
            {
                SheetMusicScript sms = GameObject.FindObjectOfType<SheetMusicScript>();
                if (!sms.isPlaying)
                {
                    sms.playSheetMusic();
                    ClickToPlayText.SetActive(false);
                }
            }
            else if(e.word == "pause music")
            {
                SheetMusicScript sms = GameObject.FindObjectOfType<SheetMusicScript>();
                if (sms.isPlaying)
                {
                    sms.pauseSheetMusic();
                    ClickToPlayText.SetActive(true);
                }
            }
            else if(e.word == "play tone")
            {
                NoteButtonActions[] nbas = GameObject.FindObjectsOfType<NoteButtonActions>();
                foreach (NoteButtonActions nba in nbas)
                {
                    if (nba.isButtonBig)
                    {
                        nba.onClick();
                    }
                }
            }
        }

        // Method for showing and hiding the menu buttons for a given widget
        private void OnDoubleTap(object sender, System.EventArgs e) {
			RaycastHit hit;
			Camera cam = Camera.main;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000))
            {
                // Case for clicking the options button for the metronome widget. Opens the menu buttons.
                if (hit.collider.gameObject.name == "MetronomeOptionsButton" && !base.isButtonGroupActive("MainMetronomeButtons"))
                {
                    // Variable that reflects whether or not the main metronome buttons are active
                    bool areButtonsActive = base.isButtonGroupActive("MainMetronomeButtons");
                    // Sets the main menu buttons as active if they are inactive or inactive if they are active
                    base.toggleActive("MainMetronomeButtons", !areButtonsActive);
                    // You always want to turn the sub menu buttons off when activating the main menu buttons.
                    // Either you are clicking the main menu buttons open and the sub ones should dissapear, or you're hiding the main menu buttons and therefore the sub buttons. 
                    base.toggleActive("SubChangeStyleMenu", false);
                    base.toggleActive("SubChangeTempo", false);
                    // Animates the main menu buttons, second parameter indicates if you are setting them visible or invisible
                    base.animateByTag("MainMetronomeButtons", !areButtonsActive);
                    // Case for clicking the options button for the tuner widget. Opens the menu buttons.
                }
                else if (hit.collider.gameObject.name == "TunerOptionsButton" && !base.isButtonGroupActive("MainTunerButtons"))
                {
                    // Variable that reflects whether or not the main tuner buttons are active
                    bool areButtonsActive = base.isButtonGroupActive("MainTunerButtons");
                    // Sets the main menu buttons as active if they are inactive or inactive if they are active
                    base.toggleActive("MainTunerButtons", !areButtonsActive);
                    // Animates the main menu buttons, second parameter indicates if you are setting them visible or invisible
                    base.animateByTag("MainTunerButtons", !areButtonsActive);
                }
                else if (hit.collider.gameObject.name == "SheetMusicOptionsButton" && !base.isButtonGroupActive("MainSheetMusicButtons"))
                {
                    // Variable that reflects whether or not the main tuner buttons are active
                    bool areButtonsActive = base.isButtonGroupActive("MainSheetMusicButtons");
                    // Sets the main menu buttons as active if they are inactive or inactive if they are active
                    base.toggleActive("MainSheetMusicButtons", !areButtonsActive);
                    // Animates the main menu buttons, second parameter indicates if you are setting them visible or invisible
                    base.animateByTag("MainSheetMusicButtons", !areButtonsActive);
                }
            }
		}

		// Handles single clicking on a widget
		public void OnAirTap (object sender, System.EventArgs e) {
			RaycastHit hit;
			Camera cam = Camera.main;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000))
            {
                // Will start/stop the metronome on a single
                if (hit.collider.gameObject.name == "MetronomeOptionsButton")
                {
                    MetronomeUtility m = GameObject.FindObjectOfType<MetronomeUtility>();
                    m.isPlaying = !m.isPlaying;
                    // Will play the currently selected tone and vibrate the tuning fork
                }
                else if (hit.collider.gameObject.name == "TunerOptionsButton")
                {
                    NoteButtonActions[] nbas = GameObject.FindObjectsOfType<NoteButtonActions>();
                    foreach (NoteButtonActions nba in nbas)
                    {
                        if (nba.isButtonBig)
                        {
                            nba.onClick();
                        }
                    }
                }
                else if (hit.collider.gameObject.name == "SheetMusicOptionsButton")
                {
                    // Play or pause the music
                    SheetMusicScript sms = GameObject.FindObjectOfType<SheetMusicScript>();
                    if (sms.isPlaying)
                    {
                        sms.pauseSheetMusic();
                        ClickToPlayText.SetActive(true);
                    }
                    else
                    {
                        sms.playSheetMusic();
                        ClickToPlayText.SetActive(false);
                    }
                }
            }
		}
	}
}
