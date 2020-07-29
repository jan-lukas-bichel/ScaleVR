using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerScript : MonoBehaviour {

    public AudioSource AudioSource;
    public AudioMixer AudioMixer;

    private static AudioManagerScript instance = null;
    public static AudioManagerScript Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static void PlayAudioAtPoint(GameObject targetObject, AudioClip audioClip)
    {
        AudioSource.PlayClipAtPoint(audioClip, targetObject.transform.position);
    }

    public void PlayAudio (AudioClip audioClip)
    {
        AudioSource.clip = audioClip;
        AudioSource.Play();
    }

    public void SetMusicHighPass (int cutoffFreq)
    {
        AudioMixer.SetFloat("MusicCutoffFreq", cutoffFreq);
    }
}
