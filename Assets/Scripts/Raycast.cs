using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Tile tile;
    public Map map;

    Ray ray;
    RaycastHit hit;

    public Camera cam;

    private void Start()
    {
        ray = new Ray();
    }

    private void Update()
    {
        ray.origin = cam.ScreenToWorldPoint(Input.mousePosition);
        ray.direction = this.transform.TransformDirection(new Vector3(0, 0, 1));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    if (hit.collider.GetComponent<Tile>().getTileType() == Tile.TileTypes.floor)
                        hit.collider.GetComponent<Tile>().setWallProps();
                }
                else
                {
                    if (hit.collider.GetComponent<Tile>().getTileType() == Tile.TileTypes.wall)
                        hit.collider.GetComponent<Tile>().setFloorProps();
                }

            }
            if (Input.GetMouseButtonDown(0))
            {

                if (hit.collider.GetComponent<Tile>().getTileType() == Tile.TileTypes.target)
                {
                    map.endTile = null;
                    hit.collider.GetComponent<Tile>().setFloorProps();
                }

                if (hit.collider.GetComponent<Tile>().getTileType() == Tile.TileTypes.start)
                {
                    map.startTile = null;
                    hit.collider.GetComponent<Tile>().setFloorProps();
                }
            }

            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.E))
            {
                if (hit.collider.GetComponent<Tile>().getTileType() != Tile.TileTypes.start && map.startTile == null)
                {
                    map.startTile = hit.collider.GetComponent<Tile>();
                    hit.collider.GetComponent<Tile>().setStartProps();
                }
            }

            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.R))
            {
                if (hit.collider.GetComponent<Tile>().getTileType() != Tile.TileTypes.target && map.endTile == null)
                {
                    map.endTile = hit.collider.GetComponent<Tile>();
                    hit.collider.GetComponent<Tile>().setTargetProps();
                }
            }
        }
    }
}
