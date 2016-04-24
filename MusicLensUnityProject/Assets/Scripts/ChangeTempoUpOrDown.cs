using UnityEngine;
using System.Collections;
using CWRU.Common.Text;
using System;

// Class for clicking the "+" and "-" buttons to increase or decrease the tempo.
namespace CWRU.Common.HololensInput {
	public class ChangeTempoUpOrDown : TempoViewActions {
        private bool holdingUp = false;
        private bool holdingDown = false;

        void Awake()
        {
            GestureManager.RegisterHoldStart(OnHoldStart);
            GestureManager.RegisterHoldEnd(OnHoldEnd);

            SpeechManager.RegisterKeyword("increase tempo 10", OnVoiceCommand);
            SpeechManager.RegisterKeyword("decrease tempo 10", OnVoiceCommand);
        }

        private void OnVoiceCommand(object sender, SpeechManager.EventInfo e)
        {
            TempoViewActions tempoView = GameObject.Find("TempoView").GetComponent<TempoViewActions>();

            if(e.word == "increase tempo 10")
            {
                if (tempoView.getTempo() < 300 && tempoView.getTempo() > 20)
                {
                    tempoView.setTempo(tempoView.getTempo() + 10);
                }
            }
            else if(e.word == "decrease tempo 10")
            {
                if (tempoView.getTempo() < 300 && tempoView.getTempo() > 20)
                {
                    tempoView.setTempo(tempoView.getTempo() - 10);
                }
            }
        }

        void Start()
        {
            InvokeRepeating("ChangeTempo", 1.0f, 0.2f);
        }

        private void OnHoldEnd(object sender, EventArgs e)
        {
            RaycastHit hit;
            Camera cam = Camera.main;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000))
            {
                if (hit.collider.gameObject.name == "TempoChangeUp" || hit.collider.gameObject.name == "TempoChangeDown")
                {
                    holdingUp = holdingDown = false;
                }
            }
        }

        private void OnHoldStart(object sender, EventArgs e)
        {
            RaycastHit hit;
            Camera cam = Camera.main;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000))
            {
                if (hit.collider.gameObject.name == "TempoChangeUp")
                {
                    holdingUp = true;
                }
                else if(hit.collider.gameObject.name == "TempoChangeDown")
                {
                    holdingDown = true;
                }
                
            }
        }

        private void ChangeTempo()
        {
			GameObject tickerWeight = GameObject.Find("TickerWeight");
			//Vector3 tickerWeightPosition = tickerWeight.transform.localPosition;

            TempoViewActions tempoView = GameObject.Find("TempoView").GetComponent<TempoViewActions>();
            if (holdingUp)
            {
                Debug.Log("Change Tempo Up");
                if (tempoView.getTempo() < 300 && tempoView.getTempo() > 20)
                {
                    tempoView.setTempo(tempoView.getTempo() + 1);
                    //tickerWeight.transform.localPosition = new Vector3 (tickerWeightPosition.x, tickerWeightPosition.y - .00015f, tickerWeightPosition.z);
                }
            }
            else if (holdingDown)
            {
                Debug.Log("Change Tempo Down");
                if (tempoView.getTempo() < 300 && tempoView.getTempo() > 20)
                {
                    tempoView.setTempo(tempoView.getTempo() - 1);
                    //tickerWeight.transform.localPosition = new Vector3 (tickerWeightPosition.x, tickerWeightPosition.y +  .00015f, tickerWeightPosition.z);
                }
            }
        }

        public override void onClick () {
			GameObject tickerWeight = GameObject.Find("TickerWeight");
			//Vector3 tickerWeightPosition = tickerWeight.transform.localPosition;
			TempoViewActions tempoView = GameObject.Find("TempoView").GetComponent<TempoViewActions>();

			// Case for clicking "TempoChangeUp" increments the tempo up by 1
			if (this.gameObject.name == "TempoChangeUp") {
				if (tempoView.getTempo() < 300 && tempoView.getTempo() > 20) {
					tempoView.setTempo (tempoView.getTempo () + 1);
					//tickerWeight.transform.localPosition = new Vector3 (tickerWeightPosition.x, tickerWeightPosition.y - .00015f, tickerWeightPosition.z);
				}
			// Case for clicking "TempoChangeUp" decrements the tempo up by 1
			} else {
				if (tempoView.getTempo () < 300 && tempoView.getTempo () > 20) {
					tempoView.setTempo (tempoView.getTempo () - 1);
					//tickerWeight.transform.localPosition = new Vector3 (tickerWeightPosition.x, tickerWeightPosition.y +  .00015f, tickerWeightPosition.z);
				}
			}
		}
			
	}
}
