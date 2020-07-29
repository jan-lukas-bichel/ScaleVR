using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicalPlayerController : MonoBehaviour {

    //Die Welt zu Verkleinern wird blockiert
    public bool UpBlocked = false;
    //Die Welt zu vergrößern wird blockiert
    public bool DownBlocked = false;
	public GameObject CenterEyeAnchor;

    private CapsuleCollider playerCollider;
    private float playerPivotHeight;
    private float playerColliderToGroundDistance;

    private void Start()
    {
        playerColliderToGroundDistance = 0.5f;
        playerCollider = GetComponent<CapsuleCollider>();
        playerPivotHeight = playerCollider.height / 2;
    }

    void Update()
	{
		transform.position = new Vector3(CenterEyeAnchor.transform.position.x, playerColliderToGroundDistance, CenterEyeAnchor.transform.position.z);
	}

	void OnCollisionStay(Collision collision)
	{
        float ObjectPivotHeight = collision.gameObject.transform.position.y;

        if (ObjectPivotHeight < playerPivotHeight)
        {
            DownBlocked = true;
        }
        if (ObjectPivotHeight > playerPivotHeight)
        {
            UpBlocked = true;
        }
    }
	void OnCollisionExit(Collision collision)
	{
        UpBlocked = false;
        DownBlocked = false;
    }
		
}
