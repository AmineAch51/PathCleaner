using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class VoidScript : MonoBehaviour {
    public Renderer rend;
    void Awake() {
        rend = GetComponent<Renderer>();
    }
    void OnMouseOver() {
        rend.material.color = new Color(0.9f, 0.9f, 0.9f);
        if (GridMovementManagerScript.movedObject != null) {
            Vector3 vec3 = transform.position;
            vec3.z = -2f;
            GridMovementManagerScript.movedObject.transform.position = vec3;
        }
    }
    void OnMouseExit() {
        rend.material.color = new Color(1f, 1f, 1f);
    }
}
