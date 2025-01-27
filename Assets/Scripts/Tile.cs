using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tile : MonoBehaviour
{
    public enum TileTypes
    {
        floor,
        wall,
        start,
        target,
        open,
        path,
        closed
    }
    public TileTypes tileType;

    private Sprite currSprite;

    private Vector2Int position;

    public int GCost;
    public int HCost;
    public int FCost;

    public bool isWalkable = true;
    public bool isOpen;
    public bool isClosed;

    public Sprite floor;
    public Sprite wall;
    public Sprite start;
    public Sprite target;
    public Sprite path;
    public Sprite open;
    public Sprite closed;

    public Tile parent;

    public List<Tile> children = new List<Tile>();

    private void Start()
    {
        position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        name = "Tile: " + position.x + ", " + position.y;
    }

    public void InsertChild(Tile _child)
    {
        if (children.Count > 0)
        {
            for (int i = 0; i < children.Count; i++)
            {
                try
                {
                    if (_child.FCost == children[i].FCost && _child.HCost <= children[i].HCost)
                    {
                        children.Insert(i, _child);
                        break;
                    }
                    else if (_child.FCost < children[i].FCost)
                    {
                        children.Insert(i, _child);
                        break;
                    }
                    else if (_child.FCost > children[i].FCost && _child.FCost <= children[i + 1].FCost)
                    {
                        children.Insert(i + 1, _child);
                        break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    children.Add(_child);
                    break;
                }

            }
        }
        else
            children.Add(_child);
    }

    public void setWallProps()
    {
        tileType = TileTypes.wall;
        isWalkable = false;
        GetComponent<SpriteRenderer>().sprite = wall;
        currSprite = wall;
    }
    public void setClosedProps()
    {
        tileType = TileTypes.closed;
        isWalkable = true;
        GetComponent<SpriteRenderer>().sprite = closed;
        currSprite = closed;
    }
    public void setFloorProps()
    {
        tileType = TileTypes.floor;
        isWalkable = true;
        GetComponent<SpriteRenderer>().sprite = floor;
        currSprite = floor;
    }
    public void setStartProps()
    {
        tileType = TileTypes.start;
        GetComponent<SpriteRenderer>().sprite = start;
        isWalkable = true;
        currSprite = start;
    }
    public void setTargetProps()
    {
        tileType = TileTypes.target;
        GetComponent<SpriteRenderer>().sprite = target;
        isWalkable = true;
        currSprite = target;
    }
    public void setPathProps()
    {
        tileType = TileTypes.path;
        GetComponent<SpriteRenderer>().sprite = path;
        isWalkable = true;
        currSprite = path;
    }
    public void setOpenProps()
    {
        tileType = TileTypes.open;
        GetComponent<SpriteRenderer>().sprite = open;
        isWalkable = true;
        currSprite = open;
    }

    public Sprite getSprite()
    {
        return currSprite;
    }
    public TileTypes getTileType()
    {
        return tileType;
    }
    public Vector2Int getPosition()
    {
        return position;
    }

    public void setSprite(Sprite spr)
    {
        currSprite = spr;
        GetComponent<SpriteRenderer>().sprite = currSprite;
    }
    public void setTileType(TileTypes type)
    {
        tileType = type;
    }
}
