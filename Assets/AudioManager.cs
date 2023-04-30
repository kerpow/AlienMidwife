using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public AudioSource[] clips;

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }



    // Start is called before the first frame update
    void Start()
    {
        clips = GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play(string name)
    {
        foreach(var c in clips)
        {
            if (c.transform.name == name)
            {

                c.Play();
                return;
            }
        }

        Debug.Log("Unable to find clip " + name);
    }

}
