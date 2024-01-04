using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPositions : MonoBehaviour
{
    [SerializeField] private List<Vector2> spawnPositionsList = new List<Vector2>();
    public List<Vector2> SpawnPositionsList
    {
        get { return spawnPositionsList; }
        set { spawnPositionsList = value; }
    }   


    [SerializeField] private bool _useAutoGenerating = false;
    public bool UseAutoGenerating
    {
        get { return _useAutoGenerating; }
    }


    [SerializeField] private int _rows;
    [SerializeField] private int _collumns;


    public void ConvertPositionsToDifferentResolutions()
    {   
        float verticalRatio = Screen.height / 1920;

        for(int i = 0; i < SpawnPositionsList.Count; i++)
        {
            Vector2 convertedPosition = new Vector2(SpawnPositionsList[i].x, SpawnPositionsList[i].y * verticalRatio);

            SpawnPositionsList[i] = convertedPosition;
        }
    }



    public void AutoGenerateSpawnPositions()
    {
        SpawnPositionsList.Clear();

        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        int distance = (int) screenSize.x / _collumns;
        int x = 0 + distance / 2;
        
        for(int i = 0; i < _collumns; i++)
        {
            int y = (int) screenSize.y - Screen.height / 3 * 2;

            for(int j = 0; j < _rows; j++)
            {
                Vector2 position = new Vector2(x, y);
                SpawnPositionsList.Add(position);
        
                y += distance;      
            }

            x += distance;
        }
    }

}
