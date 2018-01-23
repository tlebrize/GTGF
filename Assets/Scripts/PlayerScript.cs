using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	CarScript					car;
	
	void Start() {
		car = gameObject.GetComponent<CarScript>();
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
	}
}
