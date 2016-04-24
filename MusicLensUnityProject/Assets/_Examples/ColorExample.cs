using UnityEngine;
using System.Collections;
using CWRU.Common.HololensInput;

/// <summary>
/// This is an example which uses the GestureManager
/// Ensure that this is in a scene with a gesture manager, and on an object with a renderer.
/// The ChangeColor method is registered with the gesture manager.
/// As you can see the registered method needs two inputs, the sender and the eventargs.
/// You can always type in something like: 
//        GestureManager.RegisterAirTap(ChangeColor);
//  and have the quick actions for visual studio make the method for you.
/// </summary>
public class ColorExample : MonoBehaviour {

    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        GestureManager.RegisterAirTap(ChangeColor);
	}

    private void ChangeColor(object sender, System.EventArgs e)
    {
        Debug.Log("Color change");
        ChangeColor();
    }

    void ChangeColor()
    {
        rend.material.color = Random.ColorHSV(0, 1);
    }
}
