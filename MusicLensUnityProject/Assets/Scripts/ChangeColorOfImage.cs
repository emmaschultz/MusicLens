using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

// Class for changing the background color of music and chopping it into multiple images, each one line of music
public class ChangeColorOfImage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Texture2D mainImage = Resources.Load("ExampleMusic", typeof(Texture2D)) as Texture2D;
		testPrintChoppedRuns (mainImage, getYValuesToChopAt (mainImage, xValueOfRightMostBlackPixel (mainImage)));
	}

	//Prints red lines representing where to chop the image up
	void testPrintChoppedRuns(Texture2D image, List<int> yVals) {
		//Debug.Log ("Num y vals" + yVals.Count);
		for (int i = 0; i < yVals.Count; i++) {
			for (int x = 0; x < image.width; x++) {
				image.SetPixel(x, yVals[i], Color.red);
			}
		}

		image.Apply();

		byte[] bytes = image.EncodeToPNG();

		// For testing purposes, also write to a file in the project folder
		//File.WriteAllBytes(Application.dataPath + "/../MUSICIMAGEEXAMPLETHING.png", bytes);
	}

	// Saves the image with different colors (transparent background, white notes)
	void printPixelData(Texture2D image) {
		for (int y = 0; y < image.height; y++) {
			for (int x = 0; x < image.width; x++) {
				if ((int)(image.GetPixel (x, y).b) > 0) {
					image.SetPixel(x, y, Color.blue);
				} else {
					image.SetPixel(x, y, Color.white);
				}
			}
		}

		image.Apply();

		byte[] bytes = image.EncodeToPNG();

		// For testing purposes, also write to a file in the project folder
		//File.WriteAllBytes(Application.dataPath + "/../MUSICIMAGEEXAMPLETHING.png", bytes);
	}

	// Returns the x value of the furthest black pixel to the right of the image - provides the ending of musical lines
	int xValueOfRightMostBlackPixel(Texture2D image) {
		for (int i = image.width; i > 0; i--) {
			if (image.GetPixel (i, image.height - 60).b == 0) {
				return i;
			}
		}
		return -1;
	}

	// Returns a list of the y indecies in which the music needs to be chopped
	List<int> getYValuesToChopAt(Texture2D image, int x) {
		// A list of y values representing the runs of white in the x direction
		List<int> whiteRuns = getRangeOfWhiteRuns (image, x);
		List<int> valuesToChopAt = new List<int> ();

		// i to i+1 represents a run of white from top to bottom of the image.
		// This loop will add all of the middle values between these white runs to the valuesToChop array
		for (int i = 0; i < whiteRuns.Count - 1; i += 2) {
			valuesToChopAt.Add (((whiteRuns [i + 1] - whiteRuns [i]) / 2) + whiteRuns[i]);
		}

		return valuesToChopAt;
	}

	// Gets a list numbers where i to i+1 of the list represents a run of white pixels in the y direction.
	List<int> getRangeOfWhiteRuns(Texture2D image, int x) {
		List<int> whiteGaps = new List<int> ();
		whiteGaps.Add (0);
		int currentColorVal = 1;
		for (int i = 0; i < image.height; i++) {
			if (image.GetPixel (x, i).b != currentColorVal) {
				whiteGaps.Add (i);
				currentColorVal = (currentColorVal == 1) ? 0 : 1;
			}
		}
		return whiteGaps;
	}
}
