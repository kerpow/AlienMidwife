using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject valid;
    public GameObject invalid;
    internal void SetValid(bool v)
    {
        valid.SetActive(v);
        invalid.SetActive(!v);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
