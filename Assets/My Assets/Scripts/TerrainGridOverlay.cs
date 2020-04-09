using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGridOverlay : MonoBehaviour
{
    Vector3[,] points;

    List<GameObject> rows = new List<GameObject>();
    List<GameObject> cols = new List<GameObject>();

    [SerializeField]
    private Terrain terrain;

    [SerializeField]
    private float terrainGridHeight;

    [SerializeField]
    public float lineWidth;

    [SerializeField]
    public Color lineColour;

    [SerializeField]
    public Material lineMaterial;

    void Start()
    {
        overlayTerrain(terrain.terrainData);
        lineMaterial.color = lineColour;
    }

    private void Update()
    {
        
    }

    private void overlayTerrain(TerrainData _terrain)
    {
        // Populate points
        int xSize = (int)_terrain.size.x + 1;
        int zSize = (int)_terrain.size.z + 1;
        points = new Vector3[xSize, zSize];

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                points[i, j] = new Vector3(
                    i,
                    terrain.SampleHeight(new Vector3(i, 0, j)) + terrainGridHeight,
                    j
                    );
            }
        }

        DrawGrid(xSize, zSize);
    }

    private void DrawGrid(int width, int length)
    {
        // For each row...
        GameObject rows = new GameObject();
        rows.transform.parent = this.transform;
        rows.name = "Rows";

        for (int i = 0; i < width; i++)
        {
            // Create row of points
            Vector3[] rowPoints = new Vector3[length];

            // Populate points
            for (int j = 0; j < length; j++)
            {
                rowPoints[j] = points[i, j];
            }

            // Create row
            createRow(rowPoints, rows.transform);
        }


        // For each column...
        GameObject cols = new GameObject();
        cols.transform.parent = this.transform;
        cols.name = "Columns";

        for (int i = 0; i < length; i++)
        {
            // Create row of points
            Vector3[] colPoints = new Vector3[width];

            // Populate points
            for (int j = 0; j < width; j++)
            {
                colPoints[j] = points[j, i];
            }

            // Create row
            createColumn(colPoints, cols.transform);
        }

        cols.transform.eulerAngles = new Vector3(0, 90, 0);
    }

    private void createRow(Vector3[] rowPoints, Transform parent)
    {
        GameObject newRow = new GameObject();
        newRow.transform.parent = parent;

        LineRenderer lr = newRow.AddComponent<LineRenderer>();
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.positionCount = rowPoints.Length;
        lr.startColor = lineColour;
        lr.endColor = lineColour;
        lr.material = lineMaterial;
        lr.generateLightingData = true;
        lr.SetPositions(rowPoints);

        rows.Add(newRow);
    }

    private void createColumn(Vector3[] rowPoints, Transform parent)
    {
        GameObject newRow = new GameObject();
        newRow.transform.parent = parent;
        
        LineRenderer lr = newRow.AddComponent<LineRenderer>();
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.positionCount = rowPoints.Length;
        lr.startColor = lineColour;
        lr.endColor = lineColour;
        lr.material = lineMaterial;
        lr.generateLightingData = true;
        lr.SetPositions(rowPoints);

        cols.Add(newRow);
    }
}
