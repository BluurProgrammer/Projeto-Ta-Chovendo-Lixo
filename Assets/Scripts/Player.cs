using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public GameObject rotulo;
    private SpriteRenderer spriteRendererRotulo;

    public Sprite[] sprites;
    public Sprite[] rotulos;

    private int indexAtual;
    public Score score;
    private Animator animator;
    private float horizontal;
    private bool estaOlhandoParaDireita;
    private bool formaEscolhida;

    public float velocidade;

    void Start()
    {
        indexAtual = 0;
        loadComponentes();
    }

    void FixedUpdate()
    {
        controleInput();
    }

    private void limiteCamera()
    {
        var distanciaZ = (transform.position - Camera.main.transform.position).z;
        var bordaDireita = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanciaZ)).x;
        var bordaEsquerda = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanciaZ)).x;
        transform.position = new Vector3(bordaEsquerda, bordaDireita, transform.position.z);
    }

    private void trocaSprites(int indexSprite)
    {
        spriteRenderer.sprite = sprites[indexSprite];
        spriteRendererRotulo.sprite = rotulos[indexSprite];
    }

    private string getTag(int index)
    {
        string tagAtual = null;

        switch (index)
        {
            case 0: tagAtual = "lixoPapel"; break;
            case 1: tagAtual = "lixoPlastico"; break;
            case 2: tagAtual = "lixoVidro"; break;
            case 3: tagAtual = "lixoOrganico"; break;
        }

        return tagAtual;
    }

    private void trocaEmOrdemDecrescente()
    {
        indexAtual--;

        if (indexAtual < 0)
            indexAtual = 3;

        string tagAtual = getTag(indexAtual);

        if (tagAtual != null)
        {
            transform.tag = tagAtual;
            trocaSprites(indexAtual);
        }
    }

    private void trocaEmOrdemCrescente()
    {
        if (indexAtual >= sprites.Length)
            indexAtual = 0;

        string tagAtual = getTag(indexAtual);

        if (tagAtual != null)
        {
            transform.tag = tagAtual;
            trocaSprites(indexAtual);
            indexAtual++;
        }
    }

    private void ganhaPonto()
    {
        if (score != null)
            score.addPonto(1);
    }

    private void controleInput()
    {
        trocaForma();
        horizontal = Input.GetAxis("Horizontal");
        movimentar(horizontal);
    }

    private void trocaForma()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            trocaEmOrdemCrescente();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            trocaEmOrdemDecrescente();
    }

    private void animaRoda()
    {
        //animator.Setfloat("run",Mathf.abs(horizontal)); // roda
    }

    private Sprite getSpriteInVetor(string nomeSprite)
    {

        Sprite resultado = null;
        foreach (Sprite sprite in sprites)
        {
            if (nomeSprite.Equals(sprite.name))
            {
                resultado = sprite;
                break;
            }
        }

        return resultado;
    }

    private void movimentar(float horizontal)
    {
        rigidBody.velocity = new Vector2(horizontal * velocidade, rigidBody.velocity.y);
        animaRoda();
        mudarDirecao(horizontal);
    }

    private void reflete()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        rotulo.transform.localScale = new Vector2(-rotulo.transform.localScale.x, rotulo.transform.localScale.y);
    }

    private void mudarDirecao(float horizontal)
    {
        if ((horizontal > 0 && !estaOlhandoParaDireita) || (horizontal < 0 && estaOlhandoParaDireita))
        {
            estaOlhandoParaDireita = !estaOlhandoParaDireita;
            reflete();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        bool lixoSeletivoOk = transform.tag.Equals(coll.tag);

        if (lixoSeletivoOk)
            ganhaPonto();
        else
            perdeu();
    }

    private void perdeu()
    {
        GameController.INSTANCE.GameState = GameState.GAMEOVER;
        GameController.INSTANCE.game();
    }

    private void loadComponentes()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRendererRotulo = rotulo.GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }
}
