using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Queen : MonoBehaviour
{
    public float eggCooldown = 0f;
    public float eggSpawnInterval = 30f;
    public Animator animator;
    public Transform eggBasket;
    public Transform eggSpawnStart;

    // Start is called before the first frame update
    void Start()
    {
        var data = LevelData.instance.GetCurrentLevelData();
        eggSpawnInterval = data.hatchInterval;
        eggCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(eggCooldown > 0)
            eggCooldown -= Time.deltaTime;
        else if (IsEggSpawnClear() && AreMoreEggsNeeded())
            TriggerLayEgg();
    }


    private bool AreMoreEggsNeeded()
    {
        var activeEggCount = FindObjectsOfType<Egg>().Length;
        if (activeEggCount + LevelManager.instance.eggsHatched < LevelManager.instance.eggsToHatch)
        {
            return true;
        }
        return false;
    }

    private void TriggerLayEgg()
    {
        animator.SetTrigger("LayEgg");
        LeanTween.delayedCall(4.16f, () =>
        {
            var egg = Instantiate<Egg>(LevelManager.instance.eggPrefab, eggBasket);
            egg.transform.position = eggSpawnStart.position;
            LeanTween.move(egg.gameObject, eggBasket.position, .25f);

            AudioManager.instance.Play("Roar");

        });
        eggCooldown = eggSpawnInterval;
    }

    private bool IsEggSpawnClear()
    {
        var colliders = Physics.OverlapSphere(eggBasket.position, 1);
        
        foreach(var c in colliders)
        {
            if (c.GetComponentInParent<Egg>() != null)
            {
                return false;
            }
        }
        return true;
    }
}
