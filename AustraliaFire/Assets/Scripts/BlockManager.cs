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
    public Material pollutedOcean;
    public Material shrub;
    public Material wood;
    public Material scorchedLand;
    public enum BlockType { Desert, Forest, Grass, Ocean, Shrub, Wood }
    //[HideInInspector]
    public BlockType type = BlockType.Ocean;
    public enum BlockStatus { Normal, Fire, Scorch, Polluted }
    //[HideInInspector]
    public BlockStatus status = BlockStatus.Normal;
    public bool hasAnimals;
    public bool hasBrevicep, hasDevil, hasDunnart, hasEmu, hasGoby, hasKoala, hasPlatypus, hasQuoll;
    private float fireTimer = 10;
    private float pollutedTimer = 10;
    [HideInInspector]public float saveAnimalTimerMax = 10;
    [HideInInspector]public float saveAnimalTimer;
    //[HideInInspector]
    public Vector2Int coordinate;
    [HideInInspector]
    public GameManager gm;
    private bool fireExtended = false;
    private bool pollutionExtended = false;
    //LZ------->
    private int curAnimalPeopleCost;
    private int cuAnimalMoneyCost;
    [HideInInspector]public bool animalBurning;

    //--<<<<<LZ
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
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
        //LZ
        saveAnimalTimerMax = fireTimer;
        saveAnimalTimer = saveAnimalTimerMax;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (type == BlockType.Forest || type == BlockType.Grass || type == BlockType.Shrub || type == BlockType.Wood)
        {
            if (status == BlockStatus.Fire)
            {
                //if on fire, display fire material
                GetComponent<Renderer>().materials = new Material[2] { GetComponent<Renderer>().material, fire };
                fireTimer -= Time.deltaTime;
                //nearby ocean block will be polluted immediately
                OceanPollute();
                //after few seconds, fire extends
                if (fireTimer <= 8 && !fireExtended)
                {
                    FireExtend();
                    fireExtended = true;
                }
                //after few seconds, become desert
                if (fireTimer <= 0)
                {
                    type = BlockType.Desert;
                    status = BlockStatus.Normal;
                    GetComponent<Renderer>().materials = new Material[1] { desert };
                    //count firing tiles
                    gm.firingTiles--;
                }
            }
            else if (status == BlockStatus.Scorch)
            {
                //add a scorch material later
                GetComponent<Renderer>().materials = new Material[2] { GetComponent<Renderer>().material, scorchedLand };
            }
            else if (status == BlockStatus.Normal)
            {
                GetComponent<Renderer>().materials = new Material[1] { GetComponent<Renderer>().material};
            }
        }
        else if(type == BlockType.Ocean)
        {
            if (status == BlockStatus.Polluted)
            {
                pollutedTimer -= Time.deltaTime;
                if (pollutedTimer <= 0 && !pollutionExtended)
                {
                    PollutionExtend();
                    pollutionExtended = true;
                }
            }
        }
        else if(type == BlockType.Desert)
        {
            if(status != BlockStatus.Normal)
            {
                status = BlockStatus.Normal;
            }
        }

        
        //animal
        if (hasAnimals && animalBurning)
        {
            print("animal burning");
            saveAnimalTimer -= Time.deltaTime;
            if (saveAnimalTimer <= 0)
            {
                animalBurning = false;
                //has animal == false?
            }
        }
    }

    private void FireExtend()
    {
        BlockManager bm;
        //check up, down, left, right 4 blocks
        if (coordinate.x - 1 >= 0)
        {
            bm = gm.grid[coordinate.y][coordinate.x - 1].GetComponent<BlockManager>();
            if (Random.Range(0, 3) > 1 && bm.type != BlockType.Ocean && bm.type != BlockType.Desert && bm.status != BlockManager.BlockStatus.Scorch && bm.status != BlockManager.BlockStatus.Fire)
            {
                bm.setFire();
            }
        }
        if (coordinate.x + 1 < gm.grid[coordinate.y].Count)
        {
            bm = gm.grid[coordinate.y][coordinate.x + 1].GetComponent<BlockManager>();
            if (Random.Range(0, 3) > 1 && bm.type != BlockType.Ocean && bm.type != BlockType.Desert && bm.status != BlockManager.BlockStatus.Scorch && bm.status != BlockManager.BlockStatus.Fire)
            {
                bm.setFire();
            }
        }
        if (coordinate.y - 1 >= 0)
        {
            bm = gm.grid[coordinate.y - 1][coordinate.x].GetComponent<BlockManager>();
            if (Random.Range(0, 3) > 1 && bm.type != BlockType.Ocean && bm.type != BlockType.Desert && bm.status != BlockManager.BlockStatus.Scorch && bm.status != BlockManager.BlockStatus.Fire)
            {
                bm.setFire();
            }
        }
        if (coordinate.y + 1 < gm.grid.Count)
        {
            bm = gm.grid[coordinate.y + 1][coordinate.x].GetComponent<BlockManager>();
            if (Random.Range(0, 3) > 1 && bm.type != BlockType.Ocean && bm.type != BlockType.Desert && bm.status != BlockManager.BlockStatus.Scorch && bm.status != BlockManager.BlockStatus.Fire)
            {
                bm.setFire();
            }
        }
    }
    public void setFire()
    {
        hasAnimals = true;
        gm.firingTiles++;
        if (hasAnimals)
        {
            animalBurning = true;
        }
        status = BlockStatus.Fire;
        //test
        
    }
    private void OceanPollute()
    {
        BlockManager bm;
        //check nearby 8 blocks
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (coordinate.x + dx >= 0 
                    && coordinate.x + dx <= gm.grid[coordinate.y].Count 
                    && coordinate.y + dy >= 0 
                    && coordinate.y + dy < gm.grid.Count)
                {
                    bm = gm.grid[coordinate.y + dy][coordinate.x + dx].GetComponent<BlockManager>();
                    if (bm.type == BlockType.Ocean)
                    {
                        gm.countOceanPollute(bm);
                        bm.status = BlockStatus.Polluted;
                        bm.GetComponent<Renderer>().material = pollutedOcean;
                    }
                }
            }
        }
    }

    private void PollutionExtend()
    {
        BlockManager bm;
        //check nearby 8 blocks
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (coordinate.x + dx >= 0
                    && coordinate.x + dx <= gm.grid[coordinate.y].Count
                    && coordinate.y + dy >= 0
                    && coordinate.y + dy < gm.grid.Count)
                {
                    bm = gm.grid[coordinate.y + dy][coordinate.x + dx].GetComponent<BlockManager>();
                    if (bm.type == BlockType.Ocean)
                    {
                        gm.countOceanPollute(bm);
                        bm.status = BlockStatus.Polluted;
                        bm.GetComponent<Renderer>().material = pollutedOcean;
                    }
                }
            }
        }
    }
    public void fightFire()
    {
        status = BlockManager.BlockStatus.Scorch;
        GetComponent<Renderer>().materials = new Material[] { GetComponent<Renderer>().materials[0], scorchedLand };
        gm.scorchTiles++;
        gm.firingTiles--;
    }
    public void oceanSave()
    {
        status = BlockManager.BlockStatus.Normal;
        GetComponent<Renderer>().material = ocean;
        gm.pollutedTiles--;
    }
    public void recoverLand()
    {
        status = BlockManager.BlockStatus.Normal;
        GetComponent<Renderer>().materials[1] = null;
        gm.scorchTiles--;
    }
}
