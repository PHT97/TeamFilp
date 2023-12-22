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

    public Text matchTxt;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endTxt;

    public AudioClip match;
    public AudioClip fail;
    public AudioSource audioSource;

    public Sprite[] sprites;

    // ���â
    public GameObject endPanel;
    // ī�� ��Ī �õ� Ƚ�� ī����
    public int counter = 0;
    // ī�� ��Ī �õ� Ƚ�� Text
    public Text count;
    

    public static GameManager I;

    void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
        //OrderBy �����ϰڴ� Random.Range ������ ������ ToArray() �迭�� �����
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 20; i++)
        {
            GameObject newCard = Instantiate(card);
            //newCard�� cards ������ �Ű���
            newCard.transform.parent = GameObject.Find("cards").transform;

            float x = (i / 5) * 1.4f - 2.1f;
            float y = (i % 5) * 1.4f - 3.5f;
            newCard.transform.position = new Vector3(x, y, 0);

            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = sprites[rtans[i]];
            newCard.GetComponent<card>().cardName = sprites[rtans[i]].name.Substring(0,3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time > 20.0f)
        {
            timeTxt.text = "<color=red>" + time.ToString("N2") + "</color>"; // timeTxt�� ���� ���������� ����
        }

        if (time > 30.0f)
        {
            GameEnd();
        }
    }

    public void isMatched()
    {
        // �õ� Ƚ�� ����
        counter++;

        //firstCard�� secondCard ������ �Ǵ�
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if(firstCardImage == secondCardImage)
        {
            //��Ī ���� -> ������
            audioSource.PlayOneShot(match);
            matchTxt.text = firstCard.GetComponent<card>().cardName;
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if(cardsLeft == 2)
            {
                //�����Ű��!!
                GameEnd();
            }
        } else
        {
            //����
            matchTxt.text = "����!";
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();

            //Sprite Renderer = GameObject�� 2D�̹����� ǥ���ϴ� ������Ʈ
            //"back"�� ã�Ƽ� color�� ���Ӱ� �ٲٴ� �ڵ�
            firstCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(140 / 255f, 140 / 255f, 160 / 255f, 255 / 255f);
            secondCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(140 / 255f, 140 / 255f, 160 / 255f, 255 / 255f);
            audioSource.PlayOneShot(fail);
        }

        firstCard = null;
        secondCard = null;
    }

    void GameEnd()
    {
        //endTxt.SetActive(true);
        //endTxt.text = counter.ToString();
        endPanel.SetActive(true);
        count.text = counter.ToString();
        Time.timeScale = 0f;
    }

    public void retryGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
