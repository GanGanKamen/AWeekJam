using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalSet : MonoBehaviour
{
    public List<Date> dates;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            dates.Add(transform.GetChild(i).GetComponent<Date>());
        }
        for(int i = 0; i < dates.Count; i++)
        {
            dates[i].canSelect = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DateSelected(Date thisDate)
    {
        if(thisDate.isSelected == true||thisDate.canSelect == false)
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
}
