using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeScript : MonoBehaviour {
    private bool dontMove = false;
    public void disableMovement() {
        dontMove = true;
    }
    void OnMouseDown() {
        if (dontMove) {
            return;
        }
        GridMovementManagerScript.movedObject = this.gameObject;
    }
    void OnMouseUp() {
        GridMovementManagerScript.movedObject = null;
    }
}
