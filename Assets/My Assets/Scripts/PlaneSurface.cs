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

    Vector3[,] nodes;

    private void Awake()
    {
        populateNodes();
        createPlanes();
    }

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
                float xPosition = j * planeLength * (10 * newObj.transform.localScale.x);
                float yPosition = nodes[i, j].y;
                float zPosition = i * planeLength * (10 * newObj.transform.localScale.z);
                newObj.transform.localPosition = new Vector3(xPosition, yPosition, zPosition);

                // Angle plane
                Vector3 from;
                Vector3 to;
                if ((i != 0) && 
                    (j != 0) && 
                    (i != width - 1) && 
                    (j != length - 1))
                {
                    // X
                    from = nodes[i, j - 1];
                    to = nodes[i, j + 1];
                    float angleX = from.x - to.x;

                    // Y
                    float angleY = 0f;

                    // Z
                    from = nodes[i - 1, j];
                    to = nodes[i + 1, j];
                    //float angleZ = Vector3.Angle(from, to);
                    float angleZ = from.z - to.z;

                    newObj.transform.eulerAngles = new Vector3(angleX, angleY, angleZ);
                }
            }
        }
    }

    //private void populateNodes()
    //{
    //    arrayNodes = new Vector3[width * length];
    //    int counter = 0;
    //    for (int i = 0; i < width; i++)
    //    {
    //        for (int j = 0; j < length; j++)
    //        {
    //            arrayNodes[counter] = new Vector3(
    //                (i + 1) * planeLength,
    //                Random.Range(0f, 10f),
    //                (j + 1) * planeLength
    //                );
    //            counter++;
    //        }
    //    }
    //}

    private void populateNodes()
    {
        // Create new multi-dimensional array
        nodes = new Vector3[width, length];

        // Create temp floats
        float tmpX;
        float tmpY;
        float tmpZ;
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                // Determine components
                tmpX = (i + 1) * planeLength;

                // Make outside flat
                if (i == 0 || j == 0 || i == width || j == length)
                {
                    tmpY = 0f;
                }
                else
                {
                    tmpY = Random.Range(0f, 10f);
                }

                tmpZ = (j + 1) * planeLength;

                // Add vector to array
                nodes[i,j] = new Vector3(tmpX, tmpY, tmpZ);
            }
        }
    }

    #region old methods

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
