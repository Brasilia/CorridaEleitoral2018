using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GraphControl : MonoBehaviour {
	public GameObject graph;		 //referencia para o grafico
	public LineRenderer baseX;		 //eixo x
	public LineRenderer baseY;		 //eixo y
	public GameObject lines;		 //referencia p/ prefab Lines

    public List<Text> dates;
    public List<Text> scales;


    //public GameManager gm;

    private float panelWidth;
    private float panelHeight;


    // Use this for initialization
    void Start () {

        //gm = GameManager.instance;

        //baseX = baseX.gameObject.GetComponent<LineRenderer> ();

        RectTransform panel = (RectTransform) graph.transform;
        panelWidth = panel.rect.width;
        panelHeight = panel.rect.height;

        //ajusta posição das datas no grafico
        float scale = 0f;
        foreach (Text date in dates)
        {
            //tamanho texto data
            RectTransform dateSize = (RectTransform)date.transform;
            date.rectTransform.anchoredPosition = new Vector2( ( (0.25f + scale) * panelWidth) + (dateSize.rect.height/2) , -dateSize.rect.width);
            scale += 0.25f;
        }

        //ajusta posicao das escalas no grafico
        scale = 0;
        foreach(Text s in scales)
        {
            RectTransform scaleSize = (RectTransform)s.transform;
            s.rectTransform.anchoredPosition = new Vector2( -(scaleSize.rect.width), panelHeight * (0.25f + scale) - (scaleSize.rect.height / 2) );
            scale += 0.25f;
        }

        //cria eixo X
        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3 (-5f, 0f, -1f);
		positions [1] = new Vector3 (panelWidth + 25f, 0.0f, -1f);
		baseX.positionCount = positions.Length;
        baseX.startColor = Color.black;     //ajusta cor
        baseX.endColor = Color.black;
        baseX.SetPositions(positions);   //cria linha
        //cria eixo Y
        positions[0] = new Vector3 (0f, 0f, -1f);
		positions [1] = new Vector3 (0f, panelHeight + 25f, 0f);
		baseY.positionCount = positions.Length;
        baseY.startColor = Color.black;
        baseY.endColor = Color.black;
		baseY.SetPositions (positions);


        List<float> votIntentions = new List<float>();
        votIntentions.Add(0.25f);
        votIntentions.Add(0.01f);
        votIntentions.Add(0.75f);
        votIntentions.Add(1f);

        Vector3[] positionGraph = new Vector3[votIntentions.Count + 1];
        positionGraph[0] = new Vector3(0f, 0f, -1f);
        int i = 1;
        foreach (float f in votIntentions)
        {
            positionGraph[i] = new Vector3( ((((i-1) + 1f)/4f) * panelWidth), (f * panelHeight), -1f);
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

        //foreach (Candidate cand in gm.candidates) {
            //positions [1] = new Vector3 ((0.1f * panelWidth), (cand.voteIntentions * panelHeight), -1f);
        //}
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGraphActive() {
        gameObject.SetActive(true);
    }
}
