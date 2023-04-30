using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Egg eggPrefab;
    public Hatchling hatchlingPrefab;

    public Transform hatchlingExit1;
    public Transform hatchlingExit2;


    public int eggsToHatch = 10;
    public int eggsHatched = 0;

    public float timeRemaining = 60 * 10;

    public TMPro.TextMeshProUGUI eggsHatchedUI;
    public TMPro.TextMeshProUGUI timeUI;

    public GameObject winUI;
    public GameObject loseUI;

    public static LevelManager instance;
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        var data = LevelData.instance.GetCurrentLevelData();

        loseUI.SetActive(false);
        winUI.SetActive(false);
        eggsToHatch = data.eggsToHatch;
        timeRemaining = data.timeLimit;



        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (eggsHatched < eggsToHatch && timeRemaining > 0)
        {
            timeUI.text = string.Format("{0:#}", timeRemaining);
            timeRemaining -= Time.deltaTime;

            if(timeRemaining < 0)
            {
                Lose();
                var eggs = FindObjectsOfType<Egg>();
                foreach(var e in eggs)
                {
                    Destroy(e.gameObject);
                }
            }
        }
    }

    public void EggHatched()
    {
        eggsHatched++;
        UpdateScoreUI();

        if (eggsHatched >= eggsToHatch)
            StartCoroutine(Win());

    }

    private void UpdateScoreUI()
    {
        eggsHatchedUI.text = String.Format("{0} / {1}", eggsHatched, eggsToHatch);
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(8f);
        winUI.SetActive(true);
    }
    private void Lose()
    {
        loseUI.SetActive(true);
    }

    public void NextLevel()
    {

        LevelData.instance.MoveNextLevel();
        LevelData.instance.LoadMenu();
    }
    public void Restart()
    {
        LevelData.instance.LoadGame();
    }
}
