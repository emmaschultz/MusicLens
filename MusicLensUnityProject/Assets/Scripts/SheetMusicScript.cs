using UnityEngine;
using System.Collections;

public class SheetMusicScript : MonoBehaviour {
    // TODO: change these two variables to private when done with testing!
    public bool isPlaying;
    private bool isReset;

    private bool doNotResetPanels;
    
    public int BPM;
    private float avgMeasuresPerLine;
    private int beatsPerMeasure;
    private float unitsPerLine;
    private float speed;

    private Object[] objects;
    private Texture[] musicImages;
    private int musicImageCount;

    private GameObject[] musicPanels;
    private GameObject musicPanel1;
    private GameObject musicPanel2;

    private bool firstLerp;
    private Vector3 panelStartPoint;
    private Vector3 panelEndPoint;
    private float startTimeMusicPanel1;
    private float startTimeMusicPanel2;
    private float journeyLength;

    private Vector3 panelStartPointFirstLerp;
    private float journeyLengthFirstLerp;

    private float pauseTime;
    private float durationPaused1;
    private float durationPaused2;

    // Use this for initialization
    void Start() {
        pauseTime = 0;
        durationPaused1 = 0;
        durationPaused2 = 0;

        doNotResetPanels = false;

        // TODO: uncomment these once you are done testing so they are initialized to these variables
        isPlaying = false;
        pauseSheetMusic();
        isReset = false;
        //speed = 1.5f;
        BPM = 85;
        avgMeasuresPerLine = (37 / 7);
        beatsPerMeasure = 4;
        unitsPerLine = 20;
        speed = calculateSpeed();
        Debug.Log("Speed initialized to " + speed + " units/sec");

        firstLerp = true;
        panelStartPoint = new Vector3(20, 0, 10);
        panelEndPoint = new Vector3(-20, 0, 10);
        startTimeMusicPanel1 = Time.time;
        startTimeMusicPanel2 = Time.time;
        journeyLength = Vector3.Distance(panelStartPoint, panelEndPoint);

        panelStartPointFirstLerp = new Vector3(40, 0, 10);
        journeyLengthFirstLerp = Vector3.Distance(panelStartPointFirstLerp, panelEndPoint);

        initializeMusicPanels();

        initializeMusicImages();
    }

    // Update is called once per frame
    void Update() {
        // check to make sure reset hasn't been triggered
        if (isPlaying && !isReset) {
            // move musicPanel1
            float distanceCovered1 = (Time.time - durationPaused1 - startTimeMusicPanel1) * speed;
            float fracJourney1 = distanceCovered1 / journeyLength;
            //Debug.Log("fracJourney1: " + fracJourney1);
            musicPanel1.transform.localPosition = Vector3.Lerp(panelStartPoint, panelEndPoint, fracJourney1);

            // check is musicPanel1 needs reset
            if (fracJourney1 >= 1) {
                resetPanel(musicPanel1, 1);
            }

            // if you are lerp-ing for the first time, the 2nd music panel moves further the first time
            // need to account for this by having different start and end points in the Lerp() fxn the first time
            float fracJourneyFirstLerp = -1;
            float fracJourney2 = -1;
            if (firstLerp) {
                float distanceCoveredFirstLerp = (Time.time - durationPaused2 - startTimeMusicPanel2) * speed;
                fracJourneyFirstLerp = distanceCoveredFirstLerp / journeyLengthFirstLerp;
                //Debug.Log("fracJourneyFirstLerp: " + fracJourneyFirstLerp);
                musicPanel2.transform.localPosition = Vector3.Lerp(panelStartPointFirstLerp, panelEndPoint, fracJourneyFirstLerp);
            } else {
                float distanceCovered2 = (Time.time - durationPaused2 - startTimeMusicPanel2) * speed;
                fracJourney2 = distanceCovered2 / journeyLength;
                //Debug.Log("fracJourney2: " + fracJourney2);
                musicPanel2.transform.localPosition = Vector3.Lerp(panelStartPoint, panelEndPoint, fracJourney2);
            }

            // check if musicPanel2 needs reset
            if (fracJourneyFirstLerp >= 1) {
                firstLerp = false;
                resetPanel(musicPanel2, 2);
            }

            if (fracJourney2 >= 1) {
                resetPanel(musicPanel2, 2);
            }
        }
    }

