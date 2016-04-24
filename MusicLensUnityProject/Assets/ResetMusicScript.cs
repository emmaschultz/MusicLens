using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CWRU.Common.HololensInput
{
    public class ResetMusicScript : UIUtility
    {

        // Use this for initialization
        void Start()
        {
            Debug.Log("Initislized reset music script");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void onClick()
        {
            Debug.Log("RESET THE MUSIC");
            GameObject.FindObjectOfType<SheetMusicScript>().resetSheetMusic();
        }

        public override void onHover()
        {
            //base.onHover();
        }

        public override void onEndHover()
        {
            //base.onEndHover();
        }
    }
}
