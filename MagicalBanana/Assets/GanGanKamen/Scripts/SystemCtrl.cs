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
    [SerializeField] GameObject result;
    [SerializeField] GameObject acessLoad;
    [SerializeField] private int totalTime;
    private float progressTime;
    private bool countUp;
    [SerializeField] private Slider acessSlider;
    [SerializeField] private Text acessText;
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
        progressTime = 0;
        acessSlider.maxValue = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = ((int)nowTime).ToString();
        scoreText.text = score.ToString("f2") + " GB";
        CountDown();
        CountUp();
        if((int)nowTime == 0&&gamestart == true)
        {
            gamestart = false;
            canCtrl = false;
            Destroy(GameObject.FindGameObjectWithTag("Set"));
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

    private void CountUp()
    {
        if(countUp == true)
        {
            progressTime += Time.deltaTime;
        }
        acessSlider.value = progressTime;
        acessText.text = "データベースへのアクセスが完了まで後" + (totalTime - (int)progressTime).ToString() + "秒";
    }

    public void CardShuffle(MagicalSet magicalSet)
    {
        randomPos = randomPos.OrderBy(i => System.Guid.NewGuid()).ToList();
        for(int i = 0; i < magicalSet.dates.Count; i++)
        {
            var randomX = Random.RandomRange(-20f, 20f);
            var randomY = Random.RandomRange(-50f, 50f);
           magicalSet.dates[i].GetComponent<RectTransform>().localPosition = randomPos[i] 
                + new Vector3(randomX,randomY,0);
        }
    }

    private void GameOver()
    {
        GetComponent<AudioSource>().Stop();
        result.SetActive(true);
        result.GetComponent<ResultCtrl>().ScoreDisplay(score);
        nowTime = timeLimit;
        //Destroy(GameObject.FindGameObjectWithTag("Set"));
    }

    public void GameStartFromTitle()
    {
        StartCoroutine(FromTitleToMain());
    } 

    private IEnumerator FromTitleToMain()
    {
        acessLoad.SetActive(true);
        countUp = true;
        timer.gameObject.SetActive(false);
        while(acessSlider.value < acessSlider.maxValue)
        {
            yield return null;
        }
        acessLoad.SetActive(false);
        countUp = false;
        progressTime = 0;
        timer.gameObject.SetActive(true);
        GameStart();
        yield break;
    }

    public void GameStart()
    {
        SoundManager.PlayBGM(gameObject.GetComponent<AudioSource>());
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
