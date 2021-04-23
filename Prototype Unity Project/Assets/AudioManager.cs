using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource menuAudio;
    public static float vol = 1f;

    // Start is called before the first frame update
    void Start()
    {
        menuAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        menuAudio.volume = vol;
    }

    public void SetVolume(float volume) 
    {
        vol = volume;
    }
}
