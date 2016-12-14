using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

	public GameObject ballPrefab;
	public AudioClip launchClip, windupClip;
	public float maxThrust = 20f;

	private AudioSource audioSource;
	private float thrust = 0f;
	private float thrustIncrement;

	private UniversalGravitation gravity;

	// Use this for initialization
	void Start () {
		this.audioSource = GetComponent<AudioSource>();
		this.audioSource.clip = this.windupClip;
		this.thrustIncrement = (this.maxThrust * Time.fixedDeltaTime) / this.audioSource.clip.length;
		this.gravity = FindObjectOfType<UniversalGravitation>();
		Debug.Log(this.maxThrust + " * " + Time.fixedDeltaTime + " / " + this.audioSource.clip.length + " = " + this.thrustIncrement);
	}
	
	void OnMouseDown() {
		this.thrust = 0f;
		audioSource.clip = windupClip;
		audioSource.Play();

		InvokeRepeating("IncreaseThrust", 0f, Time.fixedDeltaTime);
	}

	void OnMouseUp() {
		CancelInvoke();
		LaunchBall();
		Debug.Log("Launching!");
	}

	private void IncreaseThrust() {
		this.thrust = Mathf.Clamp(this.thrust + this.thrustIncrement, 0.2f, this.maxThrust);
		Debug.Log("Increasing thrust to " + this.thrust + "!");
	}

	private void LaunchBall() {
		Vector3 thrustUnitVector = new Vector3(1f, 1f, 0f);
		GameObject ball = Instantiate(this.ballPrefab);
		PhysicsEngine physicsEngine = ball.GetComponent<PhysicsEngine>();
		ball.transform.parent = GameObject.Find("Balls").transform;
		ball.transform.position = gameObject.transform.position;

		audioSource.Stop();
		audioSource.clip = launchClip;
		audioSource.Play();

		physicsEngine.velocity = this.thrust * thrustUnitVector;
		gravity.AddGravitationalBody(physicsEngine);
	}
}
