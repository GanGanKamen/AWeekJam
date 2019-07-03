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
    [SerializeField] private Image image;
    // Start is called before the first frame update
    private void Awake()
    {
        button = GetComponent<Button>();
        collectionPoint = new Vector3(0, -500, 0);
    }

    void Start()
    {
        if(nameText != null)
        {
            nameText.text = name;
        }
        
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
            if(image != null)
            {
                image.color = new Color(255, 255, 255, 1f);
            }
        }
        else
        {
            button.interactable = false;
            if (image != null)
            {
                image.color = new Color(255, 255, 255, 0.5f);
            }
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
