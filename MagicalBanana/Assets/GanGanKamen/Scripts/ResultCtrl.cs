using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCtrl : MonoBehaviour
{
    [SerializeField] private GameObject reboot;
    [SerializeField] private Slider rebootSlider;
    [SerializeField] private int totalTime;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject retryButton;
    private SystemCtrl system;
    private float progressTime;
    private bool countUp;
    // Start is called before the first frame update
    void Start()
    {
        system = GameObject.FindGameObjectWithTag("System").GetComponent<SystemCtrl>();
        rebootSlider.maxValue = totalTime;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        CountUp();
        timeText.text = "リトライまで後　" + (totalTime - (int)progressTime).ToString() + "　秒";
    }

    public void ScoreDisplay(float score)
    {
        scoreText.text = score.ToString("f2") + " GB";
    }

    public void Retry()
    {

        StartCoroutine(StartReboot());
    }

    private void CountUp()
    {
        if(countUp == true)
        {
            progressTime += Time.deltaTime;
        }
        rebootSlider.value = progressTime;
    }

    private IEnumerator StartReboot()
    {
        reboot.SetActive(true);
        retryButton.SetActive(false);
        scoreText.gameObject.SetActive(false);
        system.score = 0;
        countUp = true;
        while(rebootSlider.value < rebootSlider.maxValue)
        {
            yield return null;
        }
        Initialize();
        gameObject.SetActive(false);
        system.GameStart();
        yield break;
    }

    private void Initialize()
    {
        scoreText.gameObject.SetActive(true);
        retryButton.SetActive(true);
        reboot.SetActive(false);
        progressTime = 0;
        countUp = false;
    }
}
