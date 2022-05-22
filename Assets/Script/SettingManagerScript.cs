using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManagerScript : MonoBehaviour {
    public static float soundVolume=1f;
    public static float musicVolume=1f;
    public Slider soundSlider;
    public Slider musicSlider;
    void Awake() {
        soundSlider.value = soundVolume;
        musicSlider.value = musicVolume;
    }
    public void setSoundVolume() {
        soundVolume = soundSlider.value;
    }
    public void setMusicVolume() {
        musicVolume = musicSlider.value;
    }
}
