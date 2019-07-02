using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SystemCtrl : MonoBehaviour
{
    public List<Vector3> randomPos;
    private Vector3[] randomPosSave;
    public bool gamestart;
    public int setNum;
    public Canvas mainCanvas;
    // Start is called before the first frame update
    void Start()
    {
        gamestart = false;
        randomPosSave = new Vector3[randomPos.Count];
        for(int i = 0; i < randomPos.Count; i++)
        {
            randomPosSave[i] = randomPos[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CardShuffle(MagicalSet magicalSet)
    {
        randomPos = randomPos.OrderBy(i => System.Guid.NewGuid()).ToList();
        for(int i = 0; i < magicalSet.dates.Count; i++)
        {
           magicalSet.dates[i].GetComponent<RectTransform>().localPosition = randomPos[i];
        }
    }

    public void GameStart()
    {
        gamestart = true;
        int random = Random.RandomRange(1, setNum + 1);
        GameObject set = Instantiate(Resources.Load<GameObject>("Sets/Set" + random.ToString()), mainCanvas.transform);
        //set.transform.parent = mainCanvas.transform;
        //set.transform.localPosition = Vector3.zero;
    }
}
