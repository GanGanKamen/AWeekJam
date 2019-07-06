using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCtrl : MonoBehaviour
{
    private SystemCtrl system;
    private bool isPrepare;
    private bool isBacking;
    [SerializeField] private GameObject accessButton;
    [SerializeField] private RectTransform back;
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private AudioSource se_start, se_titleBack;
    // Start is called before the first frame update
    void Start()
    {
        system = GameObject.FindGameObjectWithTag("System").GetComponent<SystemCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        Approach();
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

    private IEnumerator StartGame()
    {
        isPrepare = true;
        se_start.Play();
        while(maxScale.magnitude - back.localScale.magnitude > 0.01f)
        {
            yield return null;
        }
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
        isBacking = true;
        while(back.localScale != Vector3.one)
        {
            yield return null;
        }
        accessButton.SetActive(true);
        isBacking = false;
        yield break;
    }
}
