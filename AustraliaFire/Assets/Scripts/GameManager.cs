using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float time;
    public float gold;
    public float fireman;
    public GameObject block;
    //public GameObject valueUI;
    //public GameObject optionUI;
    //public GameObject ;

    private char[][] mapData;
    private GameObject map;
    [HideInInspector]
    public GameObject[][] grid;

    //LZ--->
    //actions players can take
    public enum actionList { fightFire, saveAnimal, cleanWater, recoverLand};
    [HideInInspector] public actionList curAction;
    public int curfireManCost;
    public int curMoneyCost;
    //
    public Text goldDisplay;
    public Text fireManDisplay;
    //
    Ray ray;
    RaycastHit hit;

    //---< LZ
    // Start is called before the first frame update

    void Start()
    {
        GameObject tempGO = new GameObject("Map");
        map = tempGO;
        map.transform.position = Camera.main.transform.position;
        gold = 10000;
        time = 0;
        fireman = 1000;
        GenarateMap();

        //update resource display at the beginning
        updateResourceDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    void GenarateMap()
    {
        Debug.Log(1);
        var rawMapData = Resources.Load<TextAsset>("MapData").ToString();
        Debug.Log(rawMapData);
        mapData  = rawMapData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        for (int r = 0; r < mapData.Length; r++)
        {
            for (int c = 0; c< mapData[r].Length; c++)
            {
                GameObject temp;
                switch (mapData[r][c])
                {
                    //desert
                    case 'd': 
                        temp = Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        temp.GetComponent<BlockManager>().type = BlockManager.BlockType.Desert;
                        temp.GetComponent<BlockManager>().coordinate = new Vector2(c, r);
                        break;
                    //forest
                    case 'f':
                        temp = Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        temp.GetComponent<BlockManager>().type = BlockManager.BlockType.Forest;
                        temp.GetComponent<BlockManager>().coordinate = new Vector2(c, r);
                        break;
                    //grass
                    case 'g':
                        temp = Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        temp.GetComponent<BlockManager>().type = BlockManager.BlockType.Grass;
                        temp.GetComponent<BlockManager>().coordinate = new Vector2(c, r);
                        break;
                    //ocean
                    case 'o':
                        temp = Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        temp.GetComponent<BlockManager>().type = BlockManager.BlockType.Ocean;
                        temp.GetComponent<BlockManager>().coordinate = new Vector2(c, r);
                        break;
                    //shrub
                    case 's':
                        temp = Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        temp.GetComponent<BlockManager>().type = BlockManager.BlockType.Shrub;
                        temp.GetComponent<BlockManager>().coordinate = new Vector2(c, r);
                        break;
                    //wood
                    case 'w':
                        temp = Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        temp.GetComponent<BlockManager>().type = BlockManager.BlockType.Wood;
                        temp.GetComponent<BlockManager>().coordinate = new Vector2(c, r);
                        break;
                }
            }
        }
    }

    public void updateResourceDisplay()
    {
        goldDisplay.text = gold.ToString();
        fireManDisplay.text = fireman.ToString();
    }

    //check which tile the mouse is pointing at
    public void checkMouse()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //print(hit.collider.name);
            BlockManager BM = hit.collider.GetComponent<BlockManager>();
            if (BM != null)
            {
                if (BM.status == BlockManager.BlockStatus.Fire && curAction == GameManager.actionList.fightFire)
                {
                    print("fight fire");
                    //change the following later
                    takeAction();
                }
                else if (BM.status == BlockManager.BlockStatus.Scorch && curAction == GameManager.actionList.recoverLand)
                {
                    print("recover");
                    takeAction();
                }
                else if (BM.status == BlockManager.BlockStatus.Polluted && curAction == GameManager.actionList.cleanWater)
                {
                    print("clean");
                    takeAction();
                }
                if (BM.hasAnimals && BM.status == BlockManager.BlockStatus.Scorch && curAction == GameManager.actionList.cleanWater)
                {
                    print("save animal");
                    takeAction();
                }
            }
        }
    }
    //deduct the cost from the total resource
    private void takeAction()
    {
        gold -= curMoneyCost;
        fireman -= curfireManCost;
        updateResourceDisplay();
    }
}
