using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementManagerScript : MonoBehaviour {
    public static GameObject movedObject;
    public static Transform newPlace;
    private bool youCanMove = false;
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            youCanMove = true;
        }
        
        if (Input.GetMouseButtonUp(0)) {
            youCanMove = false;
        }
        
        if (youCanMove && movedObject != null){
            /* Follow camera script */
            // mousePosition = Input.mousePosition;
            // mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            // Vector3 vec3 = Vector2.Lerp(movedObject.transform.position, mousePosition, 5f);
            // vec3.z = -2f;
            // movedObject.transform.position = vec3;
            
        }
        // if (Input.GetMouseButtonDown(1)) {
        //     Debug.Log("Pressed secondary button.");
        // }
        //
        // if (Input.GetMouseButtonDown(2)) {
        //     Debug.Log("Pressed middle click.");
        // }
    }
}
