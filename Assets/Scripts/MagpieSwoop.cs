using System;
using UnityEngine;
using UnityEngine.UI;

public class MagpieSwoop : MonoBehaviour {
    public float magpieMoveSpeed;
    private Vector3 flying;
    public Animator animator;

    public float lowPointX;
    
    private bool swooping = false;
    private bool onDescent = false;
    private bool rising = false;

    public Transform target;
    void Start()
    {
        if (!swooping) {
            flying = new Vector3(-magpieMoveSpeed/500, 0, 0);

        }
    }
    
    void Update() {

        if (transform.position.x < target.position.x + 12) {
            swooping = true;
            animator.SetBool("swooping", true);
        }
        
        transform.position += flying;

        if (swooping && !onDescent) {
            //transform.rotation = Quaternion.Euler(45, 90, -90);
            //transform.Rotate(45, 0, 0);
            //flying.y = -magpieMoveSpeed / 250;
            onDescent = true;
        }

        if (onDescent && transform.position.x <= lowPointX) {
            onDescent = false;
            rising = true;
        }

        if (rising) {
            //transform.rotation = Quaternion.Euler(135, 90, -90);
            //flying.y = magpieMoveSpeed / 250;
        }
        
    }
}
