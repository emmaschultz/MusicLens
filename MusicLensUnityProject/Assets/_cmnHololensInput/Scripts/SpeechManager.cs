using UnityEngine;
using System.Collections.Generic;
using System;

namespace CWRU.Common.HololensInput
{
    /// <summary>
    /// A class to listen for and act on any speech commands.
    /// This sanitized version won't actually work, 
    /// but RegisterKeyword is the only way to call this manager.
    /// </summary>
    public class SpeechManager : MonoBehaviour
    {
        
        public static SpeechManager singleton;

        public class EventInfo : System.EventArgs
        {
            public EventInfo(string word, double confidence)
            {
                this.word = word; this.confidence = confidence;
            }
            public string word;
            public double confidence;
        }

        private bool Enabled = false;
        
        private Dictionary<string, EventHandler<EventInfo>> callbacks;
        private List<string> keys;

        /// <summary>
        /// Initialize everything about this behavior.
        /// </summary>
        void Awake()
        {
            singleton = this;
        }

        /// <summary>
        /// Listens for a new keyword, and sets a new callback for that keyword.
        /// </summary>
        /// <param name="keyword">The new keyword.</param>
        /// <param name="handle">The callback for this keyword.</param>
        public static void RegisterKeyword(string keyword, EventHandler<EventInfo> handle)
        {
            if (singleton)
            {
                // Add the new handle to the callbacks list, keyed to the keyword.
                singleton.callbacks.Add(keyword, handle);
                singleton.keys.Add(keyword);

                // Log that this was registered.
                Debug.Log(keyword + " Registered.");
            }
        }
    }
}
