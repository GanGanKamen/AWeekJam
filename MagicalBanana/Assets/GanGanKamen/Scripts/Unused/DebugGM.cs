using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGM : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("a");
            audioSource.PlayOneShot( Resources.Load<AudioClip>("SE / SE_start"));
        }
    }
}
