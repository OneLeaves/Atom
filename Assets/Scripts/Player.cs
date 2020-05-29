using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [SerializeField]
    private Stat mana = null;
    private float maxMana = 48;
    [SerializeField]
    private Transform[] exitPoints = null;
    private int exitIndex;
    private Vector3[] faceVector = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
    private Transform target;
    public Transform MyTarget {
        get;
        set;
    }
    private SpellBook spellBook;

    protected override void Start () {
        spellBook = GetComponent<SpellBook> ();
        mana.Initialize (maxMana, maxMana);
        base.Start ();
    }

    protected override void Update () {
        GetInput ();
        base.Update ();
    }

    private void GetInput () {

        if (Input.GetKeyDown (KeyCode.I)) {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown (KeyCode.O)) {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }

        direction = Vector2.zero;
        if (Input.GetKey (KeyCode.W)) {
            exitIndex = 0;
            direction += Vector2.up;
        }
        if (Input.GetKey (KeyCode.S)) {
            exitIndex = 1;
            direction += Vector2.down;
        }
        if (Input.GetKey (KeyCode.A)) {
            exitIndex = 2;
            direction += Vector2.left;
        }
        if (Input.GetKey (KeyCode.D)) {
            exitIndex = 3;
            direction += Vector2.right;
        }
    }

    private IEnumerator Attack (int spellIndex) {
        Transform currentTarget = MyTarget; // 防止在施法过程中改变目标
        Spell newSpell = spellBook.CastSpell (spellIndex);
        isAttacking = true;
        mAnimator.SetBool ("attack", isAttacking);
        yield return new WaitForSeconds (newSpell.MyCastTime);
        Debug.Log ("create" + spellIndex);
        if (currentTarget != null) {
            SpellScript s = Instantiate (newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript> ();
            s.Initialize(currentTarget, newSpell.MyDamage);
        }
        StopAttack ();
    }

    public void CastSpell (int spellIndex) {
        if (MyTarget != null && !isAttacking && !IsMoving && InLineOfSinght ()) {
            Debug.Log ("attack" + spellIndex);
            attackRoutine = StartCoroutine (Attack (spellIndex));
        }
    }

    private bool InLineOfSinght () {
        Vector3 direction = (MyTarget.position - transform.position).normalized;
        Debug.DrawRay (transform.position, direction, Color.red);
        Debug.DrawRay (transform.position, faceVector[exitIndex], Color.green);
        return (Vector3.Angle (direction, faceVector[exitIndex])) < 45;
    }

    public override void StopAttack () {
        spellBook.StopCasting ();
        base.StopAttack ();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Obstacle") {
            other.GetComponent<Obstacle>().FadeOut();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Obstacle") {
            other.GetComponent<Obstacle>().FadeIn();
        }
    }
}