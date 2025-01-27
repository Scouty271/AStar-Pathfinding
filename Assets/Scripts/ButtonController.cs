using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Map map;
    public Pathfinding pathfinding;

    public Text textTime;    

    public void OnClickButtonAStar()
    {
        var t = 0f;

        var start = map.startTile;
        var end = map.endTile;

        for (int i = 0; i < 100; i++)
        {
            pathfinding.FindWithAStar(start, end);
            pathfinding.clearValues();
            float dt = Time.deltaTime;
            t = t + dt;
        }

        textTime.text = (t / 100).ToString();

        //pathfinding.FindWithAStar(map.startTile, map.endTile);

        //textTime.text = Time.deltaTime.ToString();

    }

    //public void OnClickButtonGreedy()
    //{
    //    pathfinding.findPathWithGreedy();
    //}

    //public void OnClickButtonDjikstra()
    //{

    //}

    //public void OnClickButtonPositionDifference()
    //{
    //    pathfinding.findPathWithPositionDifference();
    //}

    public void OnClickClearMap()
    {
        map.ClearMap();
        pathfinding.clearValues();
    }
}
