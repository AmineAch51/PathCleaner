using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour {
    void Update() {
        transform.Rotate(0f, 0f, -8.5f, Space.World);
    }
}
