using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Card", menuName = "Card/New", order = 2)]
[System.Serializable]
public class Card_Data : ScriptableObject {
    [TextArea]
	public string description;
	public Sprite image;
}
