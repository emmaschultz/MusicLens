using UnityEngine;
using System.Collections;
using System;

namespace CWRU.Common.HololensInput
{
    /// <summary>
    /// A Behavior to handle the player's gaze.
    /// Will handle HoloButton hover and presses also.
    /// 
    /// This sanitized version is nearly identicle,
    /// See GestureManager about how to simulate taps without a device.
    /// </summary>
    public class GazeManager : MonoBehaviour
    {
        [Tooltip("The object to place at where the player is gazing.")]
        public Transform gazeMarker;
        public float range;

        Camera cam;
        HoloButton button;
        Vector3 currentPoint;

        // Use this for initialization
        void Start()
        {
            cam = Camera.main;
            
            GestureManager.RegisterAirTap(OnAirTap);
        }
        
        private void OnAirTap(object sender, EventArgs e)
        {
            ButtonPress();
        }

        public void ButtonPress()
        {
            if (button)
                button.onClick();
        }

        // Update is called once per frame
        void Update()
        {
            CameraMovement();

            CursorMovement();

        }

        public float moveSpeed = 5f;
        public float lookSpeed = 10f;

        void CameraMovement()
        {
            Camera cam = Camera.main;
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            move /= Mathf.Max(move.magnitude, 1);

            Vector3 forwardDir = cam.transform.forward; forwardDir.y = 0;
            cam.transform.position += forwardDir * move.y * moveSpeed * Time.deltaTime;

            Vector3 rightDir = cam.transform.right; rightDir.y = 0;
            cam.transform.position += rightDir * move.x * moveSpeed * Time.deltaTime;

            float yrotation = cam.transform.rotation.eulerAngles.y;
            yrotation += Input.GetAxis("Mouse X") * Time.deltaTime * lookSpeed;
            float xrotation = cam.transform.rotation.eulerAngles.x;
            xrotation += -Input.GetAxis("Mouse Y") * Time.deltaTime * lookSpeed;

            //xrotation = Mathf.Clamp(xrotation,-90f, 90f);

            cam.transform.rotation = Quaternion.Euler(xrotation, yrotation, 0);
        }

        void CursorMovement()
        {

            //Raycast to the first collider I hit...
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                //...And transmit the point at which I hit
                currentPoint = hit.point;

                // Handle HoloButtons.
                HoloButton b = hit.collider.gameObject.GetComponent<HoloButton>();

                // If we aren't looking at a button...
                if (b == null)
                {
                    // ...Unfocus our current button.
                    if (button)
                    {
                        button.onEndHover();
                        button = null;
                    }
                }
                else
                {
                    if (button)
                    {
                        button.onEndHover();
                    }
                    button = b;
                    b.onHover();
                }
            }

            // Otherwise...
            else
            {
                // ...Place the point at the camera's position.
                currentPoint = cam.transform.position + cam.transform.forward * range;

                // Obviously there is no button being looked at...
                if (button)
                {
                    // ...so end hover on any current button.
                    button.onEndHover();
                    button = null;
                }
            }

            // Move the gaze marker to the desired point.
            if (gazeMarker)
            {
                gazeMarker.transform.position = currentPoint;
            }
        }
    }
}
