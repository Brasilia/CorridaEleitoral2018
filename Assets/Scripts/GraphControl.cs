using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GraphControl : MonoBehaviour {
	public GameObject graph;		 //referencia para o grafico
	public LineRenderer baseX;		 //eixo x
	public LineRenderer baseY;		 //eixo y
	public GameObject lines;		 //referencia p/ prefab Lines

    public List<Text> dates;
    public List<Text> scales;

	public List<Color> lineColors;

	public ChartLabels chartLabels;


	private GameManager gm;

    private float panelWidth;
    private float panelHeight;


	void Awake () {
		gm = GameManager.instance;

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
			date.rectTransform.anchoredPosition = new Vector2( ( (0.25f + scale) * panelWidth) + (dateSize.rect.height/2) , -dateSize.rect.width - 20);
			scale += 0.25f;
		}

		//ajusta posicao das escalas no grafico
		scale = 0;
		foreach(Text s in scales)
		{
			RectTransform scaleSize = (RectTransform)s.transform;
			s.rectTransform.anchoredPosition = new Vector2( -(scaleSize.rect.width) - 10, panelHeight * (0.25f + scale) - (scaleSize.rect.height / 2) );
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



		//        foreach (Candidate cand in gm.candidates) {
		//            positions [1] = new Vector3 ((0.1f * panelWidth), (cand.voteIntentions * panelHeight), -1f);
		//        }
	}

    // Use this for initialization
    void Start () {
		for (int i = 0; i < gm.candidates.Count; i++){
			chartLabels.AddCandidate (gm.candidates[i], lineColors[i]);
		}
    }

	void OnEnable() {
		// Desenha linhas de cada candidato
		int color = 0;
		foreach (Candidate cand in gm.candidates) {
			List<float> voteIntentions = cand.voteIntentions;

			Vector3[] positionGraph = new Vector3[voteIntentions.Count];
			int i = 0;
			foreach (float f in voteIntentions)
			{
				positionGraph[i] = new Vector3( ((((i-1) + 1f)/4f) * panelWidth), (f/100 * panelHeight), -1f);
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
			line.startColor = lineColors [color];
			line.endColor = lineColors [color];
			color++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGraphActive() {
        gameObject.SetActive(true);
		transform.parent.gameObject.SetActive (true);
    }
}
