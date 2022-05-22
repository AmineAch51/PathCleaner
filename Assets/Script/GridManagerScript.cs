using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GridManagerScript : MonoBehaviour {
    public int levelIndex;
    
    public GameObject _void;
    public GameObject _wall;
    public GameObject _mainCube;
    public GameObject _goUp;
    public GameObject _goDown;
    public GameObject _goRight;
    public GameObject _goLeft;
    public GameObject _finish;
    public GameObject _portal;
    public GameObject _rotateMinus180;

    private GameObject portal1;
    private GameObject portal2;
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
        cubes.Add(8, _portal); // Up
        cubes.Add(9, _portal); // Down
        cubes.Add(10, _portal); // Right
        cubes.Add(11, _portal); // Left
        cubes.Add(12, _rotateMinus180);
        
        readLevel();
        generateMap();
    }
    
    private static int[][] map;
    // {
    // new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    // new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7},
    // new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 1},
    // new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    // new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    // };
    public static Transform getVal(int i, int j, bool direction) {
        if (map[i][j] == 1) {
            return null;
        }

        return direction ? row[i] : column[j];
    }
    void readLevel() {
        // string path = "Assets/Resources/Levels/level" + levelIndex + ".txt";
        //
        // //Read the text from directly from the test.txt file
        // StreamReader reader = new StreamReader(path); 
        //
        // List<List<int>> l = new List<List<int>>();
        // string? line;
        // while ((line = reader.ReadLine()) != null) {
        //     string[] strs = line.Split(' ');
        //     List<int> _l = new List<int>();
        //     foreach(string str in strs) {
        //         if (str.Trim().Length == 0) {
        //             continue;
        //         }
        //         _l.Add(int.Parse(str.Trim()));
        //     }
        //     l.Add(_l);
        // }
        //
        // reader.Close();
        // string fileName = "Assets/Resources/level" + levelIndex + ".txt";
        // string text = File.ReadAllText(fileName);
        // string[] lines = text.Split(Environment.NewLine[0]);
        // foreach (string line in lines) {
        //     string[] strs = line.Split(' ');
        //     List<int> _l = new List<int>();
        //     foreach(string str in strs) {
        //         if (str.Trim().Length == 0) {
        //             continue;
        //         }
        //         _l.Add(int.Parse(str.Trim()));
        //     }
        //     l.Add(_l);
        // }
        // int[][] _map = new int[l.Count][];
        // for (int i = 0; i < l.Count; ++i) {
        //     _map[i] = l[i].ToArray();
        // }
        map = bigMap[levelIndex-1];
    }
    void generateMap() {
        float ii=0, jj;
        for (int i = 0; i < map.Length; ++i) {
            jj = 0;
            for (int j = 0; j < map[i].Length; ++j) {
                // if (i != 0 || j != 0){
                //     continue;
                // }
                bool negativeValue = map[i][j] < 0;
                int val = Math.Abs(map[i][j]);
                GameObject go = Instantiate(cubes[val], new Vector3(jj, ii), Quaternion.identity);
                if (negativeValue) {
                    Debug.Log("Negative value");
                    if (go.GetComponent<MoveCubeScript>() != null) { 
                        go.GetComponent<MoveCubeScript>().disableMovement();
                    }
                }
                if (i == 0 || j == 0) {
                    if (!column.ContainsKey(j)) {
                        column.Add( j,  go.transform);
                    }
                    if (!row.ContainsKey(i)) {
                        row.Add( i,  go.transform);   
                    }
                }

                if (val == 0) {
                    go.GetComponent<VoidScript>().setPos( i, j ); 
                }
                
                if (val == 2) {
                    Vector3 vec3 = go.transform.position;
                    //vec3.z = -3f;
                    go.transform.position = vec3;
                    //Instantiate(cubes[0], new Vector3(jj, ii), Quaternion.identity);
                    go.GetComponent<MainCubeScript>().setPos( i, j ); 
                }
                
                if(val >7 && val<12){
                    PortalScript portalScript = go.GetComponent<PortalScript>();
                    portalScript.setPos(i, j);
                    Debug.Log("/// "+val);
                    switch (val) {
                        case 8: portalScript.direction = 'U'; break;
                        case 9: portalScript.direction = 'D'; break;
                        case 10: portalScript.direction = 'R'; break;
                        case 11: portalScript.direction = 'L'; break;
                    }
                    if (portal1 == null) {
                        portal1 = go;
                    } else if (portal2 == null) {
                        portal2 = go;
                    }
                }
                
                jj += 0.96f;
            }
            ii -= 0.96f;
        }

        if(portal1 != null && portal2 != null) {
            portal1.GetComponent<PortalScript>().setOtherObject(portal2);
            portal2.GetComponent<PortalScript>().setOtherObject(portal1);
        }
    }
    
    private static int[][][] bigMap = {
        new int[][] {
            new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        },
        new int[][] {
            new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 11, 0, 0, 0, 0, 0, 0, 0, 1, 0, 7 },
            new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 11, 1, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 1, 3, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1 },
            new int[] { 1, 5, 1, 0, 0, 0, 1, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        },
        new int[][] {
            new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            new int[] { 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7 },
            new int[] { 1, 11, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            new int[] { 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11, 0, 1 },
            new int[] { 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 12, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 6, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 0, 0, 1, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        },
        new int[][] {
            new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
            new int[] { 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
            new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 4, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 6, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, -9, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 1, 0, 7, 0, 1, 0, 1, 1, 1, 0, 12, 0, 1, 1, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 1, 3, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
            new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 3, 0, 0, 1 },
            new int[] { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
            new int[] { 1, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, -10, 0, 0, 0, 0, 0, 0, 1 },
            new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        }
    };
}
