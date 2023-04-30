using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI levelName;

    public TMPro.TextMeshProUGUI story;
    public GameObject continueButton;

    public List<GameObject> shipGroups;
    // Start is called before the first frame update
    void Start()
    {
        var data = LevelData.instance.GetCurrentLevelData();
        levelName.text = data.name;
        story.text = data.story;

        SetupShipGroups();

        continueButton.SetActive(!data.isLastLevel);

    }

    private void SetupShipGroups()
    {
        var level = LevelData.instance.currentLevel;
        foreach (var s in shipGroups)
            s.SetActive(false);


        if (level < shipGroups.Count)
        {
            shipGroups[level].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextLevel()
    {
        LevelData.instance.LoadGame();
    }
}
