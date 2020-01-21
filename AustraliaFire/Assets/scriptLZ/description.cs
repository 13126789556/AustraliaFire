using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class description : MonoBehaviour
{
    private globalManager GM;
    private Text money;
    private Text people;
    void Start()
    {
        //find objects
        GM = GameObject.Find("GM").GetComponent<globalManager>();
        money = this.transform.Find("money").GetComponent<Text>();
        people = this.transform.Find("people").GetComponent<Text>();
        this.gameObject.SetActive(false);
    }


    //active the desciption box 
    public void activeDescription()
    {
        //update the number of people and money needed every time open the description box
        this.gameObject.SetActive(true);
        money.text = "Money Cost: " + GM.curMoney.ToString();
        people.text = "People Cost: " + GM.curPeople.ToString();
    }
    //de-active the description box
    public void deActiveDescription()
    {
        this.gameObject.SetActive(false);
    }
}
