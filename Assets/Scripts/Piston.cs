using UnityEngine;
using System.Collections;

public class Piston : MonoBehaviour {

	[SerializeField]
	public float HeightToMove = 0f;
	[SerializeField]
	public float Speed = 0f;
	[SerializeField]
	public float InitialOffset = 0f;

	private Vector3 startPosition;
	private Rigidbody rb;

	// Use this for initialization
	void Start (){
		rb = this.GetComponent<Rigidbody>();
		startPosition = rb.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		rb.MovePosition(startPosition + new Vector3(0, HeightToMove * Mathf.Sin(Mathf.PI * (Time.time * Speed + InitialOffset)), 0));
	}
}
