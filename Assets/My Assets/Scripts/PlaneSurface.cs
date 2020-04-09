using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSurface : MonoBehaviour
{
    [SerializeField]
    public int width;
    [SerializeField]
    public int length;

    [SerializeField]
    public float planeLength;

    Vector3[] arrayNodes;

    [SerializeField]
    private GameObject lineParticlePrefab;
    [SerializeField]
    public List<GameObject> rows;

    private void Awake()
    {
        populateNodes();
        rows = new List<GameObject>();
        drawRows();
        drawColumns();

    }

    private void drawColumns()
    {
        GameObject columns = new GameObject();
        columns.transform.parent = this.transform;
        //columns.transform.eulerAngles = new Vector3(0, 90, 0);

        int counter = 0;
        for (int i = 0; i < length; i++)
        {
            // Create row of points
            Vector3[] rowPoints = new Vector3[length];

            // Populate points
            for (int j = 0; j < width; j++)
            {
                rowPoints[j] = arrayNodes[counter];
                counter++;
            }

            // Create row
            createRow(rowPoints, columns.transform);
        }

        //columns.transform.eulerAngles = new Vector3(0, 90, 0);
    }

    private void drawRows()
    {
        GameObject rows = new GameObject();
        rows.transform.parent = this.transform;

        int counter = 0;
        for (int i = 0; i < width; i++)
        {
            // Create row of points
            Vector3[] rowPoints = new Vector3[length];

            // Populate points
            for (int j = 0; j < length; j++)
            {
                rowPoints[j] = arrayNodes[counter];
                counter++;
            }

            // Create row
            createRow(rowPoints, rows.transform);
        }

        //rows.transform.position = new Vector3(0, 0, -(width + 1));
    }



    private void createRow(Vector3[] rowPoints, Transform parent)
    {
        GameObject newRow = Instantiate(lineParticlePrefab);
        newRow.transform.parent = parent;
        newRow.GetComponent<LineParticle>().m_Points = rowPoints;
        rows.Add(newRow);
    }

    private void populateNodes()
    {
        arrayNodes = new Vector3[width * length];
        int counter = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                arrayNodes[counter] = new Vector3(
                    (i + 1) * planeLength,
                    Random.Range(0f, 2f),
                    (j + 1) * planeLength
                    );
                counter++;
            }
        }
    }

    #region old methods
    private void createPlanes()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                // Create plane
                GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
                newObj.transform.parent = this.transform;
                newObj.transform.localScale = new Vector3(width, 1f, length);

                // Position plane
                //Vector3 angle = Vector3.Angle();
                newObj.transform.localPosition = new Vector3(
                    j * planeLength * (10 * newObj.transform.localScale.x),
                    0f,
                    i * planeLength * (10 * newObj.transform.localScale.z)
                    );
                //newObj.transform.localPosition = grid.
            }
        }
    }
    private void createSurface(Vector3[,] v)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                // Create plane
                GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
                newObj.transform.parent = this.transform;
                newObj.transform.localScale = new Vector3(width, 1f, length);

                // Position plane
                //Vector3 angle = Vector3.Angle()
                newObj.transform.localPosition = new Vector3(
                    j * planeLength * 20f,
                    0f,
                    i * planeLength * 20f
                    );



            }
        }
    }
    #endregion
}
