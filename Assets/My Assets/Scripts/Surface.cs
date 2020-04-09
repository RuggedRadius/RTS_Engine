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

    [SerializeField]
    private Terrain terrain;

    [SerializeField]
    private float terrainGridHeight;

    void Start()
    {
        //populateNodes();
        //DrawGrid();

        //foreach (Vector3 pos in points)
        //{
        //    createPlane(pos);
        //}

        //applyToTerrain();
        overlayTerrain(terrain.terrainData);
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

    private void applyToTerrain()
    {
        int xBase = 1;
        int yBase = 1;

        terrain.terrainData.size = new Vector3(width, 5, length);

        float[,] yPoints = new float[width, length];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                yPoints[i, j] = points[i, j].y;
            }
        }


        //int heightmapWidth = terrain.terrainData.heightmapResolution;
        //int heightmapHeight = terrain.terrainData.heightmapResolution;
        //float[,] heights = terrain.terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);
        //Smooth(heights, terrain.terrainData);
        //terrain.terrainData.SetHeights(0, 0, heights);



        Smooth(terrain.terrainData);
        //terrain.terrainData.SetHeights(xBase, yBase, yPoints);
    }

    public static void Smooth(TerrainData terrain)
    {
        int heightmapWidth = terrain.heightmapResolution;
        int heightmapHeight = terrain.heightmapResolution;
        float[,] heights = terrain.GetHeights(0, 0, heightmapWidth, heightmapHeight);
        Smooth(heights, terrain);
        terrain.SetHeights(0, 0, heights);
    }

    public static void Smooth(float[,] heights, TerrainData terrain)
    {
        float[,] numArray = heights.Clone() as float[,];
        int length1 = heights.GetLength(1);
        int length2 = heights.GetLength(0);
        for (int index1 = 1; index1 < length2 - 1; ++index1)
        {
            for (int index2 = 1; index2 < length1 - 1; ++index2)
            {
                float num = (0.0f + numArray[index1, index2] + numArray[index1, index2 - 1] + numArray[index1, index2 + 1] + numArray[index1 - 1, index2] + numArray[index1 + 1, index2]) / 5f;
                heights[index1, index2] = num;
            }
        }
    }

    private void createPlane(Vector3 position)
    {
        GameObject newPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        newPlane.transform.position = position;
        newPlane.transform.position += new Vector3(5, 0, 5);
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
    private void DrawGrid(int width, int length)
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
