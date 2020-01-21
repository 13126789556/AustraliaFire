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
        print("MOUSE ON ACTION");
        description.activeDescription();
        
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
        print("CLICK");
        if (GM.gold >= moneyCost && GM.fireman >= peopleCost)
        {
            //reduce money and people if any action is taken
            print("button clicked");
            
            //if have enough $ and people, change the current action to this action
            GM.curAction = thisAction;
            GM.updateResourceDisplay();
            //change the following later
            GM.gold -= moneyCost;
            GM.fireman -= peopleCost;
        }
        else
        {
            print("not enough");
        }
    }


}
