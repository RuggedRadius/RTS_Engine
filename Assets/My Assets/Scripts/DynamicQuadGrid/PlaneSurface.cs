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
    public int maxHeight;
    [SerializeField]
    public float planeLength;

    GameObject[,] quads;
    Vector3[,] nodes;

    public GameObject nodesParentTemp;
    public List<GameObject> tmpNodeObjects;
    public GameObject planesParent;

    private void Awake()
    {
        maxHeight = (int)planeLength / 2;
        tmpNodeObjects = new List<GameObject>();
        populateNodes();
        createPlanes();
    }

    private void Update()
    {
        updateQuadVertices();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(wave());
        }
    }

    private void updateQuadVertices()
    {
        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < length - 1; j++)
            {
                // Create mesh
                Vector3[] vertices = new Vector3[4];
                vertices[0] = nodes[i, j];
                vertices[1] = nodes[i + 1, j];
                vertices[2] = nodes[i, j + 1];
                vertices[3] = nodes[i + 1, j + 1];

                // Create mesh and assign it to mesh filter
                Mesh mesh = createMesh(vertices);
                quads[i, j].gameObject.GetComponent<MeshFilter>().mesh = mesh;
            }
        }
    }

    private IEnumerator wave()
    {
        Debug.Log("Starting wave...");
        // Apply modulation
        int counter = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                counter++;
                float x = nodes[i, j].x;
                float y = Random.Range(0.1f, maxHeight);
                float z = nodes[i, j].z;
                nodes[i, j] = new Vector3(x, y, z);

                    
                updateQuadVertices();
            }
        }
        yield return null;
    }

    private void createPlanes()
    {
        planesParent = new GameObject();
        planesParent.transform.SetParent(this.transform);
        planesParent.name = "Planes";

        quads = new GameObject[width-1,length-1];

        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < length - 1; j++)
            {
                // Create plane
                GameObject newPlane = new GameObject();
                newPlane.transform.parent = planesParent.transform;

                // Add renderer and filter
                MeshRenderer meshRenderer = newPlane.gameObject.AddComponent<MeshRenderer>();
                // Set material
                meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
                MeshFilter meshFilter = newPlane.gameObject.AddComponent<MeshFilter>();

                // Create mesh
                Vector3[] vertices = new Vector3[4];
                vertices[0] = nodes[i, j];
                vertices[1] = nodes[i + 1, j];
                vertices[2] = nodes[i, j + 1];
                vertices[3] = nodes[i + 1, j + 1];


                // Create mesh and assign it to mesh filter
                Mesh mesh = createMesh(vertices);
                meshFilter.mesh = mesh;

                quads[i, j] = newPlane;

                // Position plane
                //newPlane.transform.localPosition = calculateQuadWorldPosition(i, j);
            }
        }
    }

    private Vector3 calculateQuadWorldPosition(int i, int j)
    {
        // X
        float xPosition = i;
        float xOffset = planeLength/2;
        xPosition -= xOffset;

        // Y
        float yPosition = nodes[i, j].y;
        
        // Z
        float zPosition = j;
        float zOffset = planeLength/2;
        zPosition -= zOffset;

        return new Vector3(i, yPosition, j);
    }

    private Mesh createMesh(Vector3[] _vertices)
    {
        Mesh mesh = new Mesh();

        mesh.vertices = _vertices;

        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
              new Vector2(0, 0),
              new Vector2(1, 0),
              new Vector2(0, 1),
              new Vector2(1, 1)
        };
        mesh.uv = uv;

        return mesh;
    }

    private void populateNodes()
    {
        // Create new multi-dimensional array
        nodes = new Vector3[width, length];

        // Create nodes parent
        nodesParentTemp = new GameObject();
        nodesParentTemp.transform.SetParent(this.transform);
        nodesParentTemp.name = "Nodes";

        // Create temp floats
        float tmpX;
        float tmpY;
        float tmpZ;
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                // Determine components
                tmpX = (i) * planeLength;

                // Make outside flat
                if (i == 0 || j == 0 || i == width || j == length)
                {
                    tmpY = 0f;
                }
                else
                {
                    tmpY = Random.Range(0f, maxHeight);
                }

                tmpZ = (j) * planeLength;

                // Add vector to array
                nodes[i,j] = new Vector3(tmpX, tmpY, tmpZ);

                //// Create temp visual object
                //GameObject tmpNodeSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //tmpNodeSphere.transform.SetParent(nodesParentTemp.transform);
                //tmpNodeSphere.transform.position = nodes[i, j];
                //tmpNodeSphere.transform.localScale = Vector3.one * 0.2f;
            }
        }
    }
}
