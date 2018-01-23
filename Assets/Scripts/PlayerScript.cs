using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	CarScript			car;
	Camera				mainCamera;
	
	[HideInInspector]
	public GameObject 	currentTile;

	void OnTriggerEnter2d(Collider2D collider) {
		if (collider.gameObject.tag == "road")
			currentTile = collider.gameObject;
	}

	void Start() {
		car = gameObject.GetComponent<CarScript>();
		mainCamera = Camera.main;
	}

	void Update () {
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");
		if (vertical > 0)
			car.inputQueue.Enqueue(CarScript.Behaviours.Accelerate);
		if (vertical < 0)
			car.inputQueue.Enqueue(CarScript.Behaviours.Brake);
		if (horizontal > 0)
			car.inputQueue.Enqueue(CarScript.Behaviours.Left);
		if (horizontal < 0)
			car.inputQueue.Enqueue(CarScript.Behaviours.Right);		
		mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
	}
}
