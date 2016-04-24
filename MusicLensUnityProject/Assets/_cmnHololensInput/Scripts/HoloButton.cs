using UnityEngine;
using System.Collections;

namespace CWRU.Common.HololensInput
{
    [RequireComponent(typeof(Collider))]
    /// <summary>
    /// The base class for any button which you want to work with GazeManager.
    /// </summary>
    public abstract class HoloButton : MonoBehaviour
    {

        /// <summary>
        /// A method called when you first look at the button.
        /// </summary>
        public abstract void onHover();

        /// <summary>
        /// A method called when you stop looking at the button.
        /// </summary>
        public abstract void onEndHover();

        /// <summary>
        /// A method called when you click on the button.
        /// </summary>
        public abstract void onClick();

    }
}
