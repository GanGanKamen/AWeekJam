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
        maxScale = new Vector3(2, 2, 0);
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
            back.localScale = Vector3.Lerp(back.localScale, maxScale, Time.deltaTime*4f);
        }
    }

    private IEnumerator StartGame()
    {
        isPrepare = true;
        while(back.localScale != maxScale)
        {
            yield return null;
        }
        system.GameStart();
        gameObject.SetActive(false);
        yield break;
    }
}
