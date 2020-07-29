using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public AudioClip Denied;
    public physicalPlayerController Body;
    public EnvironmentController Level;
    public MeshRenderer ProgressBarUI;
    public Camera cam;

    public float PickUpSpeed;
    public float PickUpRange;
    public float PickUpTimer;

    private bool UpBlocked;
    private bool DownBlocked;
    private float reloadTimer;
    private KeyCube KeyCube;
    private Material ProgressMaterial;

    void Start()
    {
        PickUpTimer = 0;
        ProgressMaterial = ProgressBarUI.GetComponent<Material>();
        Level = EnvironmentController.instance;
        ProgressBarUI.enabled = false;
    }

    void Update()
    {
        Scale();
        PickUp();
        Respawn();
    }

    private void PickUp()
    {
        if (PickUpTimer >= 1)
        {
            ProgressBarUI.material.SetFloat("_ColorRampOffset", 0);
            ProgressBarUI.enabled = false;
            KeyCube.PickedUp();
            return;
        }


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            KeyCube = hit.collider.gameObject.GetComponent<KeyCube>();

            if (KeyCube != null)
            {
                PickUpTimer += PickUpSpeed * Time.deltaTime;
                ProgressBarUI.enabled = true;
                ProgressBarUI.material.SetFloat("_ColorRampOffset", 1 - PickUpTimer);
            }
            else
            {
                if(PickUpTimer != 0)
                {
                    PickUpTimer = 0;
                    ProgressBarUI.material.SetFloat("_ColorRampOffset", 1);
                    ProgressBarUI.enabled = false;
                }
            }
        }
    }

    private void Scale()
    {
        //Überprüfen, ob irgendein Element das Skalieren in eine Richtung verhindert.
        if(Body.UpBlocked || Level.UpBlocked)
        {
            UpBlocked = true;
        }
        else
        {
            UpBlocked = false;
        }

        if(Body.DownBlocked ||Level.DownBlocked)
        {
            DownBlocked = true;
        }
        else
        {
            DownBlocked = false;
        }

        //"Big" vergrößert die Welt
        if (Input.GetButton("Big") && DownBlocked == false)
        {
            EnvironmentController.instance.Scale(1);
        }
        //"Small" verkleinert die Welt
        if (Input.GetButton("Small") && UpBlocked == false)
        {
            EnvironmentController.instance.Scale(-1);
        }
        
        //Sound spielen, falls Skalieren nicht möglich ist.
        if ((Input.GetButtonDown("Small") && UpBlocked) || Input.GetButtonDown("Big") && DownBlocked)
        {
            AudioManagerScript.Instance.PlayAudio(Denied);
        }
    }

    private void Respawn()
    {
        if (Input.GetButton("Respawn"))
        {
            reloadTimer += Time.deltaTime;
            ProgressBarUI.enabled = true;
            ProgressBarUI.material.SetFloat("_ColorRampOffset", 1 - reloadTimer);

            if (reloadTimer >= 1)
            {
                SceneManagerScript.LoadScene(0);
            }
        }
        else
        {
            if(reloadTimer != 0)
            {
                reloadTimer = 0;
                ProgressBarUI.material.SetFloat("_ColorRampOffset", 1);
                ProgressBarUI.enabled = false;
            }
        }
    }
}