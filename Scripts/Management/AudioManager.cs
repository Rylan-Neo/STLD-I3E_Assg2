using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource audioSource;

    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;
    public AudioClip bgm4;
    public AudioClip click;
    public AudioClip itemPickUp;
    public AudioClip boxBreak;
    public AudioClip enemyDeath;
    public AudioClip bossDeath;
    public AudioClip pauseMenu;
    public AudioClip gunFire;
    public AudioClip playerHurt;
    public AudioClip playerDead;
    public AudioClip enterCave;
    public AudioClip teleport;

    public void PlayBGM(AudioClip bgm)
    {
        musicSource.clip = bgm;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
