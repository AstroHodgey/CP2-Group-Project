using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MagpieSwoop : MonoBehaviour {
    public float magpieMoveSpeed;
    private Vector3 flying;
    
    private bool swooping = false;
    private bool onDescent = false;
    private bool straight = false;
    private bool rising = false;

    public Transform target;
    public Transform[] playerTransforms;
    
    private int randomInt;

    private void Awake() {
        Transform transform1 = GameObject.FindWithTag("Player1").transform;
        Transform transform2 = GameObject.FindWithTag("Player2").transform;
        Transform transform3 = GameObject.FindWithTag("Player3").transform;
        Transform transform4 = GameObject.FindWithTag("Player4").transform;
        
        playerTransforms = new []{transform1, transform2, transform3, transform4};
    }

    void Start()
    {
        randomInt = Random.Range(0, 4);
        
        if (!swooping) {
            flying = new Vector3(-magpieMoveSpeed/500, 0, 0);
        }
    }
    
    void Update() {
        target = playerTransforms[randomInt];

        if (transform.position.x < target.position.x + 12) {
            swooping = true;
        }
        
        transform.position += flying;

        if (swooping && !onDescent) {
            transform.rotation = Quaternion.Euler(0, 0, 45);
            flying.y = -magpieMoveSpeed / 150;
            onDescent = true;
            swooping = false;
        }

        if (onDescent && transform.position.y <= 4) {
            onDescent = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            flying.y = 0;
            straight = true;
            flying.x = -magpieMoveSpeed / 150;
        }

        if (straight == true && transform.position.x <= target.position.x - 4) {
            rising = true;
            flying.x = -magpieMoveSpeed / 500;


        }

        if (rising) {
            transform.rotation = Quaternion.Euler(0, 0, -55);
            flying.y = magpieMoveSpeed / 250;
            if (transform.position.y >= 15) {
                flying.y = 0;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        
        if (transform.position.x <= -10) {
            Destroy(gameObject);
        }
        
    }

    
}
