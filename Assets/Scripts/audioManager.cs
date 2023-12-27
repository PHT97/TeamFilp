using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager I;
    public AudioClip bgmusic;
    public AudioSource audioSource;
    float newSpeed = 1.2f; // 배경음의 배속 속도

    private void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = bgmusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.I.time < 20f)
        {
            audioSource.GetComponent<AudioSource>().pitch = newSpeed; // 오디오소스의 속도를 새로운 속도로하기
        }
    }
}
