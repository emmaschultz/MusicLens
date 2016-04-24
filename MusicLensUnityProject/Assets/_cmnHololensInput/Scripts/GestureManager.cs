using UnityEngine;
using System.Collections.Generic;
using System;

namespace CWRU.Common.HololensInput
{
    /// <summary>
    /// A manager to pull together all uses of Gesture Recognizer,
    /// this will allow classes to not need their own Gesture Recognizers.
    /// </summary>
    public class GestureManager : MonoBehaviour
    {
        public static GestureManager singleton;

        private List<EventHandler> tapCallbacks;
        private List<EventHandler> holdStartedCallbacks;
        private List<EventHandler> holdEndedCallbacks;
        private List<EventHandler> doubleTapCallbacks;

        // Use this for initialization
        void Awake()
        {
            singleton = this;
            tapCallbacks = new List<EventHandler>();
            doubleTapCallbacks = new List<EventHandler>();
            holdStartedCallbacks = new List<EventHandler>();
            doubleTapCallbacks = new List<EventHandler>();
        }

        #region Register Methods
        /// <summary>
        /// Add a new callback to be called after an air tap.
        /// </summary>
        /// <param name="handle">The callback to be added.</param>
        public static void RegisterAirTap(EventHandler handle)
        {
            if (singleton)
            {
                singleton.tapCallbacks.Add(handle);
                Debug.Log("Tap Event Registered");
            }
        }

        /// <summary>
        /// Add a new callback to be called after a double tap.
        /// </summary>
        /// <param name="handle">The callback to be added.</param>
        public static void RegisterDoubleTap(EventHandler handle)
        {
            if (singleton)
            {
                singleton.doubleTapCallbacks.Add(handle);
                Debug.Log("Double Tap Event Registered");
            }
        }

        /// <summary>
        /// Add a new callback to be called after a hold is started.
        /// </summary>
        /// <param name="handle">The callback to be added.</param>
        public static void RegisterHoldStart(EventHandler handle)
        {
            if (singleton)
            {
                singleton.holdStartedCallbacks.Add(handle);
                Debug.Log("Hold Start Event Registered");
            }
        }

        /// <summary>
        /// Add a new callback to be called after a hold is ended.
        /// </summary>
        /// <param name="handle">The callback to be added.</param>
        public static void RegisterHoldEnd(EventHandler handle)
        {
            if (singleton)
            {
                singleton.holdEndedCallbacks.Add(handle);
                Debug.Log("Hold Ended Event Registered");
            }
        }
        #endregion
        
        #region Non-WSA 
        /// <summary>
        /// The method called following an air tap.
        /// </summary>
        private void OnAirTap()
        {
            foreach (EventHandler eventHandler in tapCallbacks)
            {
                eventHandler(this, null);
            }
        }
        
        /// <summary>
        /// The method called following a hold start.
        /// </summary>
        private void OnHoldStart()
        {
            foreach (EventHandler eventHandler in holdStartedCallbacks)
            {
                eventHandler(this, null);
            }
        }
        
        /// <summary>
        /// The method called following a hold end.
        /// </summary>
        private void OnHoldEnd()
        {
            foreach (EventHandler eventHandler in holdEndedCallbacks)
            {
                eventHandler(this, null);
            }
        }

        /// <summary>
        /// The method called following a double tap.
        /// </summary>
        private void OnDoubleTap()
        {
            foreach (EventHandler eventHandler in doubleTapCallbacks)
            {
                eventHandler(this, null);
            }
        }
        #endregion

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnAirTap();
            }
            if (Input.GetMouseButtonDown(1))
            {
                OnHoldStart();
            }
            if (Input.GetMouseButtonUp(1))
            {
                OnHoldEnd();
            }
            if (Input.GetKey(KeyCode.Return))
            {
                OnDoubleTap();
            }
        }
    }
}