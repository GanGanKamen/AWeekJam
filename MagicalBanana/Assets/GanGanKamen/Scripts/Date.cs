using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Date : MonoBehaviour
{
    public string name;
    public bool canSelect;
    public bool isSelected;
    public bool isCollecting;
    public List<Date> nextdates;
    private Button button;
    private Vector3 collectionPoint;
    [SerializeField] private GameObject selectedMark;
    [SerializeField] private Text nameText;
    // Start is called before the first frame update
    private void Awake()
    {
        button = GetComponent<Button>();
        collectionPoint = new Vector3(0, -500, 0);
    }

    void Start()
    {
        nameText.text = name;
    }

    // Update is called once per frame
    void Update()
    {
        ButtonStatus();
        if(isCollecting == true)
        {
            GetComponent<RectTransform>().localPosition = Vector3.Lerp(GetComponent<RectTransform>().localPosition, collectionPoint, Time.deltaTime * 4f);
        }
    }

    private void ButtonStatus()
    {
        if(canSelect == true)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
        if(isSelected == true)
        {
            selectedMark.SetActive(true);
        }
        else
        {
            selectedMark.SetActive(false);
        }
    }
}
