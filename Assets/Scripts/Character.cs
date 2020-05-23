using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Animator))]
public abstract class Character : MonoBehaviour {
    [SerializeField]
    private float speed = 0;
    protected Animator mAnimator;
    protected Vector2 direction;
    private Rigidbody2D mRigidbody;
    protected bool isAttacking = false;
    protected Coroutine attackRoutine;
    [SerializeField]
    protected Transform hitBox;
    [SerializeField]
    protected Stat health;
    public Stat MyHealth {
        get {return health;}
    }
    [SerializeField]
    private float maxHealth = 100;
    public bool IsMoving {
        get {
            return direction.magnitude > 0.1;
        }
    }
    // Start is called before the first frame update
    protected virtual void Start () {
        mRigidbody = GetComponent<Rigidbody2D> ();
        mAnimator = GetComponent<Animator> ();
        health.Initialize (maxHealth, maxHealth);
    }

    // Update is called once per frame
    protected virtual void Update () {
        HandleLayers ();
    }

    private void FixedUpdate () {
        Move ();
    }

    private void Move () {
        mRigidbody.velocity = direction.normalized * speed;
    }
    private void HandleLayers () {
        if (IsMoving) {
            mAnimator.SetFloat ("dx", direction.x);
            mAnimator.SetFloat ("dy", direction.y);
            ActivateLayer ("WalkLayer");
            StopAttack ();
        } else if (isAttacking) {
            ActivateLayer ("AttackLayer");
        } else {
            ActivateLayer ("IdleLayer");
        }
    }
    public void ActivateLayer (string layerName) {
        for (int i = 0; i < mAnimator.layerCount; i++) {
            mAnimator.SetLayerWeight (i, 0);
        }
        mAnimator.SetLayerWeight (mAnimator.GetLayerIndex (layerName), 1);
    }

    public virtual void StopAttack () {
        if (attackRoutine != null) {
            StopCoroutine (attackRoutine);
            isAttacking = false;
            mAnimator.SetBool ("attack", isAttacking);
        }
    }

    public virtual void TakeDamage (float damage) {
        health.MyCurrentValue -= damage;
        if (health.MyCurrentValue <= 0) {
            mAnimator.SetTrigger("die");
        }
    }

}