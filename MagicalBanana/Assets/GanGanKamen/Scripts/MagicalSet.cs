﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalSet : MonoBehaviour
{
    public List<Date> dates;
    private SystemCtrl system;
    // Start is called before the first frame update
    void Start()
    {
        system = GameObject.FindGameObjectWithTag("System").GetComponent<SystemCtrl>();
        for(int i = 0; i < transform.childCount; i++)
        {
            dates.Add(transform.GetChild(i).GetComponent<Date>());
        }
        for(int i = 0; i < dates.Count; i++)
        {
            dates[i].canSelect = true;
        }
        system.CardShuffle(this);
    }

    // Update is called once per frame
    void Update()
    {
        NextCheck();
    }

    public void DateSelected(Date thisDate)
    {
        if(thisDate.isSelected == true||thisDate.canSelect == false||system.canCtrl== false)
        {
            return;
        }
        thisDate.isSelected = true;
        for(int i = 0; i < dates.Count; i++)
        {
            dates[i].canSelect = false;
        }
        for (int i = 0; i < thisDate.nextdates.Count; i++)
        {
            if(thisDate.nextdates[i].isSelected == false)
            {
                thisDate.nextdates[i].canSelect = true;
            }
            
        }
    }

    private void NextCheck()
    {
        int checkNum = 0;
        for(int i = 0; i < dates.Count; i++)
        {
            if(dates[i].canSelect == true)
            {
                checkNum += 1;
            }
        }
        if(checkNum == 0)
        {
            StartCoroutine(DataCollect());
        }
    }

    private IEnumerator DataCollect()
    {
        system.canCtrl = false;
        int score = 0;
        for (int i = 0; i < dates.Count; i++)
        {
            if (dates[i].isSelected == true)
            {
                dates[i].isCollecting = true;
                score += 1;
            }
        }
        yield return new WaitForSeconds(1f);
        system.score += score + Random.RandomRange(0, 0.99f);
        system.NextSet();
        Destroy(gameObject);
    }
}