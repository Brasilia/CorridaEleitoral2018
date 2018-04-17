using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Candidate", menuName = "Card/Candidate", order = 1)]
public class Candidate_Data : Person_Data {

	//Eixos
	public Alignment alignment;

	public List<Staff_Data> avaiableStaff;

}
