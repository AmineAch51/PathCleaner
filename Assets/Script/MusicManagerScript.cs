using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagerScript : MonoBehaviour {
    public static GameObject musicManager;
    private AudioSource audioSource;
    private Dictionary<int, AudioClip> musicForLevels;
    public int sceneMusicIndex;
    void Awake() {
        if (musicManager == null) {
            audioSource = GetComponent<AudioSource>();
            musicForLevels = new Dictionary<int, AudioClip>();
            init();
            Destroy(musicManager);
            Debug.Log("Is null");
            audioSource.PlayOneShot(musicForLevels[sceneMusicIndex]);
            musicManager = this.gameObject;
        }

        musicManager.GetComponent<MusicManagerScript>().audioSource.volume = SettingManagerScript.musicVolume;
        DontDestroyOnLoad(this.gameObject);
    }

    void init() {
        string prefix = "musicForLevel";
        
        musicForLevels.Add(1, Resources.Load<AudioClip>(prefix+"1"));
        musicForLevels.Add(2, Resources.Load<AudioClip>(prefix+"2"));
        musicForLevels.Add(3, Resources.Load<AudioClip>(prefix+"3"));
        musicForLevels.Add(4, Resources.Load<AudioClip>(prefix+"4"));
    }
}
