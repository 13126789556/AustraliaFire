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
    

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Image>().color = Color.gray;
        //for saving animal, the cost is the max cost
        moneyCost = moneyCoefficient * GM.x;
        peopleCost = peopleCoefficient * GM.y;
    }

    void Update()
    {
        switch (thisAction)
        {
            case GameManager.actionList.fightFire:
                if (GM.firingTiles <= 0)
                {
                    
                    GetComponent<Image>().color = Color.gray;
                }
                else
                {
                    if(GM.curActionButton == this)
                    {
                        GetComponent<Image>().color = Color.yellow;
                    }
                    else
                    {
                        GetComponent<Image>().color = Color.white;
                    }
                }
                break;
            case GameManager.actionList.cleanWater:
                if (GM.pollutedTiles <= 0)
                {
                    GetComponent<Image>().color = Color.gray;
                }
                else
                {
                    if (GM.curActionButton == this)
                    {
                        GetComponent<Image>().color = Color.yellow;
                    }
                    else
                    {
                        GetComponent<Image>().color = Color.white;
                    }
                }
                break;
            case GameManager.actionList.recoverLand:
                if (GM.scorchTiles <= 0)
                {
                    GetComponent<Image>().color = Color.gray;
                }
                else
                {
                    if (GM.curActionButton == this)
                    {
                        GetComponent<Image>().color = Color.yellow;
                    }
                    else
                    {
                        GetComponent<Image>().color = Color.white;
                    }
                }
                break;
            case GameManager.actionList.saveAnimal:
                if (GM.savableAnimalTiles <= 0)
                {

                    GetComponent<Image>().color = Color.gray;
                }
                else
                {
                    if (GM.curActionButton == this)
                    {
                        GetComponent<Image>().color = Color.yellow;
                    }
                    else
                    {
                        GetComponent<Image>().color = Color.white;
                    }
                }
                break;
        }

    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        print("mouse enter");
        description.activeDescription(moneyCost, peopleCost);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("mouse exit");
        description.deActiveDescription();
    }

    //choose an action
    public void OnPointerClick(PointerEventData eventData)
    {
        //highlight current action
        GM.curActionButton = this;

    }

    


}
