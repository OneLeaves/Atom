using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [SerializeField]
    private Stat health = null;
    [SerializeField]
    private Stat mana = null;
    private float maxHealth = 100;
    private float maxMana = 50;
    [SerializeField]
    private GameObject[] spellPrefab = null;
    [SerializeField]
    private Transform[] exitPoints = null;
    private int exitIndex;
    private Transform target;

    protected override void Start () {
        health.Initialize (maxHealth, maxHealth);
        mana.Initialize (maxMana, maxMana);
        target = GameObject.Find ("Target").transform;
        base.Start ();
    }

    protected override void Update () {
        GetInput ();
        InLineOfSinght ();
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
        if (Input.GetKeyDown (KeyCode.Space)) {
            if (!isAttacking && !IsMoving) {
                attackRoutine = StartCoroutine (Attack ());
            }
        }
    }

    private IEnumerator Attack () {

        isAttacking = true;
        mAnimator.SetBool ("attack", isAttacking);
        yield return new WaitForSeconds (1);
        CastSpell ();
        StopAttack ();
    }

    public void CastSpell () {
        Instantiate (spellPrefab[0], exitPoints[exitIndex].position, Quaternion.identity);
    }

    private bool InLineOfSinght () {
        Vector3 direction = (target.position - transform.position).normalized;
        Debug.DrawRay (transform.position, direction, Color.red);
        return false;
    }
}