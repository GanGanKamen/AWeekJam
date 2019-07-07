using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalSet : MonoBehaviour
{
    public int id;
    public List<Date> dates;
    private SystemCtrl system;
    public int combo;
    [SerializeField] private AudioSource SE_collect0;
    // Start is called before the first frame update
    void Start()
    {
        combo = 0;
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
        SoundManager.PlaySEOneTime(GetComponent<AudioSource>(), Resources.Load<AudioClip>("SE/SE_newData" ));
    }

    // Update is called once per frame
    void Update()
    {
        
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
        combo += 1;
        SoundManager.PlaySEOneTime(GetComponent<AudioSource>(), Resources.Load<AudioClip>("SE/SE_select0" + combo.ToString()));
        NextCheck();
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

    public IEnumerator DataCollect()
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
        if(score != 0)
        {
            SoundManager.PlaySEOneTime(GetComponent<AudioSource>(), Resources.Load<AudioClip>("SE/SE_colect_" + combo.ToString()));
            SE_collect0.Play();
        }

        score = score * combo;
        yield return new WaitForSeconds(1f);
        system.score += score + Random.RandomRange(0, 0.99f);
        if(system.gamestart == true)
        {
            system.NextSet(id);
        }
        if(this.gameObject != null)
        {
            Destroy(gameObject);
        }
        
        yield break;
    }
}
