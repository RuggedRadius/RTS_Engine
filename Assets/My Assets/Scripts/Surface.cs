using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    public int width;
    public int length;
    public float cellSize;
    public float maxheight;
    public float lineWidth;

    Vector3[,] points;

    List<GameObject> rows = new List<GameObject>();




    void Start()
    {
        populateNodes();
        DrawGrid();
    }

    private void DrawGrid()
    {
        // For each row...
        GameObject rows = new GameObject();
        rows.transform.parent = this.transform;

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

    void Update()
    {
        
    }

    private void populateNodes()
    {
        points = new Vector3[width, length];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                points[i,j] = new Vector3(
                    (i) * cellSize,
                    Random.Range(0f, maxheight),
                    (j) * cellSize
                    );
            }
        }
    }

    private void createRow(Vector3[] rowPoints, Transform parent)
    {
        GameObject newRow = new GameObject();
        newRow.transform.parent = parent;

        LineRenderer lr = newRow.AddComponent<LineRenderer>();
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.positionCount = rowPoints.Length;
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
        lr.SetPositions(rowPoints);

        rows.Add(newRow);
    }
}
