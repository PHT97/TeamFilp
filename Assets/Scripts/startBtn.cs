using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startBtn : MonoBehaviour
{
    // 레벨버튼을 눌렀을때 해당레벨의 타입을 설정하는 int변수
    public int type;
    //전역변수로 선언해 MainScene에서 부를수있도록하는 int변수
    public static int typecase;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameStart()
    {
        SceneManager.LoadScene("MainScene");
        //선택한 레벨에따라 시간이 바뀌도록 typecase설정
        if (type == 0)
        {
            typecase = 0;
        }
        else if (type == 1)
        {
            typecase = 1;
        }
        else if (type == 2)
        {
            typecase = 2;
        }
    }
}
