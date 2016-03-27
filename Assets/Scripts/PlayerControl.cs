using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    private Rigidbody rb;
    public float Speed;
    public float JumpStrength;
    private bool alive;
    private bool jumping;
    private bool grounded;
    public int JumpCollectables;
    public int SpeedCollectables;
    private Vector3 checkpoint;
    private Camera cam;
    private Vector3 cameraOffset;
    private CapsuleCollider cap;
    private SpriteAnimator anim;
    private Canvas can;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        JumpStrength += JumpCollectables;
        jumping = false;
        checkpoint = new Vector3(-15f, 1f, 0.0f);
        cam = Camera.main;
        cameraOffset = new Vector3(0.0f, 0.0f, -6.0f);
        cap = GetComponent<CapsuleCollider>();
        anim = GetComponent<SpriteAnimator>();
        can = this.gameObject.transform.GetChild(0).GetComponent<Canvas>();


    }

    void OnTriggerEnter (Collider other)
    {
        Debug.Log(other.gameObject.tag);
        string objTag = other.gameObject.tag;
        if (objTag == "death_obj")
        {
            rb.transform.position = checkpoint;
            
        }
        else if ( objTag == "spring_sprung")
        {
            JumpStrength++;
            JumpCollectables++;
        }
        else if (objTag == "oil_wriggle")
        {
            Destroy(other.gameObject);
            Speed++;
            SpeedCollectables++;
        }
        else if (objTag =="Respawn")
        {
            checkpoint = other.transform.position;

        }
        else if (objTag =="Finish")
        {
            can.enabled = true;
        }
    }

	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal")*Speed;
        RaycastHit hit;
        if (Physics.SphereCast(rb.transform.position,  cap.radius/2, (-transform.up), out hit, .4f)) {
            jumping = false;
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.W) == true && !jumping && grounded)
        {
            jumping = true;
            rb.velocity = new Vector3(moveHorizontal,  JumpStrength, 0);
        }
        else
        {
            rb.velocity = new Vector3(moveHorizontal, rb.velocity.y, 0);
        }
        if (jumping == true)
        {
            anim.SetCurrentAnimation("jumping");
        }
        else if (rb.velocity.x != 0)
        {
            anim.SetCurrentAnimation("walking");
        }
        else
        {
            anim.SetCurrentAnimation("idle");
        }

        
        cam.transform.position = rb.position + cameraOffset;
	}
}
