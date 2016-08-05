using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabLixo : MonoBehaviour {

    public List<Vector3> posicaoObjetosPredioCentral;
    public List<Vector3> posicaoObjetosPredioEsquerda;
    public List<Vector3> posicaoObjetosPredioDireito;

    public List<GameObject> objetos;
    public GameObject prefabMao;

    public SpawnPrefabLixo()
    {
        posicaoObjetosPredioCentral = new List<Vector3>();
        posicaoObjetosPredioEsquerda = new List<Vector3>();
        posicaoObjetosPredioDireito = new List<Vector3>();
        objetos = new List<GameObject>();
    }

    void Start ()
	{
		 StartCoroutine (spawn ());
    }
	
    private int sortear(int tamanhoMaximo)
    {
        return Random.Range(0, tamanhoMaximo);
    }

	private IList<Vector3> getLista()
    {
        int listaSorteada = sortear(3);
 	
        if (listaSorteada == 0) 
			return posicaoObjetosPredioCentral;

		if (listaSorteada == 1)
			return posicaoObjetosPredioDireito;
	
	    return posicaoObjetosPredioEsquerda;
	}
	
	private Vector3 getPosicaoObjeto()
    {
		IList<Vector3> listaSorteada = getLista ();

		int indexSorteado = sortear(listaSorteada.Count);

		Vector3 vetorPosicao = listaSorteada[indexSorteado];

        return vetorPosicao;
    }

    private GameObject getObjeto()
    {
        int indexSorteado = sortear(objetos.Count);
        GameObject gameObject = objetos[indexSorteado];

        return gameObject;
    }
	
	IEnumerator spawn () 
	{
        bool aindaJogando = GameController.INSTANCE.GameState == GameState.JOGANDO;

        while (aindaJogando) 
		{
			yield return new WaitForSeconds(4.0f);

			Vector3 posicao = getPosicaoObjeto();
			GameObject objeto = getObjeto();
		
			GameObject mao = Instantiate(prefabMao, posicao, Quaternion.identity) as GameObject;
			
			yield return new WaitForSeconds(1.1f);
			Instantiate(objeto, posicao, Quaternion.identity);
           
			yield return new WaitForSeconds(1.4f);
			Destroy(mao);
		}
	}
}
