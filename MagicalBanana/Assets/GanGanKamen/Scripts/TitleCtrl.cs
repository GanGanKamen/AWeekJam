using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCtrl : MonoBehaviour
{
    private SystemCtrl system;
    private bool isPrepare;
    [SerializeField] private RectTransform back;
    [SerializeField] private Vector3 maxScale;
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
    }

    private IEnumerator StartGame()
    {
        isPrepare = true;
        while(maxScale.magnitude - back.localScale.magnitude > 0.01f)
        {
            yield return null;
        }
        system.GameStartFromTitle();
        gameObject.SetActive(false);
        yield break;
    }

    public void BackToTitle()
    {

    }
}
