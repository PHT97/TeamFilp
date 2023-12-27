using System;
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
    float check_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 제한 시간을 감소하도록 변경할 경우
        if (check_time - GameManager.I.gameTime >= 5.0f)
        {
            check_time = 0.0f;
            GameManager.I.firstCard.GetComponent<card>().closeCard();
            GameManager.I.firstCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(140 / 255f, 140 / 255f, 160 / 255f, 255 / 255f);

            GameManager.I.firstCard = null;
        }
    }

    public void openCard()
    {
        // 카드가 2장 뒤집혀 있는 동안 다른 카드들 클릭 막기
        if (GameManager.I.cardCounter > 1) return;

        if (GameManager.I.firstCard == null)
        {
            openEffect();
            GameManager.I.firstCard = gameObject;

            // 첫 번째 카드가 뒤집힌 시간 저장하기
            check_time = GameManager.I.gameTime;
        } else
        {
            // 첫번째카드오브젝트가 다를때만
            if(GameManager.I.firstCard != gameObject)
            {
                openEffect();
                GameManager.I.secondCard = gameObject;
                GameManager.I.isMatched();
            }
        }
    }

    private void openEffect()
    {
        GameManager.I.cardCounter++; // cardCounter : 현재 게임에 뒤집혀 있는 카드 개수 카운트
        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", true);
        Invoke("openCardInvoke", 0.2f); // 0.2�� �� ����
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
        check_time = 0.0f;
    }

    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f);
    }

    void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("front").gameObject.SetActive(false);
        transform.Find("back").gameObject.SetActive(true);
        
        GameManager.I.cardCounter = 0;
        check_time = 0.0f;
    }
}
