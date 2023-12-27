using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager I;
    public AudioClip bgmusic;
    public AudioSource audioSource;
    float newSpeed = 1.2f; // ������� ��� �ӵ�

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
            audioSource.GetComponent<AudioSource>().pitch = newSpeed; // ������ҽ��� �ӵ��� ���ο� �ӵ����ϱ�
        }
    }
}
