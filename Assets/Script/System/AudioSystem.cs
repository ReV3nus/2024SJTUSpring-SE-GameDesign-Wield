using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource FXSource;

    [Header("Audios")]
    public AudioClip LevelUp;
    public AudioClip Select;
    public AudioClip Metal;


    static AudioSystem instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
        BGMSource.Play();
    }
    static public void StopBGM()
    {
        instance.BGMSource.Stop();
    }
    static public void PlayLevelUp()
    {
        instance.FXSource.PlayOneShot(instance.LevelUp);
    }
    static public void PlaySelect()
    {
        instance.FXSource.PlayOneShot(instance.Select);
    }
    static public void PlayMetal()
    {
        instance.FXSource.PlayOneShot(instance.Metal);
    }
}
