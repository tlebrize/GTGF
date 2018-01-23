using System.Linq;	
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour {

	public Transform	player;
	
	GameObject			currentTile;
	List<Transform>		road;

	void Build() {
		Debug.Log("Build");
	}

	float DistToPlayer(Transform tile) {
		return Vector3.Distance(tile.position, player.position);
	}

	void UpdateRoad() {
		int destroyed = 0;
		while (road.Count > 0 && DistToPlayer(road[0]) > 6f) {
			Destroy(road[0].gameObject);
			road.RemoveAt(0);
			destroyed++;
		}
		while (destroyed-- > 0)
			Build();
	}

	void Start () {
		List<GameObject> roadGO = new List<GameObject>(GameObject.FindGameObjectsWithTag("road"));
		road = new List<Transform>();
		foreach (GameObject tile in roadGO) {
			road.Add(tile.transform);
		}
		InvokeRepeating("UpdateRoad", 0f, 1f);
	}
}
