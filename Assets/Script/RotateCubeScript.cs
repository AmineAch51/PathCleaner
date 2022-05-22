using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCubeScript : MonoBehaviour {
    private GameObject mainCube = null;
    public int rotationDegree;
    void Update() {
        if (!(MainCubeScript.mainCube==null ||
              MainCubeScript.mainCube.GetComponent<MainCubeScript>().isBlocked) &&
            mainCube!=null && 
            mainCube.transform.position.x== transform.position.x && 
            mainCube.transform.position.y==transform.position.y) {
            
            SoundManagerScript.playBiteSound();
            
            if (rotationDegree == -180) {
                switch (mainCube.GetComponent<MainCubeScript>().direction) {
                    case 'U': mainCube.GetComponent<MainCubeScript>().setDirection('D'); break;
                    case 'D': mainCube.GetComponent<MainCubeScript>().setDirection('U'); break;
                    case 'R': mainCube.GetComponent<MainCubeScript>().setDirection('L'); break;
                    case 'L': mainCube.GetComponent<MainCubeScript>().setDirection('R'); break;
                }
            }
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
