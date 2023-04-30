using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData : MonoBehaviour
{
    public int currentLevel;
    public LevelDefinition[] levelDefinitions;

    [Serializable]
    public struct LevelDefinition
    {
        public string name;
        public string story;
        public int eggsToHatch;
        public int timeLimit;
        public int hatchInterval;
        public float moistureModifier;
        public float heatModifier;
        public bool isLastLevel;
    }


    public static LevelData instance;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal LevelDefinition GetCurrentLevelData()
    {
        return levelDefinitions[currentLevel];
    }

    public void RestartLevel()
    {

    }
    public void MoveNextLevel()
    {
        currentLevel++;
        if(currentLevel >= levelDefinitions.Length)
        {
            currentLevel = 0;
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
