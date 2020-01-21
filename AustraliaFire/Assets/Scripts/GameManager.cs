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
    public GameObject desertBlock;
    public GameObject forestBlock;
    public GameObject grassBlock;
    public GameObject oceanBlock;
    public GameObject shrubBlock;
    public GameObject woodBlock;
    //public GameObject valueUI;
    //public GameObject optionUI;
    //public GameObject ;

    private char[][] mapData;
    private GameObject map;
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
                        temp = Instantiate(desertBlock, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        break;
                    //forest
                    case 'f':
                        temp = Instantiate(forestBlock, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        break;
                    //grass
                    case 'g':
                        temp = Instantiate(grassBlock, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        break;
                    //ocean
                    case 'o':
                        temp = Instantiate(oceanBlock, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        break;
                    //shrub
                    case 's':
                        temp = Instantiate(shrubBlock, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        break;
                    //wood
                    case 'w':
                        temp = Instantiate(woodBlock, new Vector3(c - mapData[r].Length / 2, mapData.Length / 2 - r, 0), new Quaternion(0, 0, 0, 1));
                        temp.transform.SetParent(map.transform);
                        break;
                }
            }
        }
    }
}
