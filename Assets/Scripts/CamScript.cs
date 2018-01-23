using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

	float color = 0.1f;
	int sign = 1;

	void Start () {
		
	}
	
	void Update () {
		color += 0.2f * Time.deltaTime * sign;
		if (color > 0.9f)
			sign *= -1;
		else if (color < 0.1f)
			sign *= -1;
		Camera.main.backgroundColor = new Color(1-color, color, 0.5f);

	}
}
