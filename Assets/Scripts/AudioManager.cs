using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour{

    public static AudioManager Instance { get; private set; }
    public AudioSource audioSource { get; private set; }

    public AudioClip[] audios;
    private Dictionary<string, AudioClip> dicionario = new Dictionary<string, AudioClip>();

    private AudioManager()
    { 
    }

    private void loadAudio()
    {
        foreach (AudioClip audio in audios)      
            dicionario.Add(audio.name, audio);       
    }

    void Start()
    {
        loadAudio();
        audioSource = GetComponent<AudioSource>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;       
    }

    private AudioClip buscarAudio(string nome)
    {
        if (dicionario.ContainsKey(nome))        
            return dicionario[nome];
              
        return null;
    }

    public void PlayAudio(string nome)
    {
        AudioClip audioClip = buscarAudio(nome);

        if (audioClip != null)        
            audioSource.PlayOneShot(audioClip);       
    }
}
