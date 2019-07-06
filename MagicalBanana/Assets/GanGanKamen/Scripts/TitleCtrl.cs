using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCtrl : MonoBehaviour
{
    private SystemCtrl system;
    private bool isPrepare;
    private bool isBacking;
    private bool isClearing;
    private float alpha;
    [SerializeField] private GameObject titleLogo;
    [SerializeField] private GameObject accessButton;
    [SerializeField] private RectTransform back;
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private AudioSource se_start, se_titleBack;
    [SerializeField] private RawImage[] backImages;
    // Start is called before the first frame update
    void Start()
    {
        system = GameObject.FindGameObjectWithTag("System").GetComponent<SystemCtrl>();
        alpha = 1;
        isClearing = false;
    }

    // Update is called once per frame
    void Update()
    {
        Approach();
        Clear();
    }

    public void GameStart()
    {
        StartCoroutine(StartGame());
    }

    private void Approach()
    {
        if(isPrepare == true)
        {
            back.localScale = Vector3.Lerp(back.localScale, maxScale, Time.deltaTime*2f);
        }
        else if(isBacking == true)
        {
            back.localScale = Vector3.Lerp(back.localScale, Vector3.one, Time.deltaTime * 2f);
        }
        
    }

    private void Clear()
    {
        for (int i = 0; i < backImages.Length; i++)
        {
            backImages[i].color = new Color(255, 255, 255, alpha);
        }
        if (isClearing == true)
        {
            alpha -= Time.deltaTime;
        }
    }

    private IEnumerator StartGame()
    {
        isPrepare = true;
        se_start.Play();
        while(maxScale.magnitude - back.localScale.magnitude > 0.01f)
        {
            yield return null;
        }
        isClearing = true;
        while (alpha > 0)
        {
            yield return null;
        }
        isClearing = false;
        alpha = 1;
        system.GameStartFromTitle();
        isPrepare = false;
        gameObject.SetActive(false);
        yield break;
    }

    public void BackToTitle()
    {
        StartCoroutine(FromResultToTitle());
    }

    private IEnumerator FromResultToTitle()
    {
        se_titleBack.Play();
        back.localScale = maxScale;
        accessButton.SetActive(false);
        titleLogo.SetActive(false);
        isBacking = true;
        while(back.localScale != Vector3.one)
        {
            yield return null;
        }
        accessButton.SetActive(true);
        titleLogo.SetActive(true);
        isBacking = false;
        yield break;
    }
}
