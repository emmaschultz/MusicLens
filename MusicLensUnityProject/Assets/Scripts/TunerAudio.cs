using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class TunerAudio : MonoBehaviour {
    void Awake()
    {
        audio.clip = Microphone.Start(null, true, 3, AudioSettings.outputSampleRate);
        audio.Play(); // Play the audio source!
        audio.loop = true;

        samples = new float[sampleSize];
        spectrum = new float[sampleSize];
        fSample = AudioSettings.outputSampleRate;

        InvokeRepeating("AnalyzeSound", 0.0f, 0.5f);
    }

    // Use this for initialization
    void Start()
    { 
        buildNotesList();
    }

    void AnalyzeSound()
    {
        FFT();
        if(pitchValue != 0)
        {
            int i = binarySearch(pitchValue);
            Note n = notes[i];
            float c = freqToCents(n.freq, pitchValue);
            TunerScript tuner = GameObject.FindObjectOfType<TunerScript>();
            if(tuner != null)
            {
                tuner.currentCents = Convert.ToInt32(c);
                tuner.currentNote = n.note;
            }

            Debug.Log("Pitch: " + pitchValue + "Hz Note: " + n.note + " Cents: " + c);
        }
    }

    #region FFT
    float qSamples = 1024.00f;  // array size
    int sampleSize = 1024;
    float refValue = 0.1f; // RMS value for 0 dB
    float threshold = 0.01f;      // minimum amplitude to extract pitch .02
    float rmsValue;  // sound level - RMS
    float dbValue;    // sound level - dB
    float pitchValue; // sound pitch - Hz

    public AudioSource audio;

    private float[] samples; // audio samples
    private float[] spectrum; // audio spectrum
    private float fSample;

    void FFT()
    {
        audio.GetOutputData(samples, 0); // fill array with samples
        int i;
        float sum = 0;
        for (i = 0; i < qSamples; i++)
        {
            sum += samples[i] * samples[i]; // sum squared samples
        }
        rmsValue = Mathf.Sqrt(sum / qSamples); // rms = square root of average
        dbValue = 20 * Mathf.Log10(rmsValue / refValue); // calculate dB
        if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
                                            // get sound spectrum
        audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        int maxN = 0;
        for (i = 0; i <
            qSamples; i++)
        { // find max 
            if (spectrum[i] > maxV && spectrum[i] > threshold)
            {
                maxV = spectrum[i];
                maxN = i; // maxN is the index of max
            }
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < qSamples - 1)
        { // interpolate index using neighbours
            var dL = spectrum[maxN - 1] / spectrum[maxN];
            var dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        pitchValue = freqN * (fSample / 2) / qSamples; // convert index to frequency
    }
    #endregion

    #region Note Constants
    struct Note
    {
        public string note;
        public float freq;
        public Note(string note, float freq)
        {
            this.note = note;
            this.freq = freq;
        }
    }

    List<Note> notes = new List<Note>();
    public TextAsset notesTextFile;

    void buildNotesList()
    {
        string theWholeFileAsOneLongString = notesTextFile.text;
        foreach(string s in theWholeFileAsOneLongString.Split("\n"[0]))
        {
            var splits = s.Split(',');
            notes.Add(new Note(splits[0], System.Convert.ToSingle(splits[1])));
        }
    }
    #endregion

    #region Find Note
    float freqToCents(float f1, float f2)
    {
        float cents = 1200 * System.Convert.ToSingle(System.Math.Log(f2 / f1, 2));
        return cents;
    }

    int binarySearch(float freq)
    {
        int index = 0;
        Note[] arr = notes.ToArray();

        index = Math.Max(binarySearchRecur(arr, freq, 0, arr.Length - 1), index);

        return index;
    }

    int binarySearchRecur(Note[] arr, float freq, int min, int max)
    {

        try
        {
            if (min > max)
            {
                return -1;
            }
            else
            {
                int mid = (min + max) / 2;
                if (inBetweenLower(arr, freq, mid))
                {
                    return findCloserNote(arr, freq, mid - 1, mid);
                }
                else if(inBetweenUpper(arr, freq, mid))
                {
                    return findCloserNote(arr, freq, mid, mid + 1);
                }
                else if (freq < arr[mid].freq)
                {
                    return binarySearchRecur(arr, freq, min, mid - 1);
                }
                else
                {
                    return binarySearchRecur(arr, freq, mid + 1, max);
                }
            }
        }
        catch (IndexOutOfRangeException)
        {
            return 0;
        }
    }

    bool inBetweenLower(Note[] arr, float freq, int index)
    {
        return (freq < arr[index].freq && freq > arr[index - 1].freq);
    }

    bool inBetweenUpper(Note[] arr, float freq, int index)
    {
        return (freq > arr[index].freq && freq < arr[index + 1].freq);
    }

    int findCloserNote(Note[] arr, float freq, int lowerIndex, int upperIndex)
    {
        var diff1 = arr[upperIndex].freq - freq;
        var diff2 = freq - arr[lowerIndex].freq;
        if (diff1 > diff2)
        {
            return lowerIndex;
        }
        else
        {
            return upperIndex;
        }
    }
    #endregion
}
