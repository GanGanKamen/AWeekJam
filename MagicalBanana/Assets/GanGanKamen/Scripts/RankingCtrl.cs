using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tweet()
    {
        float score = GameObject.FindGameObjectWithTag("System").GetComponent<SystemCtrl>().score;
        naichilab.UnityRoomTweet.Tweet("bigdatasuperhacker", "私のスコアは" + score.ToString("f2") +"です！", "BigDataSuperHacker", "unity1week");
    }
}
