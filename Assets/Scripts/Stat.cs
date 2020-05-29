using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour {

    private Image content; // 生命条图片
    [SerializeField]
    private Text statValue = null;
    private float lerpSpeed = 5;
    private float currentFill;
    public float MyMaxValue { get; set; }
    private float currentValue;
    public float MyCurrentValue {
        get {
            return currentValue;
        }
        set {
            if (value > MyMaxValue) {
                currentValue = MyMaxValue;
            } else if (value < 0) {
                currentValue = 0;
            } else {
                currentValue = value;
            }
            currentFill = currentValue / MyMaxValue;
            if (statValue != null) {
                statValue.text = MyCurrentValue + "";
            }

        }
    }

    // Start is called before the first frame update
    void Start () {
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {
        if (currentFill != content.fillAmount) {
            content.fillAmount = Mathf.Lerp (content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }

    }

    public void Initialize (float currentValue, float maxValue) {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
        if(content == null) {
            content = GetComponent<Image>();
        }
        content.fillAmount = MyCurrentValue / MyMaxValue;
    }
}