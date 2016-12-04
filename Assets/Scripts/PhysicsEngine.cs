using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsEngine : MonoBehaviour {

	public float mass = 0;
	public Vector3 velocity = Vector3.zero;
	public List<Vector3> forces = new List<Vector3>();
	public Vector3 netForce = Vector3.zero;


	void FixedUpdate() {
		UpdateVelocity();

		transform.position += velocity * Time.deltaTime;
	}

	private void AddForces() {
		this.netForce = Vector3.zero;

		foreach (Vector3 force in this.forces) {
			this.netForce += force;
		}

		return;
	}

	private void UpdateVelocity() {
		Vector3 acceleration = Vector3.zero;
		AddForces();

		if (mass > 0) {
			acceleration = this.netForce / this.mass;
		}

		this.velocity += acceleration * Time.deltaTime;
	}
}
