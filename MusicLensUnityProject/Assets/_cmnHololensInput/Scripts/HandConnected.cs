using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace CWRU.Common.HololensInput
{
    public class HandConnected : MonoBehaviour
    {
        Transform initialParent;
        Vector3 initialPosition;
        Quaternion initialRotation = Quaternion.identity;

        // Booleans dictating whether or not to keep various aspects of the object's transfor.
        [Header("Persistance Variables")]
        [Tooltip("When hand is lost, should this object be reparented to its original parent.")]
        public bool keepOldParent;
        [Tooltip("When hand is lost, should this object return to its original position.")]
        public bool keepOldPosition;
        [Tooltip("When hand is lost, should this object return to its original rotation.")]
        public bool keepOldRotation;

        /// <summary>
        /// Used for initialization
        /// </summary>
        void Start()
        {

        }

        /// <summary>
        /// A method called when the hand is lost.
        /// </summary>
        private void HandLost()
        {
            // Reparent this 
            if (keepOldParent)
                transform.parent = initialParent;
            if (keepOldPosition)
                transform.localPosition = initialPosition;
            if (keepOldRotation)
                transform.rotation = initialRotation;
        }

        /// <summary>
        /// A method called every frame a hand is seen.
        /// </summary>
        private void HandUpdated()
        {
            // Get the current position of the hand.
            Vector3 pos = Vector3.zero;

            //POS = HAND POSITION

            // Place this object at the hand's position.
            transform.position = pos;
            if (keepOldRotation)
                transform.localRotation = initialRotation;
        }

        /// <summary>
        /// A method called when a hand is first seen.
        /// </summary>
        private void HandDetected()
        {
            // Track what this was parented to before and its local position relative to that parent.
            initialPosition = transform.localPosition;
            initialParent = transform.parent;
            initialRotation = transform.rotation;

            // Then unparent it.
            transform.parent = Camera.main.transform;
        }
    }
}
