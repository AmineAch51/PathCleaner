using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class MainCubeScript : MonoBehaviour {
    private Rigidbody2D rig;
    public float speed;
    private List<char> directions = new List<char>(){ 'U' };
    private char direction = 'U'; // L T R D
    private float maxTimeToWait = 0.6f, timeToWait = 0.6f;
    

    void Start() {
        rig = GetComponent<Rigidbody2D>();
    }

    void setPosition(float x, float y, float z) {
        transform.position = new Vector3(x, y, z);
    }
    private Transform tr=null;
    private int i, j;
    public void setPos(int i, int j) {
        this.i = i;
        this.j = j;
    }
    public void setDirection(char direction) {
        this.direction = direction;
    }
    void Update() {
        if (ScreenManager.finish) {
            return;
        }
        if (ScreenManager.pause) {
            return;
        }
        // if (Input.GetKey(KeyCode.Z)){
        //     direction = 'U';
        // }
        // if (Input.GetKey(KeyCode.D)){
        //     direction = 'R';
        // }
        // if (Input.GetKey(KeyCode.S)){
        //     direction = 'D';
        // }
        // if (Input.GetKey(KeyCode.Q)){
        //     direction = 'L';
        // }
        if (timeToWait > 0) {
            timeToWait -= Time.deltaTime;
            return;
        }
        tr = null;
        if (direction == 'U') {
            tr = GridManagerScript.getVal(i - 1, j, true);
        }else if (direction == 'D') {
            tr = GridManagerScript.getVal(i + 1, j, true);
        }else if (direction == 'R') {
            tr = GridManagerScript.getVal(i, j+1, false);
        }else if (direction == 'L') {
            tr = GridManagerScript.getVal(i, j-1, false);
        }
        if (tr == null) {
            return;
        }
        if (direction == 'R' || direction == 'L') {
            j += (direction=='R' ? 1 : -1);
            transform.position = new Vector3(tr.position.x, transform.position.y, -3f);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(tr.position.x, transform.position.y, -3f), 1f * Time.deltaTime);
        } else if (direction == 'U' || direction == 'D') {
            i += (direction=='D' ? 1 : -1);
            transform.position = new Vector3(transform.position.x, tr.position.y, -3f);
        }
        timeToWait = maxTimeToWait;
    }
}
