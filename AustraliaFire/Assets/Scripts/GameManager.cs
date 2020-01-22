using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //singleton of game manager
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
    public int brevicepNumber, devilNumber, dunnartNubmer, emuNumber, gobyNumber, koalaNumber, platypusNumber, quollNumber;

    private char[][] mapData;
    private char[][] brevicepData;
    private char[][] devilData;
    private char[][] dunnartData;
    private char[][] emuData;
    private char[][] gobyData;
    private char[][] koalaData;
    private char[][] platypusData;
    private char[][] quollData;
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

    public int x;
    public int y;
    public GameObject costDescription;

    //---< LZ
    // Start is called before the first frame update

    void Start()
    {
        brevicepNumber = 10000;
        devilNumber = 7000;
        dunnartNubmer = 100000;
        emuNumber = 600000;
        gobyNumber = 2000;
        koalaNumber = 100000;
        platypusNumber = 30000; 
        quollNumber = 10000;
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
        if (time >= 1.5f)
        {
            int randomX = Random.Range(0, grid.Count), randomY = Random.Range(0, grid[0].Count);
            BlockManager BM = grid[randomX][randomY].GetComponent<BlockManager>();
            //set a random block to fire
            if (BM.type != BlockManager.BlockType.Desert && BM.type != BlockManager.BlockType.Ocean && BM.status != BlockManager.BlockStatus.Scorch && BM.status != BlockManager.BlockStatus.Fire)
            {
                BM.setFire(BM);
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
                    BM.setFire(BM);
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
        var rawData = Resources.Load<TextAsset>("MapData").ToString();
        mapData  = rawData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        rawData = Resources.Load<TextAsset>("BrevicepsData").ToString();
        brevicepData = rawData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        rawData = Resources.Load<TextAsset>("DevilData").ToString();
        devilData = rawData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        rawData = Resources.Load<TextAsset>("DunnartData").ToString();
        dunnartData = rawData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        rawData = Resources.Load<TextAsset>("EmuData").ToString();
        emuData = rawData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        rawData = Resources.Load<TextAsset>("GobyData").ToString();
        gobyData = rawData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        rawData = Resources.Load<TextAsset>("KoalaData").ToString();
        koalaData = rawData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        rawData = Resources.Load<TextAsset>("PlatypusData").ToString();
        platypusData = rawData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        rawData = Resources.Load<TextAsset>("QuollData").ToString();
        quollData = rawData.Split('\n').Select(c => c.ToCharArray()).ToArray();
        for (int r = 0; r < mapData.Length - 1; r++)    //instantiate into grid
        {
            grid.Add(new List<GameObject>());
            for (int c = 0; c< mapData[r].Length - 1; c++)
            {
                grid.Last().Add((GameObject)Instantiate(block, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1)));
                var bm = grid[r].Last().GetComponent<BlockManager>();
                grid[r].Last().transform.SetParent(map.transform);
                bm.coordinate = new Vector2Int(c, r); 
                switch (mapData[r][c])  //block type
                {
                    //desert
                    case 'd':
                        bm.type = BlockManager.BlockType.Desert;
                        break;
                    //forest
                    case 'f':
                        bm.type = BlockManager.BlockType.Forest;
                        break;
                    //grass
                    case 'g':
                        bm.type = BlockManager.BlockType.Grass;
                        break;
                    //ocean
                    case 'o':
                        bm.type = BlockManager.BlockType.Ocean;
                        break;
                    //shrub
                    case 's':
                        bm.type = BlockManager.BlockType.Shrub;
                        break;
                    //wood
                    case 'w':
                        bm.type = BlockManager.BlockType.Wood;
                        break;
                }
                //animal species
                bm.hasBrevicep = brevicepData[r][c] == '1' ? true : false;
                bm.hasDevil = devilData[r][c] == '1' ? true : false;
                bm.hasDunnart = dunnartData[r][c] == '1' ? true : false;
                bm.hasEmu = emuData[r][c] == '1' ? true : false;
                bm.hasGoby = gobyData[r][c] == '1' ? true : false;
                bm.hasKoala = koalaData[r][c] == '1' ? true : false;
                bm.hasPlatypus = platypusData[r][c] == '1' ? true : false;
                bm.hasQuoll = quollData[r][c] == '1' ? true : false;
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
                        reduceResource(curActionButton.moneyCost, curActionButton.peopleCost);
                        //count the number of firing tiles
                        firingTiles--;
                        //? reset fire timer
                    }
                }
                else if (BM.status == BlockManager.BlockStatus.Polluted && curActionButton.thisAction == GameManager.actionList.cleanWater && gold >= curActionButton.moneyCost && fireman >= curActionButton.peopleCost)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        print("picked clean water");
                        //change the following later
                        BM.status = BlockManager.BlockStatus.Normal;
                        BM.GetComponent<Renderer>().material = BM.ocean;
                        //count the number of firing tiles
                        pollutedTiles--;
                        reduceResource(curActionButton.moneyCost, curActionButton.peopleCost);
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
                        reduceResource(curActionButton.moneyCost, curActionButton.peopleCost);
                    }
                }
                //save aimal
                else if (curActionButton.thisAction == GameManager.actionList.saveAnimal && BM.animalBurning)
                {
                    print("display description");
                    float percent = BM.saveAnimalTimer / BM.saveAnimalTimerMax;
                    int curMoneyCost = (int) (curActionButton.moneyCost * percent);
                    int curPeopleCost = (int)(curActionButton.peopleCost * percent);
                    //display animal cost when mouse over the map
                    costDescription.SetActive(true);
                    //when click, check if enough --> reduce resource, reduce animal
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        if (gold >= curMoneyCost && fireman >= curPeopleCost)
                        {
                            print("saved animal");
                            reduceResource(curMoneyCost, curPeopleCost);
                        }
                    }
                }
                else if (!BM.animalBurning)
                {
                    costDescription.SetActive(false);
                }
            }
        }
    }
    //deduct the cost from the total resource
    private void reduceResource(float moneyCost, float peopleCost)
    {
        gold -= moneyCost;
        fireman -= peopleCost;
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
