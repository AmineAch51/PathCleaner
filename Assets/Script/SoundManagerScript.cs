using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {
    private static AudioSource audioSource;
    private static AudioClip clickButtonSound;
    private static AudioClip biteSound;
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
        clickButtonSound = Resources.Load<AudioClip>("Button-click-sound");
        biteSound = Resources.Load<AudioClip>("bite");
        audioSource.volume = SettingManagerScript.soundVolume;
    }
    
    public static void playClickButtonSound() {
        audioSource.PlayOneShot(clickButtonSound);
    }
    public static void playBiteSound() {
        audioSource.PlayOneShot(biteSound);
    }
}