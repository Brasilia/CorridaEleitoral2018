using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GraphControl : MonoBehaviour {
	public GameObject graph;		 //referencia para o grafico
	public LineRenderer baseX;		 //eixo x
	public LineRenderer baseY;		 //eixo y
	public GameObject lines;		 //referencia p/ prefab para emptyObject com LineRender

    public GameManager gm;

    private float panelWidth;
    private float panelHeight;


    // Use this for initialization
    void Start () {

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        //baseX = baseX.gameObject.GetComponent<LineRenderer> ();
        Vector3[] positions = new Vector3[2];

        RectTransform panel = (RectTransform) graph.transform;
        panelWidth = panel.rect.width;
        panelHeight = panel.rect.height;

        positions [0] = new Vector3 (0f, 0f, -1f);
		positions [1] = new Vector3 (panelWidth, 0.0f, -1f);
		baseX.positionCount = positions.Length;
        baseX.SetPositions(positions);
        //Material m = baseX.GetComponent<Renderer> ().material;
        //baseX.material = new Material(Shader.Find("Particles/Additive")).SetColors(Color.white, Color.black);

        positions[0] = new Vector3 (0f, 0f, -1f);
		positions [1] = new Vector3 (0f, panelHeight, 0f);
		baseY.positionCount = positions.Length;
		baseY.SetPositions (positions);


        List<float> votIntentions = new List<float>();
        votIntentions.Add(0.5f);
        votIntentions.Add(0.3f);
        votIntentions.Add(0.7f);

        Vector3[] positionGraph = new Vector3[4];
        positionGraph[0] = new Vector3(0f, 0f, -1f);
        int i = 1;
        foreach (float f in votIntentions)
        {
            print("AQUI: " + 0.1f * panelWidth + " " + f * panelHeight);
            positionGraph[i] = new Vector3((0.1f * panelWidth * (i+1)), (f * panelHeight), -1f);
            i++;
        }

        GameObject inst = Instantiate(lines);
        //add instancia como filho do grafico
        inst.transform.parent = graph.transform;
        inst.transform.localPosition = new Vector3(0f, 0f, 0f);
        inst.transform.localScale = new Vector3(1f, 1f, 1f);

        LineRenderer line = inst.GetComponent<LineRenderer>();
        line.positionCount = positionGraph.Length;
        line.SetPositions(positionGraph);

        foreach (Candidate cand in gm.candidates) {
            //positions [1] = new Vector3 ((0.1f * panelWidth), (cand.voteIntentions * panelHeight), -1f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGraphActive() {
        gameObject.SetActive(true);
    }
}
