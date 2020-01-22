using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalManager : MonoBehaviour
{
    public enum actionList {fightFire, cleanWater};
    [HideInInspector]public actionList curAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
