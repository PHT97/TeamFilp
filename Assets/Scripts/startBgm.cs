using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startBgm : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip startbgm;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = startbgm;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
