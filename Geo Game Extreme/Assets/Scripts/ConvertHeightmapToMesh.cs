using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertHeightmapToMesh : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices; // cube sphere vertices
    private Vector3[] normals; // cube sphere normals
    private Color32[] cubeUV; // currently displays red green blue colors to help differentiate faces
    public Texture2D hMap; // HeightMap received from input
    public Texture2D ohMap; // HeightMap received from input
    public float maxHeight;
    public Material mat;
    public float tolerance;

    public MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        Kombajn();
        ConvertHeightToMesh();
        OceanHeight();
    }

    void ConvertHeightToMesh()
    {
        Vector3 vertex;
        for (int i = 0; i < vertices.Length; i++)
        {
            // Get existing vertex from mesh vertices array vertices.Length
            //print(i);
            vertex = vertices[i];

            // Calculate latitude and longtitude for each vertex
            float latitude = Vector3.Angle(Vector3.up, vertex);

            float angle = Vector3.Angle(new Vector2(1f, 0f), new Vector2(vertex.x, vertex.z));

            if (vertex.z < 0)
            {
                angle = 360 - angle;
            }

            float longitude = angle / 360;
            //print(new Vector2(vertex.x, vertex.z));
            //print(longitude);
            //print(angle);
            //print(hMap.width * longitude);

            float height = hMap.GetPixelBilinear(longitude, latitude / 180).r;

            // Apply heightmap position to existing vertex
            vertex *= 1 + maxHeight * height;

            // Set the new vertex in the cube sphere
            vertices[i] = vertex;
        }

        //what does this do???
        Vector2[] uvs = new Vector2[vertices.Length];
        for (var i = 0; i < uvs.Length; i++)
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);

        mesh.vertices = vertices; //Assign verts, uvs, and tris to the mesh
        mesh.uv = uvs;

        mesh.RecalculateNormals(); //Determines which way the triangles are facing
        mr.sharedMaterial = mat;
    }

    public void OceanHeight()
    {
        Vector3 vertex;
        for (int i = 0; i < vertices.Length; i++)
        {
            // Get existing vertex from mesh vertices array vertices.Length
            //print(i);
            vertex = vertices[i];

            // Calculate latitude and longtitude for each vertex
            float latitude = Vector3.Angle(Vector3.up, vertex);

            float angle = Vector3.Angle(new Vector2(1f, 0f), new Vector2(vertex.x, vertex.z));

            if (vertex.z < 0)
            {
                angle = 360 - angle;
            }

            float longitude = angle / 360;
            //print(new Vector2(vertex.x, vertex.z));
            //print(longitude);
            //print(angle);
            //print(hMap.width * longitude);

            float height = ohMap.GetPixelBilinear(longitude, latitude / 180).r;

            if (height > tolerance)
            {
                // Apply heightmap position to existing vertex
                vertex *= 1 + maxHeight * -(1.00f - height);

                // Set the new vertex in the cube sphere
                vertices[i] = vertex;
            }
        }

        //what does this do???
        Vector2[] uvs = new Vector2[vertices.Length];
        for (var i = 0; i < uvs.Length; i++)
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);

        mesh.vertices = vertices; //Assign verts, uvs, and tris to the mesh
        mesh.uv = uvs;

        mesh.RecalculateNormals(); //Determines which way the triangles are facing
        mr.sharedMaterial = mat;
    }

    void Kombajn()
    {

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);

        mr.sharedMaterial = new Material(Shader.Find("Standard"));
        vertices = transform.GetComponent<MeshFilter>().mesh.vertices;
        normals = transform.GetComponent<MeshFilter>().mesh.normals;

        mesh = GetComponent<MeshFilter>().mesh;
    }
}
