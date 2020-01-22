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
    public Vector2Int coordinate;
    [HideInInspector]
    public GameManager gm;
    private bool fireExtended = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case BlockType.Desert:
                if (status == BlockStatus.Fire)
                {
                    status = BlockStatus.Normal;
<<<<<<< HEAD
=======
                    GetComponent<Renderer>().materials = new Material[1] { desert };
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 532f1e4... UI completed except save animal and save land
=======
>>>>>>> parent of 532f1e4... UI completed except save animal and save land
=======
>>>>>>> parent of 532f1e4... UI completed except save animal and save land
=======
                    //count firing tiles
                    gm.countFire(this, -1);
>>>>>>> parent of 1fe851a... Revert "UI completed except save animal and save land"
=======
                    //count firing tiles
                    gm.countFire(this, -1);
>>>>>>> parent of 1fe851a... Revert "UI completed except save animal and save land"
=======
>>>>>>> parent of 532f1e4... UI completed except save animal and save land
                }
                    break;
            case BlockType.Forest:
                if (status == BlockStatus.Fire)
                {
                    //if on fire, display fire material
                    GetComponent<Renderer>().materials = new Material[2] { GetComponent<Renderer>().material, fire};
                    fireTimer -= Time.deltaTime;
                    //after 5s, fire extends
                    if (fireTimer <= 5 && !fireExtended)
                    {
                        fireExtended = true;
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y][coordinate.x - 1] != null)
                        {
                            gm.grid[coordinate.y][coordinate.x - 1].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y][coordinate.x + 1] != null)
                        {
                            gm.grid[coordinate.y][coordinate.x + 1].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y - 1][coordinate.x] != null)
                        {
                            gm.grid[coordinate.y - 1][coordinate.x].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y + 1][coordinate.x] != null)
                        {
                            gm.grid[coordinate.y + 1][coordinate.x].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                    }
                    //after 10s, become desert
                    if (fireTimer <= 0)
                    {
                        type = BlockType.Desert;
                        status = BlockStatus.Normal;
                        GetComponent<Renderer>().materials = new Material[1] { desert };
                    }
                }

<<<<<<< HEAD
                break;
            case BlockType.Grass:
                if (status == BlockStatus.Fire)
                {
                    GetComponent<Renderer>().materials = new Material[2] { GetComponent<Renderer>().material, fire };
                    fireTimer -= Time.deltaTime;
                    if (fireTimer <= 5 && !fireExtended)
                    {
                        fireExtended = true;
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y][coordinate.x - 1] != null)
                        {
                            gm.grid[coordinate.y][coordinate.x - 1].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y][coordinate.x + 1] != null)
                        {
                            gm.grid[coordinate.y][coordinate.x + 1].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y - 1][coordinate.x] != null)
                        {
                            gm.grid[coordinate.y - 1][coordinate.x].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y + 1][coordinate.x] != null)
                        {
                            gm.grid[coordinate.y + 1][coordinate.x].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                    }
                    if (fireTimer <= 0)
                    {
                        type = BlockType.Desert;
                        status = BlockStatus.Normal;
                        GetComponent<Renderer>().materials = new Material[1] { desert };
                    }
                }
                break;
            case BlockType.Ocean:
                if (status == BlockStatus.Fire)
                {
                    status = BlockStatus.Normal;
                }
                break;
            case BlockType.Shrub:
                if (status == BlockStatus.Fire)
                {
                    GetComponent<Renderer>().materials = new Material[2] { GetComponent<Renderer>().material, fire };
                    fireTimer -= Time.deltaTime;
                    if (fireTimer <= 5 && !fireExtended)
                    {
                        fireExtended = true;
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y][coordinate.x - 1] != null)
                        {
                            gm.grid[coordinate.y][coordinate.x - 1].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y][coordinate.x + 1] != null)
                        {
                            gm.grid[coordinate.y][coordinate.x + 1].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y - 1][coordinate.x] != null)
                        {
                            gm.grid[coordinate.y - 1][coordinate.x].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y + 1][coordinate.x] != null)
                        {
                            gm.grid[coordinate.y + 1][coordinate.x].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                    }
                    if (fireTimer <= 0)
                    {
                        type = BlockType.Desert;
                        status = BlockStatus.Normal;
                        GetComponent<Renderer>().materials = new Material[1] { desert };
                    }
                }
                break;
            case BlockType.Wood:
                if (status == BlockStatus.Fire)
                {
                    GetComponent<Renderer>().materials = new Material[2] { GetComponent<Renderer>().material, fire };
                    fireTimer -= Time.deltaTime;
                    if (fireTimer <= 5 && !fireExtended)
                    {
                        fireExtended = true;
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y][coordinate.x - 1] != null)
                        {
                            gm.grid[coordinate.y][coordinate.x - 1].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y][coordinate.x + 1] != null)
                        {
                            gm.grid[coordinate.y][coordinate.x + 1].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y - 1][coordinate.x] != null)
                        {
                            gm.grid[coordinate.y - 1][coordinate.x].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                        if (Random.Range(0, 3) > 1 && gm.grid[coordinate.y + 1][coordinate.x] != null)
                        {
                            gm.grid[coordinate.y + 1][coordinate.x].GetComponent<BlockManager>().status = BlockStatus.Fire;
                        }
                    }
                    if (fireTimer <= 0)
                    {
                        type = BlockType.Desert;
                        status = BlockStatus.Normal;
                        GetComponent<Renderer>().materials = new Material[1] { desert };
                    }
