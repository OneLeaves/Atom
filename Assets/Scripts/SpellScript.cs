using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour {
    private Rigidbody2D mRigidbody;
    [SerializeField]
    private float speed = 0;
    public Transform MyTarget { get; set; }

    // Start is called before the first frame update
    void Start () {
        mRigidbody = GetComponent<Rigidbody2D> ();
    }

    public void Fire () {

    }

    private void FixedUpdate () {
        if (MyTarget != null) {
            Vector2 direction = MyTarget.position - transform.position;
            mRigidbody.velocity = direction.normalized * speed;
            float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
        }

    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "HitBox" && other.transform == MyTarget) {
            GetComponent<Animator> ().SetTrigger ("impact");
            mRigidbody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }
}