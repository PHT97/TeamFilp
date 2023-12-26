using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;

    public AudioClip flip;
    public AudioSource audioSource;

    public string cardName;

    // 첫 번째 카드 뒤집힌 시간 저장
    float check_time = 999.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 첫 번째 카드가 뒤집힌지 5초가 지날 경우
        if (GameManager.I.time - check_time >= 5.0f)
        {
            check_time = 999.0f;
            GameManager.I.firstCard.GetComponent<card>().closeCard();   // 첫 번째 카드 다시 뒤집기

            GameManager.I.firstCard = null;     // 첫 번째 카드 초기화
        }
    }

    public void openCard()
    {
        // 카드가 2장 뒤집혀 있는 동안 다른 카드들 클릭 막기
        if (GameManager.I.cardCounter > 1) return;

        GameManager.I.cardCounter++;

        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", true);
        Invoke("openCardInvoke", 0.2f); // 0.2초 후 실행

        if (GameManager.I.firstCard == null)
        {
            GameManager.I.firstCard = gameObject;

            // 첫 번째 카드가 뒤집힌 시간 저장하기
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
