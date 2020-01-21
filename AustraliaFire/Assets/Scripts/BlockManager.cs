using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Material desert;
    public Material fire;
    public Material forest;
    public Material grass;
    public Material ocean;
    public Material shrub;
    public Material wood;
    public enum BlockType { Desert, Forest, Grass, Ocean, Shrub, Wood }
    //[HideInInspector]
    public BlockType type = BlockType.Ocean;
    public enum BlockStatus { Normal, Fire, Scorch, Polluted }
    //[HideInInspector]
    public BlockStatus status = BlockStatus.Normal;
    public bool hasAnimals;
    private float fireTimer = 10;
    private float pollutedTimer = 10;
    private float saveAnimalTimer = 10;
    //[HideInInspector]
    public Vector2 coordinate;
    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case BlockType.Desert:
                GetComponent<Renderer>().material = desert;
                break;
            case BlockType.Forest:
                GetComponent<Renderer>().material = forest;
                break;
            case BlockType.Grass:
                GetComponent<Renderer>().material = grass;
                break;
            case BlockType.Ocean:
                GetComponent<Renderer>().material = ocean;
                break;
            case BlockType.Shrub:
                GetComponent<Renderer>().material = shrub;
                break;
            case BlockType.Wood:
                GetComponent<Renderer>().material = wood;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case BlockType.Desert:
                break;
            case BlockType.Forest:
                if(status == BlockStatus.Fire)
                {
                    Debug.Log("need fireman");
                    GetComponent<Renderer>().materials = new Material[2] { GetComponent<Renderer>().material, fire };
                    fireTimer -= Time.deltaTime;
                    if(fireTimer <= 0)
                    {
                        type = BlockType.Desert;
                        status = BlockStatus.Normal;
                        GetComponent<Renderer>().materials = new Material[1] { desert };
                    }
                }
                break;
            case BlockType.Grass:
                break;
            case BlockType.Ocean:
                break;
            case BlockType.Shrub:
                break;
            case BlockType.Wood:
                break;
        }
    }
}
