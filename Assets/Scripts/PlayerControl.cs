using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    private Rigidbody rb;
    public float Speed;
    public float JumpHeight;
    private bool alive;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        Debug.Log(rb.position);
        rb.MovePosition(transform.position + movement * Speed);
	}
}
