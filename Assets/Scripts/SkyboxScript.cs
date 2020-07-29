using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxScript : MonoBehaviour {

    public float Speed = 1;
	
	void Update ()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * Speed);
    }
}
