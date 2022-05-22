using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class VoidScript : MonoBehaviour {
    public Renderer rend;
    public int i, j;
    public void setPos(int i, int j) {
        this.i = i;
        this.j = j;
    }
    void Awake() {
        rend = GetComponent<Renderer>();
    }
    void OnMouseOver() {
        rend.material.color = new Color(0.9f, 0.9f, 0.9f);
        if (GridMovementManagerScript.movedObject != null) {
            Vector3 vec3 = transform.position;
            GridMovementManagerScript.movedObject.transform.position = vec3;
            if(GridMovementManagerScript.movedObject.CompareTag("Portal")) {
                GridMovementManagerScript.movedObject.GetComponent<PortalScript>().setPos(i, j);
            }
        }
    }
    void OnMouseExit() {
        rend.material.color = new Color(1f, 1f, 1f);
    }
}
