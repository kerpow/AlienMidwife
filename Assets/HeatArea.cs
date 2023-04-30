using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatArea : MonoBehaviour
{
    public List<Egg> eggs;
    public float tempGain = 1f;
    public float levelModifier = 1f;
    // Start is called before the first frame update
    void Start()
    {
        levelModifier = LevelData.instance.GetCurrentLevelData().heatModifier;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var e in eggs)
            e.AddTemp(tempGain * levelModifier * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        var egg = other.GetComponent<Egg>();
        if (!eggs.Contains(egg)) { 
            eggs.Add(egg);

            AudioManager.instance.Play("Sizzle");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var egg = other.GetComponent<Egg>();
        eggs.Remove(egg);
    }
}
