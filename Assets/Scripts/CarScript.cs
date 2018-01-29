using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {

	private Rigidbody2D			rb2d;
	private float				steeringDirection;
	private float 				currentAcceleration;

	[HideInInspector]
	public Queue<Behaviours>	inputQueue;
	[HideInInspector]
	public enum					Behaviours {Accelerate, Brake, Left, Right, Boost};

	public float				boost;
	public float 				accelerationSpeed;
	public float 				brakingSpeed;
	public float				steering;	

	void Start() {
		inputQueue = new Queue<Behaviours>();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Accelerate() {
		Debug.Log("Accelerate "+(currentAcceleration>accelerationSpeed));
		float currentBoost = 0;
		Vector3 velocity = transform.up * currentAcceleration / 60f;
		rb2d.AddForce(velocity, ForceMode2D.Impulse);
		currentAcceleration = accelerationSpeed;
	}

	void Brake() {
		Vector3 velocity = -transform.up * brakingSpeed / 60f;
		rb2d.AddForce(velocity, ForceMode2D.Impulse);
	}

	void Steer() {
		rb2d.rotation += steeringDirection * steering * (rb2d.velocity.magnitude / 5.0f);
	}

	void ApplyInput(Behaviours behaviour) {
		if (behaviour == Behaviours.Boost) {
			Debug.Log("boost");
			currentAcceleration = boost * accelerationSpeed;
		}
		else if (behaviour == Behaviours.Accelerate)
			Accelerate();
		else if (behaviour == Behaviours.Brake)
			Brake();
		else if (behaviour == Behaviours.Left) {
			steeringDirection = -1;
			Steer();
		} else if (behaviour == Behaviours.Right) {
			steeringDirection = 1;
			Steer();
		}
	}

	void FixedUpdate() {
		while (inputQueue.Count > 0)
			ApplyInput(inputQueue.Dequeue());
	}
}
