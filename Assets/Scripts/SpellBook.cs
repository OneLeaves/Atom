using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour {
    [SerializeField]
    private Image castingBar = null;
    [SerializeField]
    private Spell[] spells = null;
    [SerializeField]
    private Text castTime = null;
    [SerializeField]
    private CanvasGroup canvasGroup = null;
    private Coroutine spellRoutine;
    private Coroutine fadeRoutine;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public Spell CastSpell (int index) {
        castingBar.fillAmount = 0;
        spellRoutine = StartCoroutine (Progress (index));
        fadeRoutine = StartCoroutine (FadeBar ());
        return spells[index];
    }

    private IEnumerator Progress (int index) {
        float timeLeft = spells[index].MyCastTime;
        float rate = 1f / spells[index].MyCastTime;
        float progress = 0f;
        while (progress <= 1.0) {
            castingBar.fillAmount = Mathf.Lerp (0, 1, progress);
            progress += rate * Time.deltaTime;
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0) {
                timeLeft = 0;
            }
            castTime.text = timeLeft.ToString ("F1");
            yield return null;
        }
        StopCasting ();
    }

    private IEnumerator FadeBar () {
        float rate = 1f / 0.5f;
        float progress = 0f;
        while (progress <= 1.0) {
            canvasGroup.alpha = Mathf.Lerp (0, 1, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }
    }
    public void StopCasting () {
        if (fadeRoutine != null) {
            StopCoroutine (fadeRoutine);
            canvasGroup.alpha = 0;
            fadeRoutine = null;
        }
        if (spellRoutine != null) {
            StopCoroutine (spellRoutine);
            spellRoutine = null;
        }
    }
}