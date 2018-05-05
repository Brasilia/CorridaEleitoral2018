using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GraphControl : MonoBehaviour {
	public GameObject graph;		 //referencia para o grafico
	public LineRenderer baseX;		 //eixo x
	public LineRenderer baseY;		 //eixo y
	public List<Player> candidates;	 //lista dos candidatos
	public GameObject lines;		 //referencia p/ prefab para emptyObject com LineRender


	// Use this for initialization
	void Start () {
		//baseX = baseX.gameObject.GetComponent<LineRenderer> ();
		Vector3[] positions = new Vector3[2];

		positions [0] = new Vector3 (0f, 0f, 0f);
		positions [1] = new Vector3 (20.0f, 0.0f, 0.0f);
		baseX.positionCount = positions.Length;
		Material m = baseX.GetComponent<Renderer> ().material;

		baseX.SetPositions (positions);

		positions [0] = new Vector3 (0f, 0f, 0f);
		positions [1] = new Vector3 (0f, 5f, 0f);
		baseY.positionCount = positions.Length;
		baseY.SetPositions (positions);

		foreach (Player cand in candidates) {
			GameObject inst = Instantiate (lines);
			inst.transform.parent = graph.transform;
			inst.transform.localPosition = new Vector3 (0f, 0f, 0f);
			LineRenderer line = inst.GetComponent<LineRenderer> ();
			positions [0] = new Vector3 (0f, 0f, 0f);
			positions [1] = new Vector3 (10f, 5f, 0f);
			line.positionCount = positions.Length;
			line.useWorldSpace = false;
			line.SetPositions (positions);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
