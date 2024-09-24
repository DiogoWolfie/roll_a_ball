using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource MusicadeFundo;
    public AudioClip[] musicasdefundo; //vetor para o caso de querer trocar
    // Start is called before the first frame update
    void Start()
    {

        AudioClip Musica = musicasdefundo[0];
        MusicadeFundo.clip = Musica;
        MusicadeFundo.loop = true;
        MusicadeFundo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
