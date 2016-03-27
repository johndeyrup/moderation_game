using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    private Rigidbody rb;
    public float Speed;
    public float JumpStrength;
    private bool alive;
    private bool jumping;
    public int JumpCollectables;
    public int SpeedCollectables;
    public GameObject heroPrefab;
    private Vector3 checkpoint;
    private Camera cam;
    private Vector3 cameraOffset;
    private CapsuleCollider cap;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        JumpStrength += JumpCollectables;
        jumping = false;
        checkpoint = new Vector3(-15f, 1f, 0.0f);
        cam = Camera.main;
        cameraOffset = new Vector3(0.0f, 0.0f, -6.0f);
        cap = GetComponent<CapsuleCollider>();

    }

    void OnTriggerEnter (Collider other)
    {
        Debug.Log(other.gameObject.tag);
        string objTag = other.gameObject.tag;
        if (objTag == "death_obj")
        {            
            Instantiate(heroPrefab, checkpoint, Quaternion.identity);
            Destroy(gameObject);
            
        }
        else if ( objTag == "spring_sprung")
        {
            Destroy(other.gameObject);
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
    }

	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        RaycastHit hit;
        if (Physics.SphereCast(rb.transform.position,  cap.radius/2, (-transform.up), out hit, .3f)) {
            jumping = false;
        }

        if (Input.GetKeyDown(KeyCode.W) == true && !jumping)
        {
            jumping = true;
            rb.velocity = new Vector3(moveHorizontal,  JumpStrength, 0);
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        
        rb.MovePosition(transform.position + movement * Speed);
        cam.transform.position = rb.position + cameraOffset;
	}
}
