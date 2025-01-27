using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Pathfinding : MonoBehaviour
{
    public Tile tile;
    public Map map;

    public List<Tile> openList = new List<Tile>();
    public List<Tile> closedList = new List<Tile>();

    public List<Tile> path = new List<Tile>();

    public bool pathFound;

    public void FindWithAStar(Tile _start, Tile _target)
    {
        var currTile = _start;

        currTile.isOpen = true;

        addNeighborsToOpen(currTile);

        while (true)
        {
            addNeighborsToOpen(currTile);

            if (currTile.children.Count > 0)
            {
                foreach (var child in currTile.children)
                {
                    if (child.FCost <= openList[0].FCost)
                    {
                        if (child.isOpen)
                        {
                            child.setClosedProps();
                        }

                        child.isClosed = true;
                        openList.Remove(child);
                        currTile = child;
                        updateNeightbors(currTile);
                        break;
                    }
                    else
                    {
                        currTile = openList[0];
                        currTile.isClosed = true;
                        currTile.setClosedProps();
                        openList.Remove(currTile);
                        updateNeightbors(currTile);
                        break;
                    }
                }
            }
            else
            {
                currTile = openList[0];
                currTile.isClosed = true;
                currTile.setClosedProps();
                openList.Remove(currTile);
            }

            if (currTile == _target)
            {
                while (true)
                {
                    if (currTile.parent != null)
                    {
                        currTile.setPathProps();
                        path.Add(currTile);
                        currTile = currTile.parent;
                    }
                    else
                        break;
                }
                break;
            }
        }
    }

    private void updateNeightbors(Tile _currTile)
    {
        updateNeightbor(_currTile, 1, 0);
        updateNeightbor(_currTile, -1, 0);
        updateNeightbor(_currTile, 0, 1);
        updateNeightbor(_currTile, 0, -1);

        updateNeightbor(_currTile, 1, 1);
        updateNeightbor(_currTile, -1, 1);
        updateNeightbor(_currTile, -1, -1);
        updateNeightbor(_currTile, 1, -1);
    }

    private void addNeighborsToOpen(Tile _currTile)
    {
        addNeighborToOpen(_currTile, 1, 0);
        addNeighborToOpen(_currTile, -1, 0);
        addNeighborToOpen(_currTile, 0, 1);
        addNeighborToOpen(_currTile, 0, -1);

        addNeighborToOpen(_currTile, 1, 1);
        addNeighborToOpen(_currTile, -1, -1);
        addNeighborToOpen(_currTile, -1, 1);
        addNeighborToOpen(_currTile, 1, -1);
    }

    private void updateNeightbor(Tile _currTile, int _x, int _y)
    {
        try
        {
            var t = map.tiles[_currTile.getPosition().x + _x, _currTile.getPosition().y + _y];

            if (t.isWalkable && t != null && t.isOpen && t.parent != _currTile && !t.isClosed && t.getTileType() != Tile.TileTypes.start)
            {
                var backupGCost = t.GCost;

                setTilePathfindingValues(t, _currTile);

                if (t.GCost <= backupGCost)
                {
                    t.parent.children.Remove(t);
                    t.parent = _currTile;
                    _currTile.InsertChild(t);
                }
                else
                {
                    setTilePathfindingValues(t, t.parent);
                }
            }
        }
        catch (Exception)
        {
        }
    }

    private void addNeighborToOpen(Tile _currTile, int _x, int _y)
    {
        try
        {
            var t = map.tiles[_currTile.getPosition().x + _x, _currTile.getPosition().y + _y];

            if (t != null && !t.isOpen && t.isWalkable && !t.isClosed)
            {
                setTilePathfindingValues(t, _currTile);
                t.parent = _currTile;
                _currTile.InsertChild(t);
                InsertToOpen(t);

                if (t.getTileType() != Tile.TileTypes.target && t.getTileType() != Tile.TileTypes.start && t.getTileType() != Tile.TileTypes.closed)
                {
                    t.isOpen = true;
                    t.setOpenProps();
                }
            }
        }
        catch (IndexOutOfRangeException)
        {
        }
    }

    public void InsertToOpen(Tile _tile)
    {
        if (openList.Count > 0)
        {
            for (int i = 0; i < openList.Count; i++)
            {
                try
                {
                    if (_tile.FCost == openList[i].FCost && _tile.HCost <= openList[i].HCost)
                    {
                        openList.Insert(i, _tile);
                        break;
                    }
                    else if (_tile.FCost < openList[i].FCost)
                    {
                        openList.Insert(i, _tile);
                        break;
                    }
                    else if (_tile.FCost > openList[i].FCost && _tile.FCost <= openList[i + 1].FCost)
                    {
                        openList.Insert(i + 1, _tile);
                        break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    openList.Add(_tile);
                    break;
                }

            }
        }
        else
            openList.Add(_tile);
    }

    private void setTilePathfindingValues(Tile _neighborTile, Tile currTile)
    {
        var neighborXPos = _neighborTile.getPosition().x;
        var neighborYPos = _neighborTile.getPosition().y;

        var currXPos = currTile.getPosition().x;
        var currYPos = currTile.getPosition().y;

        var diffX = neighborXPos - currXPos;
        var diffY = neighborYPos - currYPos;

        //////////////////////////////////////////////////////////

        _neighborTile.GCost = 0;

        if (diffX < 0)
            diffX = -diffX;

        if (diffY < 0)
            diffY = -diffY;

        if (diffX > 0 && diffY > 0)
        {
            _neighborTile.GCost = currTile.GCost + 14;
        }
        else if (diffX > 0)
        {
            _neighborTile.GCost = currTile.GCost + 10;
        }
        else if (diffY > 0)
        {
            _neighborTile.GCost = currTile.GCost + 10;
        }

        //////////////////////////////////////////////////////////

        _neighborTile.HCost = 0;

        var endXPos = map.endTile.getPosition().x;
        var endYPos = map.endTile.getPosition().y;

        var xDiffEnd = neighborXPos - endXPos;
        var yDiffEnd = neighborYPos - endYPos;

        if (xDiffEnd < 0)
            xDiffEnd = -xDiffEnd;

        if (yDiffEnd < 0)
            yDiffEnd = -yDiffEnd;

        while (xDiffEnd != 0 || yDiffEnd != 0)
        {
            if (xDiffEnd > 0 && yDiffEnd > 0)
            {
                _neighborTile.HCost += 14;

                xDiffEnd--;
                yDiffEnd--;
            }
            else if (xDiffEnd > 0)
            {
                _neighborTile.HCost += 10;
                xDiffEnd--;
            }
            else if (yDiffEnd > 0)
            {
                _neighborTile.HCost += 10;
                yDiffEnd--;
            }
        }

        _neighborTile.FCost = _neighborTile.HCost + _neighborTile.GCost;
    }

    public void clearValues()
    {
        pathFound = false;
        openList.Clear();
        closedList.Clear();
        path.Clear();
    }
}