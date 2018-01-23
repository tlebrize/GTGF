using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour {

	LineRenderer 		lineRenderer;
	int					turnStep = 0;
	int					turnDirection = 0;
	int					turnSize = 2;
	float				color = 0.1f;
	int					colorSign = 1;

	public Transform 	player;
	public int			lineLength;
	public int			strechSize;
	public int			trailSize;
	public int			turnRange;
	public Vector2		turnSizeRange;

	bool LastNodeTooFar() {
		Vector3	position = lineRenderer.GetPosition(0);
		float distance = Vector3.Distance(position, player.position);
		return distance > trailSize;
	}

	void PushOneNode(int node) {
		Vector3 nextNode = lineRenderer.GetPosition(node + 1);
		lineRenderer.SetPosition(node, nextNode);
	}

	int FindTurn() {
		turnStep++;
		if (turnStep == turnSize) {
			turnStep = 1;
			turnDirection = Random.Range(-turnRange, turnRange+1);
			turnSize = Random.Range((int)turnSizeRange[0], (int)turnSizeRange[1]);
		}
		return turnStep*turnDirection;
	}

	void PushAllNodes() {
		for (int i = 0 ; i < lineRenderer.positionCount - 1 ; i++)
			PushOneNode(i);
		Vector3 headNode = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
		headNode.y += strechSize;
		headNode.x += FindTurn();
		lineRenderer.SetPosition(lineRenderer.positionCount - 1, headNode);
	}

	void Start() {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = lineLength;
		int i = 0;
		Vector3 previousNode = new Vector3(0, 0, 0);
		while (i++ < lineRenderer.positionCount - 1) {
			lineRenderer.SetPosition(i, new Vector3(previousNode.x + FindTurn(), i*strechSize, 0));
			previousNode = lineRenderer.GetPosition(i);
		}
	}
	
	void UpdateColor () {
		color += 0.2f * Time.deltaTime * colorSign;
		if (color > 0.9f)
			colorSign *= -1;
		else if (color < 0.1f)
			colorSign *= -1;
		lineRenderer.startColor = new Color(1-color, 0.5f, color);
		lineRenderer.endColor = new Color(1-color, 0.5f, color);
	}

	void Update() {
		if (LastNodeTooFar())
			PushAllNodes();
		UpdateColor();
	}

}

