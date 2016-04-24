using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class BlinkMetronomeActions : MetronomeUtility {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Play();
    }

    /// <summary>
    /// Gets necessary information from MetronomeUtility and uses it to play the metronome appropriately
    /// </summary>
    void Play() {
        Animator myAnimator = this.GetComponent<Animator>();
		int subdiv = getSubdivisions();
        switch (subdiv) {
            case 0:
                myAnimator.Play("BlinkMetronomeDownbeat");
                //Debug.Log("Playing blink downbeat.");
                break;
            case 1:
                myAnimator.Play("BlinkMetronomeEighth");
                break;
            case 2:
                myAnimator.Play("BlinkMetronomeTriplet");
                break;
            case 3:
                myAnimator.Play("BlinkMetronomeSixteenth");
                break;
        }

        myAnimator.speed = getMetSpeed();
        myAnimator.enabled = getIsPlaying();
    }

    /// <summary>
    /// Takes the BPM and turns it into the speed of the animation
    /// </summary>
    /// <returns></returns>
    float getMetSpeed() {
        float baseBPM;
		if (getSubdivisions() < 3) {
            baseBPM = 120;
        } else {
            baseBPM = 90;
        }

        float currBPM = (float)getBPM();
        //Debug.Log(currBPM);
        return currBPM / baseBPM;
    }

}
