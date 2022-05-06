using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManagerScript : MonoBehaviour {
    public GameObject _void;
    public GameObject _wall;
    public GameObject _mainCube;
    public GameObject _goUp;
    public GameObject _goDown;
    public GameObject _goRight;
    public GameObject _goLeft;
    public GameObject _finish;
    private Dictionary<int, GameObject> cubes;
    private static Dictionary<int, Transform> row, column;
    public Camera cam;
    void Awake() {
        cubes = new Dictionary<int, GameObject>();
        row = new Dictionary<int, Transform>();
        column = new Dictionary<int, Transform>();
        cubes.Add(0, _void);
        cubes.Add(1, _wall);
        cubes.Add(2, _mainCube);
        cubes.Add(3, _goUp);
        cubes.Add(4, _goDown);
        cubes.Add(5, _goRight);
        cubes.Add(6, _goLeft);
        cubes.Add(7, _finish);
    }
    private static int[][] map = {
        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    public static Transform getVal(int i, int j, bool direction) {
        if (map[i][j] == 1) {
            return null;
        }

        return direction ? row[i] : column[j];
    }
    void generateMap() {
        float ii=0, jj;
        for (int i = 0; i < map.Length; ++i) {
            jj = 0;
            for (int j = 0; j < map[i].Length; ++j) {
                // if (i != 0 || j != 0){
                //     continue;
                // }
                GameObject go = Instantiate(cubes[map[i][j]], new Vector3(jj, ii), Quaternion.identity);
                if (i == 0 || j == 0) {
                    if (!column.ContainsKey(j)) {
                        column.Add( j,  go.transform);
                    }
                    if (!row.ContainsKey(i)) {
                        row.Add( i,  go.transform);   
                    }
                }
                if (map[i][j] == 2) {
                    Vector3 vec3 = go.transform.position;
                    vec3.z = -3f;
                    go.transform.position = vec3;
                    Instantiate(cubes[0], new Vector3(jj, ii), Quaternion.identity);
                    go.GetComponent<MainCubeScript>().setPos( i, j ); 
                }
                jj += 0.96f;
            }
            ii -= 0.96f;
        }
    }
    void Start() {
        generateMap();
    }
}
