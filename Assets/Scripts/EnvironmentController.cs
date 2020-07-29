using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnvironmentController : MonoBehaviour {

    public static EnvironmentController instance = null;

    //Maximaler Faktor, für die Größe der Welt
    public float MaxSize = 10;
    //Minimaler Faktor, für die Größe der Welt
    public float MinSize = 0.1f;
    //Die Beschleunigung des Skalierens
    public float GrowthSteepness = 1f;
    //Die Geschwindigkeit des Skalierens
    public float ScaleSpeed = 0.005f;

    public float SizeFactor;
    public bool UpBlocked;
    public bool DownBlocked;

    public GameObject Player;
    public GameObject Pivot;
    public KeyCube KeyCube;

    private float x;

    void Awake()
    {
        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);

        //Start x berechnen, so dass die Welt ihren ursprünglichen Scale behält
        x = Mathf.Sqrt(Pivot.transform.localScale.x / GrowthSteepness);
    }

    public void Start()
    {
        Scale(0);
    }

    public void UpdateHoldingDistance()
    {
        KeyCube.UpdatedHoldingDistance = KeyCube.InitialHoldingDistance + SizeFactor;
    }

    public void CheckScaleLimits()
    {
        //Überprüfen, ob das Größenlimit der Welt nach oben, oder unten, erreicht wurde.
        if (Pivot.transform.localScale.x < MinSize)
        {
            UpBlocked = true;
        }
        else
        {
            UpBlocked = false;
        }

        if (Pivot.transform.localScale.x > MaxSize)
        {
            DownBlocked = true;
        }
        else
        {
            DownBlocked = false;
        }
    }

    public void UpdateMusicHighpass()
    {
        //Setzt das Frequenz-Limit für den Highpass-Filter der Musik (die Tiefen Töne werden ab einer bestimmten Frequenz herausgefiltert). Funktion ist so gewählt, dass WErte zwischen 10Hz und ca. 900Hz entstehen.
        //Wird leider noch nicht dynamisch aufgrund der MAximalen und Minimalen Größe der Welt berechnet.
        if (SizeFactor <= 0.33)
        {
            AudioManagerScript.Instance.SetMusicHighPass(Mathf.RoundToInt(19 * (1 / SizeFactor) - 47));
        }
    }

    public void Scale(float scaleAmount)
    {
        CheckScaleLimits();

        //Den Pivot der Welt, in Form von Parent Object, beim SPieler platzieren
        this.transform.parent = null;
        Pivot.transform.position = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
        this.transform.parent = Pivot.transform;

        //Skalierung der Welt durchführen
        x += scaleAmount * ScaleSpeed;
        SizeFactor = GrowthSteepness * Mathf.Pow(x, 2);
        Pivot.transform.localScale = new Vector3(1, 1, 1) * SizeFactor;

        UpdateHoldingDistance();
        UpdateMusicHighpass();       
    } 
}
