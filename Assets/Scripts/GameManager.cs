using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private Player player = null;
    private NPC currentTarget;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        ClickTarget ();
    }

    private void ClickTarget () {
        if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject()) {
            RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask ("Clickable"));
            if (hit.collider != null) {
                Debug.Log("select");
                if(currentTarget != null){
                    currentTarget.DeSelect();
                }
                currentTarget = hit.collider.GetComponent<NPC>();
                player.MyTarget = currentTarget.Select();
                UIManager.MyInstance.ShowTargetFrame(currentTarget);
            } else {
                Debug.Log("deselect");
                UIManager.MyInstance.HideTargetFrame();
                if(currentTarget != null) {
                    currentTarget.DeSelect();
                }
                currentTarget = null;
                player.MyTarget = null;
            }
        }
    }
}