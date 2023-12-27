using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartBtn : MonoBehaviour
{
    public GameObject stopPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void restart()
    {
        audioManager.I.audioSource.UnPause();
        stopPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
