using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

// AS1234
public class GameManager : MonoBehaviour
{
    public Text timeTxt;
    public float time;
    Dictionary<GameObject, Vector3> cardList = new Dictionary<GameObject, Vector3>();
    bool isCardGenerated;

    public GameObject matchTxt;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endTxt;
    public GameObject audioManager;

    public AudioClip match;
    public AudioClip fail;
    public AudioSource audioSource;

    public Sprite[] sprites;

    // 분
    int min;
    // 초
    float sec;

    // 결과창
    public GameObject endPanel;
    // 카드 매칭 시도 횟수 카운터
    public int counter = 0;
    // 카드 매칭 시도 횟수 Text
    public Text count;
    // 현재 게임에 뒤집힌 카드 수
    public int cardCounter = 0;
    // 점수 표시 Text
    public Text scoreTxt;
    // 점수
    public int score = 30;      // 기본 점수
    // 틀릴 경우 시간 감소가 되는 time과 별개의 시간
    // 첫 번째 카드가 뒤집히고 나서 5초 뒤를 카운트하기 위한 시간입니다.
    public float gameTime;
    

    public static GameManager I;

    void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // startBtn으로 넘어온 typecase 값에따라 시간을 조절하는 if문
        if (startBtn.typecase == 0)
        {
            time = 120.0f;
        }
        else if (startBtn.typecase == 1)
        {
            time = 90.0f;
        }
        else if (startBtn.typecase == 2)
        {
            time = 60.0f;
        }

        gameTime = time;

        Time.timeScale = 1.0f;
        audioManager.SetActive(true);

        isCardGenerated = false;
        generateCard();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCardGenerated)
        {
            StartCoroutine(moveCard(0.03f));
        }

        if(isCardGenerated)
        {
            //시간이 감소
            time -= Time.deltaTime;
            gameTime -= Time.deltaTime;
            //전체 시간이 60초보다 클때
            if (time >= 60.0f)
            {
                min = (int)time / 60;
                sec = time % 60;
                timeTxt.text = min + ":" + (int)sec;
            }
            //전체시간이 60초보다 작을때
            if(time <= 60.0f)
            {
                min = 0;
                sec = time % 60;
                timeTxt.text = min + ":" + (int)sec;
            }

            if (time < 20.0f)
            {
                timeTxt.text = "<color=red>" + min + ":" + (int)sec + "</color>"; // timeTxt의 색을 빨간색으로 변경
            }

            if (time < 0.0f)
            {
                GameEnd();
            }
        }
    }

    void generateCard()
    {
        int[] cards = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
        //OrderBy 정렬하겠다 Random.Range 랜덤한 순서로 ToArray() 배열로 만든다
        cards = cards.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 20; i++)
        {
            GameObject newCard = Instantiate(card);
            //newCard를 cards 밑으로 옮겨줘
            newCard.transform.parent = GameObject.Find("cards").transform;

            float x = (i / 5) * 1.4f - 2.1f;
            float y = (i % 5) * 1.4f - 3.5f;
            newCard.transform.position = new Vector3(-2.1f, -3.5f, 0);

            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = sprites[cards[i]];
            newCard.GetComponent<card>().cardName = sprites[cards[i]].name.Substring(0, 3);

            cardList.Add(newCard, new Vector3(x, y, 0));
        }
    }

    IEnumerator moveCard(float waitSeconds)
    {
        foreach (KeyValuePair<GameObject, Vector3> item in cardList)
        {
            GameObject card = item.Key;
            Vector3 to = item.Value;

            card.transform.position = Vector3.Lerp(card.transform.position, to, 0.5f);

            //card.transform.rotation = Quaternion.Lerp(Quaternion.Euler(90f, 0, 0), Quaternion.Euler(0, 0, 0), 0.1f);
            yield return new WaitForSeconds(waitSeconds);
        }

        isCardGenerated = true;
    }

    public void isMatched()
    {
        // 시도 횟수 증가
        counter++;

        //firstCard와 secondCard 같은지 판단
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if(firstCardImage == secondCardImage)
        {
            //매칭 성공 -> 없애자
            audioSource.PlayOneShot(match);
            showTxt(firstCard.GetComponent<card>().cardName);
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            // 점수 더하기
            sumScore(10);

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if(cardsLeft == 2)
            {
                //종료시키자!!
                GameEnd();
            }
        } else
        {
            //덮자
            showTxt("실패!");
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();

            // 점수 차감하기
            sumScore(-5);

            //Sprite Renderer = GameObject에 2D이미지를 표시하는 컴포넌트
            //"back"을 찾아서 color를 새롭게 바꾸는 코드
            firstCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(140 / 255f, 140 / 255f, 160 / 255f, 255 / 255f);
            secondCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(140 / 255f, 140 / 255f, 160 / 255f, 255 / 255f);
            audioSource.PlayOneShot(fail);

            // 짝맞추기 실패했을때 시간을 감소시킴
            if (startBtn.typecase == 0)
            {
                time -= 5.0f;
            }
            else if (startBtn.typecase == 1)
            {
                time -= 4.0f;
            }
            else if (startBtn.typecase == 2)
            {
                time -= 3.0f;
            }
        }

        firstCard = null;
        secondCard = null;
    }

    void showTxt(string txt)
    {
        if (matchTxt.activeSelf == true)
        {
            matchTxt.SetActive(false);
        }

        matchTxt.GetComponent<Text>().text = txt;
        //등장애니메이션 추가
        matchTxt.SetActive(true);
    }

    void sumScore(int a)
    {
        score += a;

        if(score <= 0)
        {
            score = 0;
        }
    }

    void GameEnd()
    {
        Debug.Log("GameEnd");
        // 결과창 띄우기
        endPanel.SetActive(true);

        float endTime = time;

        // 남은 시간의 십의 자리 수 * 5 만큼 점수 더해주기
        // score += (int)(제한시간 - time) / 10 * 5;
        sumScore( (int)endTime / 10 * 5);
        // 점수 표기
        scoreTxt.text = score.ToString();

        count.text = counter.ToString();
        Time.timeScale = 0f;
        audioManager.SetActive(false);
    }

    public void retryGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
