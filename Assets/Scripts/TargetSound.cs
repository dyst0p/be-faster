using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSound : MonoBehaviour
{
    public AudioClip[] audioClip;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = audioClip[Random.Range(0, audioClip.Length)];
        GetComponent<AudioSource>().Play();
    }
}