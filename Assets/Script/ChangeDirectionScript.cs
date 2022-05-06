using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionScript : MonoBehaviour {
    public char direction='U';
    private GameObject mainCube = null;
    IEnumerator changeDirection(Collider2D col) {
        yield return new WaitForSeconds(1);
        col.gameObject.GetComponent<MainCubeScript>().setDirection(direction);
    }

    private void Update() {
        if (mainCube!=null && 
            mainCube.transform.position.x== transform.position.x && 
            mainCube.transform.position.y==transform.position.y) {
            //Debug.Log("Here");
            mainCube.GetComponent<MainCubeScript>().setDirection(direction);
            mainCube = null;
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.CompareTag("MainCube")){
            mainCube = col.gameObject;
            // col.gameObject.GetComponent<MainCubeScript>().addDirection(direction);
            // Destroy(gameObject, 0.6f); // Kill your self after 2 seconds
        }
    }
}
