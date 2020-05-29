using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.U2D;


public class LevelManager : MonoBehaviour {
    [SerializeField]
    private Transform map = null;
    [SerializeField]
    private Texture2D[] mapData = null;
    [SerializeField]
    private MapElement[] mapElements = null;
    [SerializeField]
    private Sprite defaultTile = null;
    private Dictionary<Point, GameObject> waterTiles = new Dictionary<Point, GameObject>();
    [SerializeField]
    private SpriteAtlas waterAtlas;

    private Vector3 WorldStartPos {
        get {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        }
    }
    // Start is called before the first frame update
    void Start() {
        GenerateMap();
    }

    // Update is called once per frame
    void Update() {

    }

    private void GenerateMap() {
        int height = mapData[0].height;
        int width = mapData[0].width;

        for (int i = 0; i < mapData.Length; i++) {
            for (int x = 0; x < mapData[i].width; x++) {
                for (int y = 0; y < mapData[i].height; y++) {
                    Color c = mapData[i].GetPixel(x, y);
                    MapElement newElement = Array.Find(mapElements, e => e.MyColor == c);
                    Debug.Log(c.ToString());
                    if (newElement != null) {
                        float xPos = WorldStartPos.x + (defaultTile.bounds.size.x * x);
                        float yPos = WorldStartPos.y + (defaultTile.bounds.size.y * y);
                        GameObject go = Instantiate(newElement.MyElementPrefab);
                        go.transform.position = new Vector2(xPos, yPos);
                        if (newElement.MyTileTag == "Water") {
                            waterTiles.Add(new Point(x, y), go);
                        }
                        go.transform.parent = map;
                        // if (newElement.MyTileTag == "Tree") {
                        //     go.GetComponent<SpriteRenderer> ().sortingOrder = height * 2 - y * 2;
                        // }
                    }
                }
            }
        }
        CheckWater();
    }

    public void CheckWater() {
        foreach (KeyValuePair<Point, GameObject> tile in waterTiles) {
            string composition = TileCheck(tile.Key);
            if (Regex.IsMatch(composition, @".E.WE.W.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_0");
            } else if (Regex.IsMatch(composition, @".W.WE.W.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_1");
            } else if (Regex.IsMatch(composition, @".W.WE.E.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_2");
            } else if (Regex.IsMatch(composition, @".E.EW.E.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_3");
            } else if (Regex.IsMatch(composition, @".E.EE.E.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_4");
            } else if (Regex.IsMatch(composition, @".E.WW.W.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_5");
            } else if (Regex.IsMatch(composition, @".W.WW.W.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_6");
            } else if (Regex.IsMatch(composition, @".W.WW.E.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_7");
            } else if (Regex.IsMatch(composition, @".E.WW.E.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_8");
            } else if (Regex.IsMatch(composition, @".E.EW.W.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_10");
            } else if (Regex.IsMatch(composition, @".W.EW.W.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_11");
            } else if (Regex.IsMatch(composition, @".W.EW.E.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_12");
            } else if (Regex.IsMatch(composition, @".E.EW.E.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_13");
            } else if (Regex.IsMatch(composition, @".E.EE.W.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_15");
            } else if (Regex.IsMatch(composition, @".W.EE.W.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_16");
            } else if (Regex.IsMatch(composition, @".W.EE.E.")) {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_17");
            }
            if (Regex.IsMatch(composition, @"...W.EW.")) {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_18");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            if (Regex.IsMatch(composition, @"EW.W....")) {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_19");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            if (Regex.IsMatch(composition, @"....W.WE")) {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_23");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            if (Regex.IsMatch(composition, @".WE.W...")) {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("Enviroment_24");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
        }
    }

    public string TileCheck(Point currentPoint) {
        string composition = string.Empty;
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x != 0 || y != 0) {
                    if (waterTiles.ContainsKey(new Point(currentPoint.MyX + x, currentPoint.MyY + y))) {
                        composition += "W";
                    } else {
                        composition += "E";
                    }
                }
            }
        }
        return composition;
    }
}

[Serializable]
public class MapElement {
    [SerializeField]
    private string tileTag = null;
    [SerializeField]
    private Color color = Color.black;
    [SerializeField]
    private GameObject elementPrefab = null;
    public GameObject MyElementPrefab {
        get {
            return elementPrefab;
        }
    }
    public Color MyColor {
        get {
            return color;
        }
    }
    public string MyTileTag {
        get {
            return tileTag;
        }
    }

}

public struct Point {
    public int MyX { get; set; }
    public int MyY { get; set; }
    public Point(int x, int y) {
        this.MyX = x;
        this.MyY = y;
    }
}