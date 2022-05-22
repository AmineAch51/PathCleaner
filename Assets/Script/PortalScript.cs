using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour {
    public GameObject otherGate;
    public char direction;
    public int i, j;
    private GameObject mainCube = null;
    public bool youCanTeleport = true;

    void rotateArrow() {
        GameObject go = transform.GetChild(1).gameObject;
        var rotationVector = go.transform.rotation.eulerAngles;
        switch (direction) {
            case 'U': rotationVector.z = 0f; break;
            case 'D': rotationVector.z = 180f; break;
            case 'R': rotationVector.z = -90f; break;
            case 'L': rotationVector.z = 90f; break;    
        }
        go.transform.rotation = Quaternion.Euler(rotationVector);
    }
    
    void Start() {
        rotateArrow();
    }

    public void setPos(int i, int j) {
        this.i = i;
        this.j = j;
    }
    public void setOtherObject(GameObject otherGate) {
        this.otherGate = otherGate;
        Debug.Log(i+" "+j);
    }
    public IEnumerator telePort(PortalScript portalScript, GameObject go) {
        
        yield return new WaitForSeconds(0.25f);
        
        go.SetActive(false);
        portalScript.youCanTeleport = false;
        Vector3 vec3 = portalScript.transform.position;
        go.transform.position = vec3;
        gameObject.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
        gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
        gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
        MainCubeScript mainCubeScript = go.GetComponent<MainCubeScript>();
        Debug.Log("Portal direction "+ portalScript.direction);
        mainCubeScript.setDirection(portalScript.direction);
        mainCubeScript.setPos( portalScript.i, portalScript.j );

        yield return new WaitForSeconds(0.25f);
        
        go.SetActive(true);
        ScreenManager.pause = false;
        
        SoundManagerScript.playBiteSound();
        
        Destroy(gameObject);
        Destroy(portalScript.gameObject);
    }
    private void Update() {
        if (ScreenManager.pause) {
            return;
        }
        if (youCanTeleport &&
            !(MainCubeScript.mainCube==null ||
              MainCubeScript.mainCube.GetComponent<MainCubeScript>().isBlocked) &&
            mainCube!=null && 
            mainCube.transform.position.x== transform.position.x && 
            mainCube.transform.position.y==transform.position.y) {
            //Debug.Log("Here");
            
            SoundManagerScript.playBiteSound();
            
            GameObject go = mainCube;
            mainCube = null;
            ScreenManager.pause = true;
            PortalScript portalScript = otherGate.GetComponent<PortalScript>();
            StartCoroutine(telePort(portalScript, go));
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("MainCube")) {
            mainCube = col.gameObject;
            // col.gameObject.GetComponent<MainCubeScript>().addDirection(direction);
            // Destroy(gameObject, 0.6f); // Kill your self after 2 seconds
        }
    } 
    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("MainCube")) {
            youCanTeleport = true;
        }
    }
}
