using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionScript : MonoBehaviour {
    public char direction='U';
    private GameObject mainCube = null;
    // IEnumerator changeDirection(Collider2D col) {
    //     yield return new WaitForSeconds(1);
    //     col.gameObject.GetComponent<MainCubeScript>().setDirection(direction);
    // }

    private void Update() {
        if (!(MainCubeScript.mainCube==null ||
              MainCubeScript.mainCube.GetComponent<MainCubeScript>().isBlocked) &&
             mainCube!=null && 
             mainCube.transform.position.x== transform.position.x && 
             mainCube.transform.position.y==transform.position.y) {
                //Debug.Log("Here");
                SoundManagerScript.playBiteSound();
                mainCube.GetComponent<MainCubeScript>().setDirection(direction);
                mainCube = null;
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("MainCube")){
            mainCube = col.gameObject;
        }
    }
}