    // use this function to play the sheet music after pausing it
    public void playSheetMusic() {
        durationPaused1 += (Time.time - pauseTime);
        durationPaused2 += (Time.time - pauseTime);
        pauseTime = 0;
        isPlaying = true;
    }

    // use this function to pause the sheet music
    public void pauseSheetMusic() {
        pauseTime = Time.time;
        isPlaying = false;
    }

    // use this function to reset the sheet music to the beginning
    // this function will also pause the music, so it will be necessary to use the playSheetMusic() function after this
    public void resetSheetMusic() {
        isReset = true;

        doNotResetPanels = false;

        durationPaused1 = 0;
        durationPaused2 = 0;

        // reset starting position of the panels
        musicPanel1.transform.localPosition = panelStartPoint;
        musicPanel2.transform.localPosition = panelStartPointFirstLerp;

        // need to re-initialize these variables
        //speed = 1f;
        firstLerp = true;
        //startTimeMusicPanel1 = Time.time;
        //startTimeMusicPanel2 = Time.time;

        // put the first two sheet music images on to the panels again
        musicPanel1.GetComponent<Renderer>().material.mainTexture = musicImages[0];
        musicPanel2.GetComponent<Renderer>().material.mainTexture = musicImages[1];
        musicImageCount = 2;

        isPlaying = false;
        isReset = false;
    }

    private float calculateSecondsPerBeat() {
        float secondsPerBeat = 60 * (1 / BPM);
        return secondsPerBeat;
    }

    private void resetPanel(GameObject panel, int panelNum) {
        if (!doNotResetPanels) {
            Debug.Log("Reseting panel #" + panelNum);
            panel.transform.localPosition = panelStartPoint;

            // reset the start time for whichever panel is being reset
            if (panelNum == 1) {
                startTimeMusicPanel1 = Time.time;
                durationPaused1 = 0;
            } else if (panelNum == 2) {
                startTimeMusicPanel2 = Time.time;
                durationPaused2 = 0;
            } else {
                Debug.Log("You entered in a panel number that was not a 1 or 2");
            }

            if(musicImageCount == musicImages.Length - 1) {
                doNotResetPanels = true;
            }

            // check to make sure there are more images to place on the panel
            if (musicImageCount < musicImages.Length) {
                // apply musicImages[musicImageCount] to panel
                panel.GetComponent<Renderer>().material.mainTexture = musicImages[musicImageCount];
                musicImageCount++;
            } else {
                //we have reached the end of our music, so stop playing
                isPlaying = false;
            }
        }
        

    }

    private float calculateSpeed() {
        return (float)(BPM * (1.0f / beatsPerMeasure) * (1.0f / 60.0f) * (1.0f / avgMeasuresPerLine) * unitsPerLine);
    }

    private void initializeMusicPanels() {
        // initialize the variables used to reference the music panels
        musicPanels = new GameObject[2];
        musicPanels = GameObject.FindGameObjectsWithTag("MusicPanel");
        if (musicPanels.Length == 0) {
            Debug.Log("Could not find music panel tags.");
        }
        musicPanel1 = musicPanels[1];
        musicPanel2 = musicPanels[0];
    }

    private void initializeMusicImages() {
        // load images into musicImages[]
        objects = Resources.LoadAll("Images", typeof(Texture));
        if (objects != null) {
            musicImages = new Texture[objects.Length];
            for (int i = 0; i < objects.Length; i++) {
                musicImages[i] = (Texture)objects[i];
            }
            Debug.Log("musicImages.Length: " + musicImages.Length);
        }

        // check to make sure there is more than one image in the array
        if (musicImages.Length < 1) {
            Debug.Log("Not enough sheet music images.");
            Debug.Break();
        }

        // put the first two images onto the panels
        musicPanel1.GetComponent<Renderer>().material.mainTexture = musicImages[0];
        musicPanel2.GetComponent<Renderer>().material.mainTexture = musicImages[1];

        musicImageCount = 2; // is 2 because we have placed the first two images onto the panels already
    }
}
