using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeScript : MonoBehaviour {
    
    void OnMouseDown() {
        GridMovementManagerScript.movedObject = this.gameObject;
    }
    void OnMouseUp() {
        GridMovementManagerScript.movedObject = null;
    }
}
