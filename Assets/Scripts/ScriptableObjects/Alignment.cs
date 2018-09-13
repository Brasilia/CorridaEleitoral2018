using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Alignment {

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
		public void Set (EconomicAxis a){
			value = a.value;
			bolsaFamilia = a.bolsaFamilia;
			salarioMinimo = a.salarioMinimo;
			impostoDeRenda = a.impostoDeRenda;
			privatizacao = a.privatizacao;
			previdencia = a.previdencia;
		}
		public static EconomicAxis operator + (EconomicAxis a1, EconomicAxis a2){
			EconomicAxis a;
			a.value = a1.value + a2.value;
			a.bolsaFamilia = a1.bolsaFamilia + a2.bolsaFamilia;
			a.salarioMinimo = a1.salarioMinimo + a2.salarioMinimo;
			a.impostoDeRenda = a1.impostoDeRenda + a2.impostoDeRenda;
			a.privatizacao = a1.privatizacao + a2.privatizacao;
			a.previdencia = a1.previdencia + a2.previdencia;
			return a;
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
		public void Set(CivilAxis a){
			value = a.value;
			servicoMilitarObrigatorio = a.servicoMilitarObrigatorio;
			escolasMilitares = a.escolasMilitares;
		}
		public static CivilAxis operator + (CivilAxis a1, CivilAxis a2){
			CivilAxis a;
			a.value = a1.value + a2.value;
			a.servicoMilitarObrigatorio = a1.servicoMilitarObrigatorio + a2.servicoMilitarObrigatorio;
			a.escolasMilitares = a1.escolasMilitares + a2.escolasMilitares;
			return a;
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
		public void Set(SocietalAxis a){
			value = a.value;
			ensinoReligiosoEscolas = a.ensinoReligiosoEscolas;
			legalizacaoAborto = a.legalizacaoAborto;
			casamentoGay = a.casamentoGay;
			legalizacaoDrogas = a.legalizacaoDrogas;
		}
		public static SocietalAxis operator +(SocietalAxis a1, SocietalAxis a2){
			SocietalAxis a;
			a.value = a1.value + a2.value;
			a.ensinoReligiosoEscolas = a1.ensinoReligiosoEscolas + a2.ensinoReligiosoEscolas;
			a.legalizacaoAborto = a1.legalizacaoAborto + a2.legalizacaoAborto;
			a.casamentoGay = a1.casamentoGay + a2.casamentoGay;
			a.legalizacaoDrogas = a1.legalizacaoDrogas + a2.legalizacaoDrogas;
			return a;
		}
	}
		

	public EconomicAxis economic;
	public CivilAxis civil;
	public SocietalAxis societal;


	public void Set(Alignment a) {
		economic.Set (a.economic);
		civil.Set (a.civil);
		societal.Set (a.societal);
	}
	public static Alignment operator + (Alignment a1, Alignment a2){
		Alignment a = new Alignment();
		a.economic = a1.economic + a2.economic;
		a.civil = a1.civil + a2.civil;
		a.societal = a1.societal + a2.societal;
		return a;
	}

}
