using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishScript : MonoBehaviour {
    private GameObject mainCube = null;
    public GameObject _screenManager;

    private void Start() {
        _screenManager = GameObject.Find("ScreenManager");
    }

    private void Update() {
        if(ScreenManager.finish) {
            return;
        }
        if (!(MainCubeScript.mainCube==null ||
              MainCubeScript.mainCube.GetComponent<MainCubeScript>().isBlocked) &&
            mainCube!=null && 
            mainCube.transform.position.x== transform.position.x && 
            mainCube.transform.position.y==transform.position.y) {
            mainCube = null;
            ScreenManager.finish = true;
            _screenManager.GetComponent<ScreenManager>()._Go(MainCubeScript.nextLevel);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("MainCube")) {
            mainCube = col.gameObject;
            // col.gameObject.GetComponent<MainCubeScript>().addDirection(direction);
            // Destroy(gameObject, 0.6f); // Kill your self after 2 seconds
        }
    }
}
