using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatchling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var seq = LeanTween.sequence();

        //this rotation stuff is weird, but this is a jam and it works
        seq.append(() =>
        {

            transform.LookAt(LevelManager.instance.hatchlingExit1.transform.position);
            LeanTween.rotateAroundLocal(gameObject, Vector3.forward, 20, .1f).setLoopPingPong();
        });
        seq.append(LeanTween.move(gameObject, LevelManager.instance.hatchlingExit1.transform.position, 3f));

        seq.append(() =>
        {
            transform.LookAt(LevelManager.instance.hatchlingExit2.transform.position);
            LeanTween.rotateAroundLocal(gameObject, Vector3.forward, 20, .1f).setLoopPingPong();
        });
        seq.append(LeanTween.move(gameObject, LevelManager.instance.hatchlingExit2.transform.position, 2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
