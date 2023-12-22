using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioClip bgmusic;
    public AudioSource audioSource;
    float newSpeed = 1.2f; // ������� ��� �ӵ�
    float time = 0.0f; // �ð���

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = bgmusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 20f)
        {
            audioSource.GetComponent<AudioSource>().pitch = newSpeed; // ������ҽ��� �ӵ��� ���ο� �ӵ����ϱ�
        }
    }
}
