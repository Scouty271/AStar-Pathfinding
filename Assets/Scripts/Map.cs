using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Tile tile;

    public Tile[,] tiles;

    public int size = 100;

    public Tile startTile;
    public Tile endTile;

    private void Start()
    {
        tiles = new Tile[size, size];
        for (int ix = 0; ix < size; ix++)
        {
            for (int iy = 0; iy < size; iy++)
            {
                tile.setSprite(tile.floor);
                tiles[ix, iy] = Instantiate(tile, new Vector3(ix, iy, 0), Quaternion.identity);
            }
        }
    }

    public void ClearMap()
    {
        for (int ix = 0; ix < size; ix++)
        {
            for (int iy = 0; iy < size; iy++)
            {
                tiles[ix, iy].children.Clear();
                tiles[ix, iy].parent = null;
                tiles[ix, iy].isOpen = false;
                tiles[ix, iy].isClosed = false;
                tiles[ix, iy].setFloorProps();
                tiles[ix, iy].GCost = 0;
                tiles[ix, iy].HCost = 0;
                tiles[ix, iy].FCost = 0;
            }
        }
    }
    public void DeleteStartAndTarget()
    {
        startTile.setFloorProps();
        endTile.setFloorProps();
    }


    public Tile getTile(int ix, int iy)
    {
        return tiles[ix, iy];
    }
}
