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
    public enum actionList { fightFire, cleanWater };
    [HideInInspector] public actionList curAction;
    //
    public Text goldDisplay;
    public Text fireManDisplay;
   
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
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

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
}
