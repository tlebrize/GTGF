using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadTrigger : MonoBehaviour {

	public LineRenderer road;
	private CapsuleCollider2D collider;

	void Update () {
		collider = GetComponent<CapsuleCollider2D>();
		Vector3 roadEnd = road.GetPosition(1);
		Quaternion rotation = Quaternion.FromToRotation(Vector3.up, roadEnd - transform.position);
		transform.rotation = rotation;
	}
	

}
