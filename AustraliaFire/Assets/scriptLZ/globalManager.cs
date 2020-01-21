using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalManager : MonoBehaviour
{
    public enum actionList {fightFire, cleanWater};
    [HideInInspector]public actionList curAction;

    public int curMoney;
    public int curPeople;


    // Start is called before the first frame update
    void Start()
    {
        curMoney = 5000;
        curPeople = 10;
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
