using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public int ponto { get; private set; }

    private Text txtScore;

	void Start()
    {
		ponto = 0;
        txtScore = GetComponent<Text> ();
	}

	private void atualizaPontuacao()
	{
        txtScore.text = "Score: "+ ponto.ToString();
	}
	
	public void addPonto(int valorPonto)
	{
		ponto += valorPonto;
		atualizaPontuacao();
	}
}
