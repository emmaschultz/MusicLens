using UnityEngine;
using System.Collections;

namespace CWRU.Common.HololensInput
{
    public class ExampleButton : HoloButton
    {
        Renderer rend;
        void Start()
        {
            rend = GetComponent<Renderer>();
        }

        private void Pulse()
        {
            targetSize = 1.5f;
        }

        float targetSize = 1;
        void Update()
        {
            transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, targetSize, Time.deltaTime * 5f);
        }
        public override void onClick()
        {
            Debug.Log("Clicked!");
            rend.material.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        }

        public override void onEndHover()
        {
            targetSize = 1f;
        }

        public override void onHover()
        {
            targetSize = 1.2f;
        }
    }
}
