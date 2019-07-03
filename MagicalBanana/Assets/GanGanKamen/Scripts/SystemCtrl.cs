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
    public bool canCtrl;
    public float nowTime;
    [SerializeField] float timeLimit;
    [SerializeField] Text timer;
    public float score;
    [SerializeField] Text scoreText;
    //[SerializeField] GameObject resultCanvas;
    // Start is called before the first frame update
    void Start()
    {
        canCtrl = false;
        nowTime = timeLimit;
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
        timer.text = ((int)nowTime).ToString();
        scoreText.text = score.ToString("f2") + " GB";
        CountDown();
        if((int)nowTime == 0&&gamestart == true)
        {
            gamestart = false;
            canCtrl = false;
            Invoke("GameOver", 2f);
        }
    }

    private void CountDown()
    {
        if(gamestart == true)
        {
            nowTime -= Time.deltaTime;
        }
    }

    public void CardShuffle(MagicalSet magicalSet)
    {
        randomPos = randomPos.OrderBy(i => System.Guid.NewGuid()).ToList();
        for(int i = 0; i < magicalSet.dates.Count; i++)
        {
           magicalSet.dates[i].GetComponent<RectTransform>().localPosition = randomPos[i];
        }
    }

    private void GameOver()
    {
        //resultCanvas.SetActive(true);
        nowTime = 30f;
    }

    public void GameStart()
    {
        canCtrl = true;
        gamestart = true;
        int random = Random.RandomRange(1, setNum + 1);
        GameObject set = Instantiate(Resources.Load<GameObject>("Sets/Set" + random.ToString()), mainCanvas.transform);
    }

    public void NextSet()
    {
        canCtrl = true;
        int random = Random.RandomRange(1, setNum + 1);
        GameObject set = Instantiate(Resources.Load<GameObject>("Sets/Set" + random.ToString()), mainCanvas.transform);
    }
}
