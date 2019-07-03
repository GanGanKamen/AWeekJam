using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCtrl : MonoBehaviour
{
    [SerializeField] private Text scoreText;
     private SystemCtrl system;
    // Start is called before the first frame update
    void Start()
    {
         system = GameObject.FindGameObjectWithTag("System").GetComponent<SystemCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreDisplay(float score)
    {
        scoreText.text = score.ToString("f2") + " GB";
    }

    public void Retry()
    {
        system.score = 0;
        system.GameStart();
    }
}
