using UnityEngine;
using System.Collections;

public class Gear : MonoBehaviour {

	[SerializeField]
	public float RotSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		//this.transform.Rotate(new Vector3(0, 0, RotSpeed));
		Rigidbody rb = this.GetComponent<Rigidbody>();
		rb.MoveRotation(rb.rotation * Quaternion.Euler(0, 0, RotSpeed));
	}
}