=======
    private void FireExtend()
    {
        fireExtended = true;
        BlockManager bm;
        //check up, down, left, right 4 blocks
        if (coordinate.x - 1 >= 0)
        {
            bm = gm.grid[coordinate.y][coordinate.x - 1].GetComponent<BlockManager>();
            if (Random.Range(0, 3) > 1 && bm.type != BlockType.Ocean && bm.type != BlockType.Desert)
            {
                bm.status = BlockStatus.Fire;
            }
        }
        if (coordinate.x + 1 < gm.grid[coordinate.y].Count)
        {
            bm = gm.grid[coordinate.y][coordinate.x + 1].GetComponent<BlockManager>();
            if (Random.Range(0, 3) > 1 && bm.type != BlockType.Ocean && bm.type != BlockType.Desert)
            {
                bm.status = BlockStatus.Fire;
            }
        }
        if (coordinate.y - 1 >= 0)
        {
            bm = gm.grid[coordinate.y - 1][coordinate.x].GetComponent<BlockManager>();
            if (Random.Range(0, 3) > 1 && bm.type != BlockType.Ocean && bm.type != BlockType.Desert)
            {
                bm.status = BlockStatus.Fire;
            }
        }
        if (coordinate.y + 1 < gm.grid.Count)
        {
            bm = gm.grid[coordinate.y + 1][coordinate.x].GetComponent<BlockManager>();
            if (Random.Range(0, 3) > 1 && bm.type != BlockType.Ocean && bm.type != BlockType.Desert)
            {
                bm.status = BlockStatus.Fire;
            }
        }
    }
    private void OceanPollute()
    {
        BlockManager bm;
        //check nearby 8 blocks
        if (coordinate.x - 1 >= 0)
        {
            bm = gm.grid[coordinate.y][coordinate.x - 1].GetComponent<BlockManager>();
            if (bm.type == BlockType.Ocean)
            {
                bm.status = BlockStatus.Polluted;
            }
            if (coordinate.y - 1 >= 0)
            {
                bm = gm.grid[coordinate.y - 1][coordinate.x - 1].GetComponent<BlockManager>();
                if (bm.type == BlockType.Ocean)
                {
                    bm.status = BlockStatus.Polluted;
                }
            }
        }
        if (coordinate.x + 1 < gm.grid[coordinate.y].Count)
        {
            bm = gm.grid[coordinate.y][coordinate.x + 1].GetComponent<BlockManager>();
            if (bm.type == BlockType.Ocean)
            {
                bm.status = BlockStatus.Polluted;
            }
            if (coordinate.y + 1 < gm.grid.Count)
            {
                bm = gm.grid[coordinate.y + 1][coordinate.x + 1].GetComponent<BlockManager>();
                if (bm.type == BlockType.Ocean)
                {
                    bm.status = BlockStatus.Polluted;
                }
            }
        }
        if (coordinate.y - 1 >= 0)
        {
            bm = gm.grid[coordinate.y - 1][coordinate.x].GetComponent<BlockManager>();
            if (bm.type == BlockType.Ocean)
            {
                bm.status = BlockStatus.Polluted;
            }
            if (coordinate.x + 1 < gm.grid[coordinate.y].Count)
            {
                bm = gm.grid[coordinate.y - 1][coordinate.x + 1].GetComponent<BlockManager>();
                if (bm.type == BlockType.Ocean)
                {
                    bm.status = BlockStatus.Polluted;
>>>>>>> parent of 532f1e4... UI completed except save animal and save land
                }
                break;
        }
        if (status == BlockStatus.Scorch)
        {
<<<<<<< HEAD
            //add a scorch material later
            GetComponent<Renderer>().materials = new Material[1] { GetComponent<Renderer>().material };
=======
            bm = gm.grid[coordinate.y + 1][coordinate.x].GetComponent<BlockManager>();
            if (bm.type == BlockType.Ocean)
            {
                bm.status = BlockStatus.Polluted;
            }
            if (coordinate.x - 1 >= 0)
            {
                bm = gm.grid[coordinate.y + 1][coordinate.x - 1].GetComponent<BlockManager>();
                if (bm.type == BlockType.Ocean)
                {
                    bm.status = BlockStatus.Polluted;
                }
            }
>>>>>>> parent of 532f1e4... UI completed except save animal and save land
        }
    }
}
