using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public class Count {
        public int minimum;
        public int maximum;
        public Count (int min, int max) {
            minimum = min;
            maximum = max;
        }
    }
    public int columns = 8; //主场景列数
    public int rows = 8; //主场景行数
    public GameObject[] floorTiles;
    private List<Vector3> gridPositions = new List<Vector3> ();

    // 初始化位置列表
    void InitialiseList () {
        gridPositions.Clear ();
        for (int x = 1; x < columns; x++) {
            for (int y = 1; y < rows; y++) {
                gridPositions.Add (new Vector3 (x, y, 0f));
            }
        }

    }

}