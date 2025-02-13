using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public List<GameObject> boxPrefabs; 
    public BoxListConfig gridConfig; 
    public Vector3 startPosition; 
    public float spacing = 1f; 

    void Start()
    {
        SpawnGrid();
    }

    public void SpawnGrid()
    {

        for (int row = 0; row < gridConfig.BoxRow.Count; row++)
        {
            List<int> currentRow = gridConfig.BoxRow[row].Columns; // Access columns
            for (int col = 0; col < currentRow.Count; col++)
            {
                int prefabIndex = currentRow[col];

                if (prefabIndex < 0 || prefabIndex >= boxPrefabs.Count)
                {
                    Debug.LogWarning($"Invalid prefab index {prefabIndex} at row {row}, column {col}");
                    continue;
                }

                Vector3 spawnPosition = startPosition + new Vector3(col * spacing, 0, row * spacing);

                GameObject newBox = Instantiate(boxPrefabs[prefabIndex], spawnPosition, Quaternion.identity);
                newBox.transform.SetParent(transform);
            }
        }

    }
}
