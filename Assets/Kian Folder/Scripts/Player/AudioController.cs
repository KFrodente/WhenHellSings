using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip clip, AudioSource source, float pitch = 1)
    {
        source.pitch = pitch;
        source.PlayOneShot(clip);
    }
}
