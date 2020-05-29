using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private Color fadeColor;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        fadeColor = defaultColor;
        fadeColor.a = 0.7f;
    }

    public void FadeIn() {
        spriteRenderer.color = fadeColor;
    }
    public void FadeOut() {
        spriteRenderer.color = defaultColor;
    }
}
