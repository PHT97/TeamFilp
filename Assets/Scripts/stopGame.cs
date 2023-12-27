using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopGame : MonoBehaviour
{
    //stopPanel »£√‚
    public GameObject stopPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void stopgame()
    {
        stopPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
