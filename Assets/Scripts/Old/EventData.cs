using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldGame
{
	[CreateAssetMenu (fileName = "Event", menuName = "Event/New", order = 1)]
	public class EventData : ScriptableObject
	{

		// identificador
		//public int id;

		// textos
		[TextArea (minLines: 3, maxLines: 10)]
		public string contentText;

		// respostas
		public List<EventAnswer> answers;

		//textura
		public Texture2D itemIcon = null;

	}
}