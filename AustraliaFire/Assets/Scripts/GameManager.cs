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
    //[HideInInspector] public actionList curAction;
    [HideInInspector] public pickAction curActionButton;
    //public int curfireManCost;
    //public int curMoneyCost;
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

    public pickAction[] actionButtons;

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
            int randomX = Random.Range(0, grid.Count), randomY = Random.Range(0, grid[0].Count);
            BlockManager BM = grid[randomX][randomY].GetComponent<BlockManager>();
            //set a random block to fire
            if (BM.type != BlockManager.BlockType.Desert && BM.type != BlockManager.BlockType.Ocean && BM.status != BlockManager.BlockStatus.Scorch && BM.status != BlockManager.BlockStatus.Fire)
            {
                //count the number of firing tiles
                firingTiles++;
                //set fire on the tile
                BM.status = BlockManager.BlockStatus.Fire;
            }
            //----------< LZ

            //grid[randomX][randomY].GetComponent<BlockManager>().status = BlockManager.BlockStatus.Fire;
            if (Random.Range(0, 2) > 1)
            {
                // LZ -->>>>>   count the number of firing tiles
                randomX = Random.Range(0, grid.Count);
                randomY = Random.Range(0, grid[0].Count);
                BM = grid[randomX][randomY].GetComponent<BlockManager>();
                //----------< LZ
                if (BM.status != BlockManager.BlockStatus.Scorch && BM.type != BlockManager.BlockType.Desert && BM.type != BlockManager.BlockType.Ocean && BM.status != BlockManager.BlockStatus.Fire)
                {
                    firingTiles++;
                    grid[Random.Range(0, grid.Count)][Random.Range(0, grid[0].Count)].GetComponent<BlockManager>().status = BlockManager.BlockStatus.Fire;
                }
            }
            time = 0;
        }
        //check mouse over the map
        checkMouse();
        //deselect
        deSelectButton();
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

    //action after clicking the map
    public void checkMouse()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //print(hit.collider.name);
            BlockManager BM = hit.collider.GetComponent<BlockManager>();
            if (BM != null && curActionButton != null)
            {
                if (BM.status == BlockManager.BlockStatus.Fire && curActionButton.thisAction == GameManager.actionList.fightFire && gold >= curActionButton.moneyCost && fireman >= curActionButton.peopleCost)
                {
                    //add: highlight the area
                    //if click, take action
                    if (Input.GetMouseButtonDown(0))
                    {
                        print("picked fight fire");
                        //change the following later
                        BM.status = BlockManager.BlockStatus.Scorch;
                        scorchTiles++;
                        reduceResource();
                        //count the number of firing tiles
                        firingTiles--;
                    }
                }
                else if (BM.status == BlockManager.BlockStatus.Polluted && curActionButton.thisAction == GameManager.actionList.cleanWater && gold >= curActionButton.moneyCost && fireman >= curActionButton.peopleCost)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        print("picked clean water");
                        //change the following later
                        BM.status = BlockManager.BlockStatus.Normal;
                        //count the number of firing tiles
                        pollutedTiles--;
                        reduceResource();
                    }
                }
                else if (BM.status == BlockManager.BlockStatus.Scorch && curActionButton.thisAction == GameManager.actionList.recoverLand && gold >= curActionButton.moneyCost && fireman >= curActionButton.peopleCost)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        print("picked recover land");
                        //change the following later
                        BM.status = BlockManager.BlockStatus.Normal;
                        //count the number of firing tiles
                        scorchTiles--;
                        reduceResource();
                    }
                }
                
                //add later: add animal cost to block manager
                if (BM.hasAnimals && BM.status == BlockManager.BlockStatus.Scorch && curActionButton.thisAction == GameManager.actionList.cleanWater)
                {
                    print("save animal");
                    reduceResource();
                }
            }
        }
    }
    //deduct the cost from the total resource
    private void reduceResource()
    {
        gold -= curActionButton.moneyCost;
        fireman -= curActionButton.peopleCost;
        updateResourceDisplay();
    }

    void deSelectButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (curActionButton != null)
            {
                curActionButton.GetComponent<Image>().color = Color.white;
                curActionButton = null;
            }
            
            
        }
    }
    public void countOceanPollute(BlockManager BM)
    {
        if (BM.status != BlockManager.BlockStatus.Polluted)
        {
            pollutedTiles++;
            print("polluted tiles:" + pollutedTiles);
        }
    }
    public void countScortchLand(BlockManager BM)
    {
        if (BM.status != BlockManager.BlockStatus.Scorch)
        {
            scorchTiles++;
            print("scortch tiles:" + scorchTiles);
        }
    }
}
