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
    public int moneyCost;
    public int peopleCost;
    




    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("MOUSE ON ACTION");
        description.activeDescription(moneyCost, peopleCost); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.deActiveDescription();
        //GetComponent<Outline>().acti
    }
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //choose an action
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GM.gold >= moneyCost && GM.fireman >= peopleCost)
        {
            //update current action
            GM.curAction = thisAction;
            GM.curfireManCost = peopleCost;
            GM.curMoneyCost = moneyCost;
            
            
        }
        else
        {
            print("not enough");
        }
    }

    


}
