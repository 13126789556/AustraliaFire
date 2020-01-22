using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    public globalManager GM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        print("11111");
        if (GM.curAction == globalManager.actionList.fightFire)
        {
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (GM.curAction == globalManager.actionList.cleanWater)
        {
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
}
