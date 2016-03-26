using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    private Rigidbody rb;
    public float Speed;
    public float JumpStrength;
    private bool alive;
    private bool jumping;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        jumping = false;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (rb.velocity.y == 0) {
            jumping = false;
        }

        if (Input.GetKeyDown(KeyCode.W) == true && !jumping)
        {
            Debug.Log("pressed a w");
            jumping = true;
            rb.velocity = new Vector3(moveHorizontal,  JumpStrength, 0);
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        
        rb.MovePosition(transform.position + movement * Speed);
	}
}
