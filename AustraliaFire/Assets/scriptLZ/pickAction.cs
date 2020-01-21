using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pickAction : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager GM;
    public GameManager.actionList thisAction;
    public description description;
    //fix following
    [HideInInspector]public int moneyCost;
    [HideInInspector]public int peopleCost;

    public int moneyCoefficient = 1;
    public int peopleCoefficient = 1;
    public int x;
    public int y;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (thisAction != GameManager.actionList.saveAnimal)
        {
            moneyCost = moneyCoefficient * x;
            peopleCost = peopleCoefficient * y;
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (thisAction != GameManager.actionList.saveAnimal)
        {
            description.activeDescription(moneyCost, peopleCost);
        }
            
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (thisAction != GameManager.actionList.saveAnimal)
        {
            description.deActiveDescription();
        }
    }

    //choose an action
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GM.gold >= moneyCost && GM.fireman >= peopleCost)
        {
            //fix following later (delete)
            GM.curAction = thisAction;
            GM.curfireManCost = peopleCost;
            GM.curMoneyCost = moneyCost;
            //update current action
            if (GM.curActionButton != null)
            {
                GM.curActionButton.GetComponent<Image>().color = Color.white;
            }
            //highlight current action
            GM.curActionButton = this;
            GetComponent<Image>().color = Color.yellow;


        }
        else
        {
            print("not enough");
        }
    }

    


}
