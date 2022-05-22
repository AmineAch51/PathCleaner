using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {
    public static bool pause=true;
    public static bool finish = false;
    public static GameObject screenManager;
    public GameObject back;
    public char initDirectionOfMainCube;
    public int nextLevel;
    private Animator anim;

    public static bool rePlayMusic = false;
    
    IEnumerator show() {
        pause = true;
        anim.SetBool("show", true);
        yield return new WaitForSeconds(1);
        back.SetActive(false);
        pause = false;
    }

    void Awake() {
        MainCubeScript._direction = initDirectionOfMainCube;
        MainCubeScript.nextLevel = nextLevel;
    }

    void Start() {
        screenManager = gameObject;
        anim = back.GetComponent<Animator>();
        StartCoroutine(show());
    }

    IEnumerator hide(int index) {
        pause = true;
        back.SetActive(true);
        anim.SetTrigger("hide");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
        ScreenManager.finish = false;
    }
    public void _Go(int index) {
        //Debug.Log(index+" "+SceneManager.GetActiveScene().buildIndex);
        if(index!=SceneManager.GetActiveScene().buildIndex &&
           MusicManagerScript.musicManager != null) {
            Destroy(MusicManagerScript.musicManager);
        }
        rePlayMusic = (index != SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(hide(index));
    }
    
    public void Go(int index) {
        if (index!=SceneManager.GetActiveScene().buildIndex &&
            MusicManagerScript.musicManager != null) {
            Destroy(MusicManagerScript.musicManager);
        }
        rePlayMusic = (index != SceneManager.GetActiveScene().buildIndex);
        SoundManagerScript.playClickButtonSound();
        StartCoroutine(hide(index));
    }

    public void Exit() {
        Application.Quit();
    }
}
