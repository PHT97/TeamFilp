using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startBtn : MonoBehaviour
{
    // ������ư�� �������� �ش緹���� Ÿ���� �����ϴ� int����
    public int type;
    //���������� ������ MainScene���� �θ����ֵ����ϴ� int����
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
        //������ ���������� �ð��� �ٲ�� typecase����
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
