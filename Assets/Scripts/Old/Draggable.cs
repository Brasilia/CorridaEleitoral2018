using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldGame
{
	public class Draggable : MonoBehaviour
	{

		// Use this for initialization
		void Start ()
		{
		
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}

		public void OnDrag ()
		{
			float x, y;
			x = Input.mousePosition.x;
			if (x < 0)
				x = 0.0f;
			if (x > 900f)
				x = 900f;
			y = transform.position.y;
			transform.position = new Vector2 (x, y);
			//transform.SetAsLastSibling ();
		}


	}
}
