using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeaponBoxCanvasUI : MonoBehaviour
{

    [SerializeField] Sprite chosenBoxSprite;
    [SerializeField] Sprite unchosenBoxSprite;

    private GameObject boxImage;
    private GameObject weaponImage;
    private GameObject firstCounterBackground;
    private GameObject secondCounterBackground;
    private GameObject firstCounterText;
    private GameObject secondCounterText;


    // Start is called before the first frame update
    void Start()
    {

        List<GameObject> list = new List<GameObject>();

        foreach(Transform child in this.transform)
        {
            list.Add(child.gameObject);
        }

        this.boxImage = list[0];
        this.weaponImage = list[1];
        this.firstCounterBackground = list[2];
        this.firstCounterText = list[3];
        this.secondCounterBackground = list[4];
        this.secondCounterText = list[5];
    }

    public void TurnIntoEmptyBox()
    {
        this.boxImage.SetActive(true);
        this.weaponImage.SetActive(false);
        this.firstCounterBackground.SetActive(false);
        this.firstCounterText.SetActive(false);
        this.secondCounterBackground.SetActive(false);
        this.secondCounterText.SetActive(false);
    }

    public bool SetTheBoxChosen()
    {
        if (this.boxImage == null)
            return false;
        this.boxImage.GetComponent<Image>().sprite = this.chosenBoxSprite;
        return true;
    }

    public bool SetTheBoxUnchosen()
    {
        if (this.boxImage == null)
            return false;
        this.boxImage.GetComponent<Image>().sprite = this.unchosenBoxSprite;
        return true;
    }

    public bool SetAndShowFirstCounter(int value)
    {
        if (this.firstCounterBackground == null || this.firstCounterText == null)
            return false;
        this.firstCounterBackground.SetActive(true);
        this.firstCounterText.SetActive(true);

        TextMeshPro textCounter = this.firstCounterText.GetComponent<TextMeshPro>();

        if (textCounter != null)
            textCounter.text = value.ToString();
        return true;
    }

    public bool SetAndShowSecondCounter(int value)
    {
        if (this.secondCounterBackground == null || this.secondCounterText == null)
            return false;
        this.secondCounterBackground.SetActive(true);
        this.secondCounterText.SetActive(true);
        this.secondCounterText.GetComponent<TextMeshPro>().text = value.ToString();
        return true;
    }

    public bool HideFirstCounter()
    {
        bool result = true;

        if (this.firstCounterText != null)
        {
            this.firstCounterText.SetActive(false);
        }
        else
        {
            result = false;
        }

        if (this.firstCounterBackground != null)
        {
            this.firstCounterBackground.SetActive(false);
        }
        else
        {
            result = false;
        }

        return result;
    }

    public bool HideSecondCounter()
    {
        bool result = true;

        if (this.secondCounterText != null)
        {
            this.secondCounterText.SetActive(false);
        }
        else
        {
            result = false;
        }

        if (this.secondCounterBackground != null)
        {
            this.secondCounterBackground.SetActive(false);
        }
        else
        {
            result = false;
        }

        return result;
    }

    public bool SetAndShowWeaponImage(Sprite sprite)
    {
        if (this.weaponImage == null)
            return false;
        this.weaponImage.SetActive(true);
        this.weaponImage.GetComponent<Image>().sprite = sprite;
        return true;
    }

    public bool hideWeaponImage()
    {
        if (this.weaponImage == null)
            return false;
        this.weaponImage.SetActive(false);
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
