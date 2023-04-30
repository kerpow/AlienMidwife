using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private int tutorialNumber;
    public List<string> messsages;
    public TMPro.TextMeshProUGUI message;
    public TMPro.TextMeshProUGUI progress;

    public void NextTutorial()
    {
        message.text = messsages[tutorialNumber];
        progress.text = string.Format("{0} / {1}", tutorialNumber + 1, messsages.Count);
        tutorialNumber++;
        if (tutorialNumber == messsages.Count)
            tutorialNumber = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (LevelData.instance.currentLevel == 0)
        {
            NextTutorial();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
