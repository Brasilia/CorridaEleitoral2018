using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Alignment {

	[System.Serializable]
	public struct EconomicAxis {
		public int value;
		//SubEixos (subtemas)
		public int bolsaFamilia;
		public int salarioMinimo;
		public int impostoDeRenda;
		public int privatizacao;
		public int previdencia;
		//Métodos Get/Set
		public List<int> Get(){
			List<int> values = new List<int> ();
			values.Add (value);
			values.Add (bolsaFamilia);
			values.Add (salarioMinimo);
			values.Add (impostoDeRenda);
			values.Add (privatizacao);
			values.Add (previdencia);
			return values;
		}
		public void Set(List<int> values){
			value = values [0];
			bolsaFamilia = values [1];
			salarioMinimo = values [2];
			impostoDeRenda = values [3];
			privatizacao = values [4];
			previdencia = values [5];
		}
	}
	[System.Serializable]
	public struct CivilAxis {
		public int value;
		//SubEixos (subtemas)
		public int servicoMilitarObrigatorio;
		public int escolasMilitares;
		//Métodos Get/Set
		public List<int> Get(){
			List<int> values = new List<int> ();
			values.Add (value);
			values.Add (servicoMilitarObrigatorio);
			values.Add (escolasMilitares);
			return values;
		}
		public void Set(List<int> values){
			value = values [0];
			servicoMilitarObrigatorio = values [1];
			escolasMilitares = values [2];
		}
	}
	[System.Serializable]
	public struct SocietalAxis {
		public int value;
		//SubEixos (subtemas)
		public int ensinoReligiosoEscolas;
		public int legalizacaoAborto;
		public int casamentoGay;
		public int legalizacaoDrogas;
		//Métodos Get/Set
		public List<int> Get(){
			List<int> values = new List<int> ();
			values.Add (value);
			values.Add (ensinoReligiosoEscolas);
			values.Add (legalizacaoAborto);
			values.Add (casamentoGay);
			values.Add (legalizacaoDrogas);
			return values;
		}
		public void Set(List<int> values){
			value = values [0];
			ensinoReligiosoEscolas = values [1];
			legalizacaoAborto = values [2];
			casamentoGay = values [3];
			legalizacaoDrogas = values [4];
		}
	}

	public EconomicAxis economic;
	public CivilAxis civil;
	public SocietalAxis societal;

}
