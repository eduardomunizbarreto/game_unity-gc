using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAudio : MonoBehaviour
{

    public AudioSource audioSource;

    public static ControladorAudio instancia;

    void Awake()
    {
        if (instancia == null)
            instancia = this;
        else Destroy(gameObject);
    }


    public void playAudio(AudioClip audio)
    {
        this.audioSource.clip = audio;
        this.audioSource.Play();
    }
}
