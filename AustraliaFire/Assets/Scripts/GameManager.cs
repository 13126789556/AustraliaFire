using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<GameManager>();
            if (_instance == null)
            {
                var obj = new GameObject();
                obj.AddComponent<GameManager>();
                _instance = obj.GetComponent<GameManager>();
            }
            return _instance;
        }
    }

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
    public List<List<GameObject>> grid;

    //LZ--->
    //actions players can take
    public enum actionList { fightFire, saveAnimal, cleanWater, recoverLand};
    [HideInInspector] public actionList curAction;
    [HideInInspector] public pickAction curActionButton;
    public int curfireManCost;
    public int curMoneyCost;
    //
    public Text goldDisplay;
    public Text fireManDisplay;
    //
    Ray ray;
    RaycastHit hit;
    //
    [HideInInspector] public int firingTiles;
    [HideInInspector] public int scorchTiles;
    [HideInInspector] public int pollutedTiles;

    //---< LZ
    // Start is called before the first frame update

    void Start()
    {
        grid = new List<List<GameObject>>();
        GameObject tempGO = new GameObject("Map");
        map = tempGO;
        map.transform.position = Camera.main.transform.position;
        gold = 10000;
        time = 0;
        fireman = 1000;
        GenarateMap();

        //update resource display at the beginning
        updateResourceDisplay();
        curActionButton = null;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 2)
        {
            // LZ -->>>>>  count the number of firing tiles
            int randomX = Random.Range(0, grid.Count), randomY = Random.Range(0, grid[0].Count);
            BlockManager BM = grid[randomX][randomY].GetComponent<BlockManager>();
            if (BM != null && BM.status != BlockManager.BlockStatus.Fire)
<<<<<<< HEAD
            {
                firingTiles++;
                print("firing tiles:" + firingTiles);
=======
            {
                firingTiles++;
                print("firing tiles:" + firingTiles);
            }
            //----------< LZ

            //set a random block to fire
            if (BM.type != BlockManager.BlockType.Desert && BM.type != BlockManager.BlockType.Ocean)
            {
                BM.status = BlockManager.BlockStatus.Fire;
>>>>>>> parent of 532f1e4... UI completed except save animal and save land
            }
            //----------< LZ

            grid[randomX][randomY].GetComponent<BlockManager>().status = BlockManager.BlockStatus.Fire;
            if (Random.Range(0, 2) > 1)
            {
                // LZ -->>>>>   count the number of firing tiles
                randomX = Random.Range(0, grid.Count);
                randomY = Random.Range(0, grid[0].Count);
                
                BM = grid[randomX][randomY].GetComponent<BlockManager>();
                if (BM != null && BM.status != BlockManager.BlockStatus.Fire)
                {
                    firingTiles++;
                    print("firing tiles:" + firingTiles);
                }
                //----------< LZ

                grid[Random.Range(0, grid.Count)][Random.Range(0, grid[0].Count)].GetComponent<BlockManager>().status = BlockManager.BlockStatus.Fire;
                //count the number of firing tiles
                firingTiles++;
                print("firing tiles:" + firingTiles);
            }
            time = 0;
        }
        //check mouse over the map
        checkMouse();
    }

    void GenarateMap()
    {
        var rawMapData = Resources.Load<TextAsset>("MapData").ToString();
        mapData  = rawMapData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        for (int r = 0; r < mapData.Length; r++)    //instantiate into grid
        {
            grid.Add(new List<GameObject>());
            for (int c = 0; c< mapData[r].Length; c++)
            {
                switch (mapData[r][c])
                {
                    //desert
                    case 'd': 
                        grid.Last().Add((GameObject) Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1)));
                        grid[r].Last().transform.SetParent(map.transform);
                        grid[r].Last().GetComponent<BlockManager>().type = BlockManager.BlockType.Desert;
                        grid[r].Last().GetComponent<BlockManager>().coordinate = new Vector2Int(c, r);
                        break;
                    //forest
                    case 'f':
                        grid.Last().Add((GameObject)Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1)));
                        grid[r].Last().transform.SetParent(map.transform);
                        grid[r].Last().GetComponent<BlockManager>().type = BlockManager.BlockType.Forest;
                        grid[r].Last().GetComponent<BlockManager>().coordinate = new Vector2Int(c, r);
                        break;
                    //grass
                    case 'g':
                        grid.Last().Add((GameObject)Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1)));
                        grid[r].Last().transform.SetParent(map.transform);
                        grid[r].Last().GetComponent<BlockManager>().type = BlockManager.BlockType.Grass;
                        grid[r].Last().GetComponent<BlockManager>().coordinate = new Vector2Int(c, r);
                        break;
                    //ocean
                    case 'o':
                        grid.Last().Add((GameObject)Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1)));
                        grid[r].Last().transform.SetParent(map.transform);
                        grid[r].Last().GetComponent<BlockManager>().type = BlockManager.BlockType.Ocean;
                        grid[r].Last().GetComponent<BlockManager>().coordinate = new Vector2Int(c, r);
                        break;
                    //shrub
                    case 's':
                        grid.Last().Add((GameObject)Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1)));
                        grid[r].Last().transform.SetParent(map.transform);
                        grid[r].Last().GetComponent<BlockManager>().type = BlockManager.BlockType.Shrub;
                        grid[r].Last().GetComponent<BlockManager>().coordinate = new Vector2Int(c, r);
                        break;
                    //wood
                    case 'w':
                        grid.Last().Add((GameObject)Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1)));
                        grid[r].Last().transform.SetParent(map.transform);
                        grid[r].Last().GetComponent<BlockManager>().type = BlockManager.BlockType.Wood;
                        grid[r].Last().GetComponent<BlockManager>().coordinate = new Vector2Int(c, r);
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
                    //add: highlight the area
                    //if click, take action
                    //add: check if money and people are enough
                    if (Input.GetMouseButtonDown(0))
                    {
                        print("fight fire");
                        //change the following later
                        BM.status = BlockManager.BlockStatus.Scorch;
                        //count the number of firing tiles
                        firingTiles--;
                        print("firing tiles:" + firingTiles);

                        takeAction();
                    }

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
