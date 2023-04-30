using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Egg : MonoBehaviour
{
    public bool isHatched = false;



    public float healRate = 5f;
    public float harmRate = 5f;
    public float health = .1f;

    public float temperature = .5f;
    public float minSafeTemp = .25f;
    public float maxSafeTemp = .75f;


    public float moisture = .5f;
    public float minSafeMoisture = .25f;
    public float maxSafeMoisture = .75f;


    public float tempDecay = .1f;
    public float moistureDecay = .1f;

    private Vector3 destination;
    public float eggMoveSpeed = 10f;

    public GameObject closed;
    public GameObject open;

    public Image healthImage;
    public Image tempImage;
    public Image moistureImage;
    public TMPro.TextMeshProUGUI message;
    public GameObject eggModel;
    public GameObject status;

    

    internal void Move(Vector3 point)
    {
        destination = point;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(LevelData.instance.currentLevel == 0) //tutorial
        {
            healRate = healRate * .5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (destination != Vector3.zero && Vector3.Distance(transform.position, destination) > .1f)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * eggMoveSpeed);
        }

        if (health <= 0)
        {
            message.text = "Cracked";
            LeanTween.scale(eggModel, Vector3.zero, 5f);
            StartCoroutine(LateDestroy());

        }
        else if(!isHatched)
        {
            healthImage.fillAmount = health;
            tempImage.fillAmount = temperature;
            moistureImage.fillAmount = moisture;
            var newMessage = GetMessage();
            if (message.text == "" && newMessage != "")
                AudioManager.instance.Play("Alert");
            message.text = GetMessage();

            if (temperature > 0)
                temperature = AdjustBounded(temperature, -tempDecay * Time.deltaTime);

            if (moisture > 0)
                moisture = AdjustBounded(moisture, -moistureDecay * Time.deltaTime);


            //everything is good
            if (temperature > minSafeTemp && temperature < maxSafeTemp && moisture > minSafeMoisture && moisture < maxSafeMoisture)
            {
                health = AdjustBounded(health, healRate * Time.deltaTime);
            }
            else
            {
                health = AdjustBounded(health, -harmRate * Time.deltaTime);
            }

            if (health == 1)
            {
                SetOpen(true);
                
            }

        }


    }

    private IEnumerator LateDestroy()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Destroy(gameObject);
    }
    private IEnumerator LateDestroyAndSpawnHatchling()
    {
        yield return new WaitForSeconds(5f);
        AudioManager.instance.Play("Hatch");

        yield return new WaitForSeconds(.6f);
        var h = Instantiate<Hatchling>(LevelManager.instance.hatchlingPrefab);
        h.transform.position = transform.position;
        GameObject.Destroy(gameObject);

    }
    private string GetMessage()
    {
        if (moisture < minSafeMoisture)
            return "Need Ooze";
        if (moisture > maxSafeMoisture)
            return "Too Oozy";
        if (temperature < minSafeTemp)
            return "Need Heat";
        if (temperature > maxSafeTemp)
            return "Too Hot";
        if (health == 0)
            return "Rotten Egg";


        return "";


    }

    public float AdjustBounded(float source, float adjustment)
    {
        source += adjustment;
        if (source > 1)
            source = 1f;
        if (source < 0)
            source = 0f;

        return source;
    }


    public void SetOpen(bool isOpen)
    {
        open.SetActive(isOpen);
        closed.SetActive(!isOpen);
        isHatched = isOpen;
        status.SetActive(false);
        LeanTween.moveLocal(eggModel.gameObject, Vector3.one * .05f, .1f).setLoopPingPong();
        LevelManager.instance.EggHatched();
        StartCoroutine(LateDestroyAndSpawnHatchling());
    }

    internal void AddTemp(float v)
    {
        temperature = AdjustBounded(temperature, v);
    }

    internal void AddMoisture(float v)
    {
        moisture = AdjustBounded(moisture, v);
    }
}
