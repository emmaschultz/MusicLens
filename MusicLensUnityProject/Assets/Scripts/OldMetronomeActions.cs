using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class OldMetronomeActions : MetronomeUtility {

    // Use this for initialization
    void Start () {
        //Play();
    }
	
	// Update is called once per frame
	void Update () {
        Play();
	}

    /// <summary>
    /// Gets necessary information from MetronomeUtility and uses it to play the metronome appropriately
    /// </summary>
    void Play()
    {
        Animator myAnimator = this.GetComponent<Animator>();
		int subdiv = getSubdivisions();
		//Debug.Log ("In Play subdivision is " + subdiv);
		switch (subdiv)
        {
            case 0:
                myAnimator.Play("ClassicMetronomeDownbeat");
                break;
			case 1:
                myAnimator.Play("ClassicMetronomeEighth");
                break;
            case 2:
                myAnimator.Play("ClassicMetronomeTriplet");
                break;
            case 3:
                myAnimator.Play("ClassicMetronomeSixteenth");
                break;
        }

        myAnimator.speed = getMetSpeed();
        myAnimator.enabled = getIsPlaying();
    }

    /// <summary>
    /// Takes the BPM and turns it into the speed of the animation
    /// </summary>
    /// <returns></returns>
    float getMetSpeed()
    {
        float baseBPM;
		if (getSubdivisions() < 3){
            baseBPM = 120;
        }
        else
        {
            baseBPM = 90;
        }

		float currBPM = (float)getBPM();

        return currBPM / baseBPM;
    }
		
}
