using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {
    public static bool pause=true;
    public static bool finish = false;
    public GameObject back;
    private Animator anim;
    IEnumerator show() {
        pause = true;
        anim.SetBool("show", true);
        yield return new WaitForSeconds(1);
        back.SetActive(false);
        pause = false;
    }
    void Start() {
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
    public void Go(int index) {
        StartCoroutine(hide(index));
    }

    public void Exit() {
        Application.Quit();
    }
}
