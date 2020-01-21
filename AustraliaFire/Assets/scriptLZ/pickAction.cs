using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pickAction : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private globalManager GM;
    public globalManager.actionList thisAction;
    public description description;
    public int moneyCost;
    public int peopleCost;


    public void OnPointerEnter(PointerEventData eventData)
    {
        description.activeDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.deActiveDescription();
        //GetComponent<Outline>().acti
    }
    void Start()
    {
        GM = GameObject.Find("GM").GetComponent<globalManager>();
    }

    //when click, when if the current money and people are enough
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (GM.curMoney >= moneyCost && GM.curPeople >= peopleCost)
        {
            //reduce money and people if any action is taken
            print("button clicked");
            GM.curMoney -= moneyCost;
            GM.curPeople -= peopleCost;
            //if have enough $ and people, change the current action to this action
            GM.curAction = thisAction;
        }
        else
        {
            print("not enough");
        }
    }


}
