using UnityEngine;

public class Pauser : MonoBehaviour {

    public static Pauser INSTANCE { get; private set; }

    private Pauser()
    {
    }

    void Awake()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
        }

        INSTANCE = this;
    }

    public void pausar()
    {
        if (Time.timeScale <= 0)
        {
            AudioManager.Instance.audioSource.Play();
            Time.timeScale = 1;
        }
        else
        {
            AudioManager.Instance.audioSource.Stop();
            Time.timeScale = 0;
        }
    }
}
