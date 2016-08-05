using UnityEngine;
using UnityEngine.UI;

public class PontosGameOver : MonoBehaviour {

    private Text txtScore;
    public Score score;

    private void atualiza()
    {
        txtScore.text += score.ponto;
    }

    void Start ()
    {
        txtScore = GetComponent<Text>();
        atualiza();
    }
}
