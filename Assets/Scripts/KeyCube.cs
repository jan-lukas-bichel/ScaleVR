using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyCube : MonoBehaviour {


    public float MovingForce;
    public float InitialHoldingDistance;
    public float UpdatedHoldingDistance;
    public float Friction;

    public AudioClip PickUpSound;
    public AudioClip LevelFinished;
    public AudioClip CubeDropped;

    public Material Solid;
	public Material Transparent;
	public Renderer Renderer;
	public Camera Cam;

	private bool JustPickedUp;
	private playerController Player;
    private Rigidbody RigidBody;

    void Start()
    {
		JustPickedUp = false;
		Player = GameObject.FindWithTag("Player").GetComponent<playerController>();
		Renderer = GetComponent<Renderer> ();
        RigidBody = GetComponent<Rigidbody>();
        MovingForce = 20f;
        InitialHoldingDistance = 0.5f;
        Friction = 0.95f;
    }

    public void PickedUp()
    {
		Vector3 _targetPosition;
		_targetPosition = Cam.transform.position + (Cam.transform.forward * EnvironmentController.instance.SizeFactor + UpdatedHoldingDistance * Cam.transform.forward);
        RigidBody.velocity = RigidBody.velocity * Friction;
        RigidBody.AddForce((_targetPosition - transform.position) *Vector3.Distance(_targetPosition, transform.position) * MovingForce);

		//Einmal beim Aufheben ausführen
		if (JustPickedUp == false) {
            RigidBody.useGravity = false;
            Renderer.material = Transparent;
            AudioManagerScript.PlayAudioAtPoint(gameObject, PickUpSound);
            EnvironmentController.instance.UpdateHoldingDistance();
			JustPickedUp = true;
		}

    }

    private void OnCollisionEnter(Collision _collision)
    {
        //Wenn Kollision mit Energie-Feld stattfindet
		if(_collision.gameObject.CompareTag("EnergyField"))
        {
            AudioManagerScript.PlayAudioAtPoint(gameObject, CubeDropped);
			JustPickedUp = false;
            RigidBody.useGravity = true;
            Player.PickUpTimer = 0f;
			Renderer.material = Solid;
        }

        //Wenn Kollision mit dem Ziel stattfindet
        else if (_collision.gameObject.CompareTag("Goal"))
		{
            AudioManagerScript.Instance.PlayAudio(LevelFinished);
            SceneManagerScript.LoadScene(1);
        }
    }
}
