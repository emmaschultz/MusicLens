using UnityEngine;
using System.Collections;
using System;
using CWRU.Common.Text;
using UnityEngine.UI;

/*
    Use the getter setter methods of this class to set a value for String currentNote and int currentCents
    Every time update is called it pulls the values from these to fields and sets the tuner accordingly
 */

public class TunerScript : MonoBehaviour
{
    private GameObject[] scaleList = new GameObject[11];
    private GameObject tunerArm;
    private GameObject noteBox;
    //private HUITextController noteTextController;
    private TextMesh noteText;

    private float _labelSize;
    public float LabelSize
    {
        get { return _labelSize; }
        set
        {
            _labelSize = value;
            setUpTuner();
        }
    }
    private float _noteSize;
    public float NoteSize
    {
        get { return _noteSize; }
        set
        {
            _noteSize = value;
            setUpTuner();
        }
    }
    public float NoteBoxSize { get; set; }
    public String currentNote { get; set; }
    public int currentCents { get; set; }

    public float testTime, testValue;

    // Use this for initialization
    void Start()
    {
        tunerArm = GameObject.FindWithTag("TunerArm");
        noteBox = GameObject.FindWithTag("noteBox");
        //noteTextController = noteBox.GetComponent<HUITextController>();
        noteText = noteBox.GetComponent<TextMesh>();

        LabelSize = .2f;
        NoteSize = .1f;
        NoteBoxSize = .3f;

        setUpTuner();
    }

    #region SetUp
    private void setUpTuner()
    {
        //Transform forkTransform = GameObject.FindGameObjectWithTag("TuningFork").transform;
        //this.gameObject.transform.localPosition = new Vector3(forkTransform.position.x, forkTransform.position.y, forkTransform.position.z);

        #region GettingLabels
        scaleList[0] = GameObject.FindWithTag("0");

        #region Negative Numbers
        for (int i = 1; i < 6; i++)
        {
            scaleList[i] = GameObject.FindWithTag("m" + i * 10);
        }
        #endregion
        #region Positive Numbers
        for (int i = 1; i < 6; i++)
        {
            scaleList[i + 5] = GameObject.FindWithTag("p" + i * 10);
        }
        #endregion
        #endregion

        #region SetPosition
        scaleList[0].transform.localPosition = setLabelPosition(0);

        for (int i = 1; i < 6; i++)
        {
            scaleList[i].transform.localPosition = setLabelPosition(i);
        }
        for (int i = 6; i < 11; i++)
        {
            scaleList[i].transform.localPosition = setLabelPosition(i + 1);
        }
        #endregion

        #region SetLabelSize
        for (int i = 0; i < scaleList.Length; i++)
        {
            scaleList[i].transform.localScale = new Vector3(LabelSize, LabelSize, LabelSize);
        }
        #endregion

        #region TunerArmSet
        float newArmLength = noteBox.transform.localScale.y;
        tunerArm.transform.localScale = new Vector3(newArmLength / 4, newArmLength, newArmLength);
        tunerArm.transform.localPosition = new Vector3(0f, 0f, 0f);
        #endregion

        #region Notebox and Text
        noteBox.transform.localPosition = new Vector3(0f, 0f, 0f);
        noteBox.transform.localScale = new Vector3(NoteBoxSize, NoteBoxSize, NoteBoxSize);

        noteText.transform.localPosition = new Vector3(0, 0, 0);
        noteText.transform.localScale = new Vector3(NoteSize, NoteSize, NoteSize);
        noteText.anchor = TextAnchor.MiddleCenter;
        #endregion
    }

    private void setLableDimensions(int i)
    {
        Transform firstDigit = scaleList[i].transform.GetChild(0).transform;
        Transform secondDigit = scaleList[i].transform.GetChild(0).transform;
        if (i != 0)
        {
            secondDigit = scaleList[i].transform.GetChild(1).transform;
        }

        firstDigit.localPosition = new Vector3(-1, firstDigit.position.y, firstDigit.position.z);
        if (i != 0)
        {
            secondDigit.localPosition = new Vector3(0, secondDigit.position.y, secondDigit.position.z);
        }
    }

    private Vector3 setLabelPosition(int i)
    {
        float armLength = tunerArm.transform.GetChild(0).GetChild(0).transform.localScale.y;

        int offset = 90;
        float angle = 180 / 10;
        float upperBound = 6;

        float xValue = 0f;
        float zValue = 0f;
        if (i < 6)
        {
            xValue = -armLength * Mathf.Cos(Mathf.Deg2Rad * (offset - i % upperBound * angle));
            zValue = scaleList[i].transform.position.z;
        }
        else
        {
            xValue = -armLength * Mathf.Cos(Mathf.Deg2Rad * (offset + i % upperBound * angle));
            zValue = scaleList[i - 1].transform.position.z;

        }

        float yValue = armLength * Mathf.Sin(Mathf.Deg2Rad * (offset - i % upperBound * angle));
        return new Vector3(xValue, yValue, 0);
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        changeArmRotation(-currentCents);
        setCurrentNote(currentNote);
        //testTuner();
    }

    #region TunerActioons
    private void changeArmRotation(float input)
    {
        Quaternion start = tunerArm.transform.localRotation;
        Quaternion end;

        float rightAngle = 90;
        //float zValue = rightAngle * input / 50;
        float zValue = rightAngle * input / 50;
        float armSpeed = .1F;

        end = Quaternion.Euler(0, 0, zValue);
        tunerArm.transform.rotation = Quaternion.Slerp(start, end, armSpeed);
    }

    private void setCurrentNote(String note)
    {
        //noteTextController.SetText(note);
        noteText.text = note;
        noteText.fontSize = (int)NoteSize;

        //noteTextController.transform.localScale = new Vector3(NoteSize, NoteSize, NoteSize);
    }
    #endregion

    private void testTuner()
    {
        float amount = Mathf.PingPong(Time.time * 10, 800);
        testTime = amount;
        testValue = amount % 100;
        if (testValue < 50)
        {
            amount *= -1;
            amount = amount % 50;
        }

        String note;
        #region NoteChoice
        if (amount < 100)
        {
            note = "A";
        }
        else if (amount < 200)
        {
            note = "B";
        }
        else if (amount < 300)
        {
            note = "C";
        }
        else if (amount < 400)
        {
            note = "D";
        }
        else if (amount < 500)
        {
            note = "E";
        }
        else if (amount < 600)
        {
            note = "F";
        }
        else if (amount < 700)
        {
            note = "G";
        }
        else
        {
            note = "*";
        }
        #endregion
        changeArmRotation(amount);
        setCurrentNote(note);
    }
} 
