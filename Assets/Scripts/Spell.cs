using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Spell {
    [SerializeField]
    private string name = null;
    [SerializeField]
    private int damage = 0;
    [SerializeField]
    private Sprite icon = null;
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private float castTime = 0;
    [SerializeField]
    private GameObject spellPrefab = null;
    [SerializeField]
    private Color barColor = Color.black;
    public string MyName {
        get { return name; }
    }
    public int MyDamage {
        get { return damage; }
    }
    public float MySpeed {
        get { return speed; }
    }
    public float MyCastTime {
        get { return castTime; }
    }
    public GameObject MySpellPrefab {
        get { return spellPrefab; }
    }

}