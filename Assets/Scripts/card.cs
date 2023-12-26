using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;

    public AudioClip flip;
    public AudioSource audioSource;

    public string cardName;

    // ù ��° ī�� ������ �ð� ����
    float check_time = 999.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ù ��° ī�尡 �������� 5�ʰ� ���� ���
        if (GameManager.I.time - check_time >= 5.0f)
        {
            check_time = 999.0f;
            GameManager.I.firstCard.GetComponent<card>().closeCard();   // ù ��° ī�� �ٽ� ������

            GameManager.I.firstCard = null;     // ù ��° ī�� �ʱ�ȭ
        }
    }

    public void openCard()
    {
        // ī�尡 2�� ������ �ִ� ���� �ٸ� ī��� Ŭ�� ����
        if (GameManager.I.cardCounter > 1) return;

        GameManager.I.cardCounter++;

        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", true);
        Invoke("openCardInvoke", 0.2f); // 0.2�� �� ����

        if (GameManager.I.firstCard == null)
        {
            GameManager.I.firstCard = gameObject;

            // ù ��° ī�尡 ������ �ð� �����ϱ�
            check_time = GameManager.I.time;
        } else
        {
            GameManager.I.secondCard = gameObject;
            GameManager.I.isMatched();
        }
    }

    void openCardInvoke()
    {
        transform.Find("back").gameObject.SetActive(false);
        transform.Find("front").gameObject.SetActive(true);
    }

    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 1.0f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);

        GameManager.I.cardCounter = 0;
        check_time= 999.0f;
    }

    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f);
    }

    void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);

        GameManager.I.cardCounter = 0;
        check_time = 999.0f;
    }
}
