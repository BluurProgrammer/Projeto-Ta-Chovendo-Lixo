using UnityEngine;

public class GameController : MonoBehaviour {

    private GameController()
    { }

    public GameState GameState { get; set; }

    public GameObject[] componentes;
    public static GameController INSTANCE { get; private set; }

    void Awake()
    {
        GameState = GameState.INICIO;

        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
        }

        INSTANCE = this;
    }

    private void inicio()
    {
        GameState = GameState.JOGANDO;

        foreach (GameObject ob in componentes)
        {
            if (ob.CompareTag("txtInicio") || ob.CompareTag("panel"))
                ob.gameObject.SetActive(false);

            if (ob.CompareTag("txtScore") || ob.CompareTag("spawnLixo"))
                ob.gameObject.SetActive(true);
        }
    }

    private void pauserGame()
    {
        Pauser.INSTANCE.pausar();
    }

    private void fim()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void gameOver()
    {
        GameState = GameState.FIM;

        foreach (GameObject ob in componentes)
        {
            if (ob.CompareTag("spawnLixo") || ob.CompareTag("txtScore"))
                ob.gameObject.SetActive(false);

            if (ob.CompareTag("panel") || ob.CompareTag("txtGameOver"))
                ob.gameObject.SetActive(true);
        }
    }

    public void game()
    {
        switch (GameState)
        {
            case GameState.INICIO: inicio(); break;

            case GameState.JOGANDO: pauserGame(); break;

            case GameState.GAMEOVER: gameOver(); break;

            case GameState.FIM: fim(); break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))      
            game();      
    }
}
