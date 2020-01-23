using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class description : MonoBehaviour
{
    private GameManager GM;
    private Text money;
    private Text people;
    void Start()
    {
        //find objects
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        money = this.transform.Find("money").GetComponent<Text>();
        people = this.transform.Find("people").GetComponent<Text>();
        this.gameObject.SetActive(false);
    }
    //active the desciption box 
    public void activeDescription(int moneyCost, int FireManCost)
    {
        //update the number of people and money needed every time open the description box
        this.gameObject.SetActive(true);
        this.money.text = "Money Cost: " + moneyCost.ToString();
        this.people.text = "People Cost: " + FireManCost.ToString();
    }
    //de-active the description box
    public void deActiveDescription()
    {
        this.gameObject.SetActive(false);
    }
}
