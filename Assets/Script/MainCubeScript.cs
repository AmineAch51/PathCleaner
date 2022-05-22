using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class MainCubeScript : MonoBehaviour {
    private Rigidbody2D rig;
    private List<char> directions = new List<char>(){ 'U' };
    public char direction; // L T R D
    public static char _direction;
    public static int nextLevel;
    private float maxTimeToWait = 0f, timeToWait = 0f;
    void Start() {
        rig = GetComponent<Rigidbody2D>();
        mainCube = gameObject;
        direction = _direction;
    }
    void setPosition(float x, float y, float z) {
        transform.position = new Vector3(x, y, z);
    }
    private Transform tr=null;
    private int i, j;
    public static GameObject mainCube;
    public bool isBlocked=false;
    public void setPos(int i, int j) {
        this.i = i;
        this.j = j;
    }
    public void setDirection(char direction) {
        this.direction = direction;
    }
    void Update() {
        rig.velocity = new Vector2(0f, 0f);
        if(isBlocked) {
            return;
        }
        if (ScreenManager.finish) {
            return;
        }
        if (ScreenManager.pause) {
            return;
        }
        if (_moveUp || _moveDown || _moveRight || _moveLeft) {
            switch (direction){
                case 'U': moveUp(); break; 
                case 'D': moveDown(); break; 
                case 'R': moveRight(); break; 
                case 'L': moveLeft(); break; 
            }
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
            isBlocked = true;
            ScreenManager.screenManager.GetComponent<ScreenManager>()._Go(SceneManager.GetActiveScene().buildIndex);
            return;
        }
        // if (direction == 'R' || direction == 'L') {
        //     j += (direction=='R' ? 1 : -1);
        //     transform.position = new Vector2(tr.position.x, transform.position.y);
        // } else if (direction == 'U' || direction == 'D') {
        //     i += (direction=='D' ? 1 : -1);
        //     transform.position = new Vector2(transform.position.x, tr.position.y);
        // }
        if (direction == 'U') {
            _moveUp = true;
        }else if (direction == 'D') {
            _moveDown = true;
        } else if (direction == 'R') {
            _moveRight = true;
        }else if (direction == 'L') {
            Debug.Log("Move left -------------");
            _moveLeft = true;
        }
        timeToWait = maxTimeToWait;
    }

    private bool _moveUp=false;
    private bool _moveDown=false;
    private bool _moveRight=false;
    private bool _moveLeft=false;

    private float speed=1.5f;
    void moveUp() {
        rig.velocity = new Vector2(0, speed);
        if (transform.position.y>=tr.position.y) {
            rig.velocity = new Vector2(0f, 0f);
            _moveUp = false;
            --i;
            transform.position = new Vector2(transform.position.x, tr.position.y);
            tr = null;
        }
    }
    void moveDown() {
        rig.velocity = new Vector2(0, -speed);
        if (transform.position.y<=tr.position.y) {
            rig.velocity = new Vector2(0f, 0f);
            _moveDown = false;
            ++i;
            transform.position = new Vector2(transform.position.x, tr.position.y);
            tr = null;
        }
    }
    void moveRight() {
        rig.velocity = new Vector2(speed, 0);
        if (transform.position.x >= tr.position.x) {
            rig.velocity = new Vector2(0f, 0f);
            _moveRight = false;
            ++j;
            transform.position = new Vector2(tr.position.x, transform.position.y);
            tr = null;
        }
    }
    void moveLeft() {
        rig.velocity = new Vector2(-speed, 0);
        if (transform.position.x <= tr.position.x) {
            rig.velocity = new Vector2(0f, 0f);
            _moveLeft = false;
            --j;
            transform.position = new Vector2(tr.position.x, transform.position.y);
            tr = null;
        }
    }
}
