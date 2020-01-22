using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pickAction : MonoBehaviour, IPointerClickHandler
{
    private globalManager GM;
    public globalManager.actionList thisAction;
    

    /*
    

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GM").GetComponent<globalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("22222");
        GM.curAction = thisAction;
    }


}
