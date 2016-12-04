using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Version 1 of this simple component to draw lines representing force vectors
// Think of them as rocket trails if you like

[DisallowMultipleComponent]
[RequireComponent (typeof(PhysicsEngine))]
public class DrawForces : MonoBehaviour {

	public bool showTrails = true;
	public bool showNetForce = true;

	private List<Vector3> forceVectorList = new List<Vector3>();
	private LineRenderer lineRenderer;
	private LineRenderer netLineRenderer;
	private Vector3 netForce = Vector3.zero;
	private PhysicsEngine physicsEngine;

	// Use this for initialization
	void Start () {
		physicsEngine = GetComponent<PhysicsEngine>();

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		lineRenderer.startWidth = 0.2f;
		lineRenderer.endWidth = 0.2f;
		lineRenderer.useWorldSpace = false;
		lineRenderer.startColor = Color.yellow;
		lineRenderer.endColor = Color.yellow;

		netLineRenderer = gameObject.AddComponent<LineRenderer>();
		netLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		netLineRenderer.startWidth = 0.2f;
		netLineRenderer.endWidth = 0.2f;
		netLineRenderer.useWorldSpace = false;
		netLineRenderer.startColor = Color.red;
		netLineRenderer.endColor = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		forceVectorList = physicsEngine.forces;

		if (showTrails) {
			lineRenderer.enabled = true;
			lineRenderer.numPositions = showTrails ? (forceVectorList.Count * 2) : 0;
Debug.Log(lineRenderer.numPositions);
			int i = 0;
			foreach (Vector3 forceVector in forceVectorList) {
				lineRenderer.SetPosition(i, Vector3.zero);
				lineRenderer.SetPosition(i+1, -forceVector);
				Debug.Log("Position: " + (i+1) + ": " + lineRenderer.GetPosition(i+1));
				i = i + 2;
			}
		} 
		else {
			lineRenderer.enabled = false;
		}

		if (showNetForce) {
			netLineRenderer.enabled = true;
			netLineRenderer.numPositions = showNetForce ? 2 : 0;
Debug.Log(netLineRenderer.numPositions);
			netForce = physicsEngine.netForce;
			netLineRenderer.SetPosition(0, Vector3.zero);
			netLineRenderer.SetPosition(1, -netForce);
			Debug.Log(netLineRenderer.GetPosition(1) + " netForce: " + netForce);
		}
		else {
			netLineRenderer.enabled = false;
		}
	}
}
 