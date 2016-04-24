using UnityEngine;

namespace CWRU.Common.Text
{
	public class ExampleHUITextSceneController : MonoBehaviour
	{
		public GameObject HUITextGameObject;

		private HUITextController huiTextController;

		// Use this for initialization
		void Start()
		{
			huiTextController = HUITextGameObject.GetComponent<HUITextController>();
        }

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				huiTextController.SetText("Push Down Arrow to change");
            }
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				huiTextController.SetText("Push Up Arrow to change");
			}
		}
	}
}