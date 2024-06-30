/*
* Author: Rylan Neo
* Date of creation: 20th June 2024
* Description: Easy Audio management
*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// The 2 sources of audio for this game
    /// </summary>
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource audioSource;

    /// <summary>
    /// All audio clips are stored her and ay=udio manager is referenced when want to play a clip
    /// </summary>
    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;
    public AudioClip bgm4;
    public AudioClip click;
    public AudioClip consumeDrug;
    public AudioClip itemPickUp;
    public AudioClip boxBreak;
    public AudioClip enemyDeath;
    public AudioClip bossDeath;
    public AudioClip gunFire;
    public AudioClip playerHurt;
    public AudioClip playerDead;
    public AudioClip enterCave;
    public AudioClip teleport;

    /// <summary>
    /// The function to change the bgm
    /// </summary>
    /// <param name="bgm"></param>
    public void PlayBGM(AudioClip bgm)
    {
        musicSource.clip = bgm;
        musicSource.loop = true;
        musicSource.Play();
    }

    /// <summary>
    /// The function to play a certain SFX
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
