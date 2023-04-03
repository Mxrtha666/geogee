using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;
    Texture2D hMap;
    Texture2D ohMap;
    float amplitude;
    float tol;

    public TerrainFace(Mesh mesh, int resolution, Vector3 localUp, Texture2D hMap, float amplitude, Texture2D ohMap, float tol)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        this.hMap = hMap;
        this.ohMap = ohMap;
        this.amplitude = amplitude;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;
        Vector2[] uvs = new Vector2[resolution * resolution]; ;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = Normalizee(pointOnUnitCube);
                Vector3 vertex = pointOnUnitSphere;

                // Calculate latitude and longtitude for each vertex
                float latitude = Vector3.Angle(Vector3.up, vertex);               
                float angle = Vector3.Angle(new Vector2(1f, 0f), new Vector2(vertex.x, vertex.z));                
                if (vertex.z < 0)
                {
                    angle = 360 - angle;
                }                
                float longitude = angle / 360;               
                float height = hMap.GetPixelBilinear(longitude, latitude / 180).r;
                uvs[i] = new Vector2(longitude, latitude);

                vertices[i] = 10 * pointOnUnitSphere * (1 + height * amplitude);
                //height = ohMap.GetPixelBilinear(longitude, latitude / 180).r;

                //if (height > tol)
                //{
                //     // + pointOnUnitSphere * heightmap result;
                //    // Apply heightmap position to existing vertex
                //    
                //}
                //else
                //{
                //    height = ohMap.GetPixelBilinear(longitude, latitude / 180).r;
                //    vertices[i] = 10 * pointOnUnitSphere * (1 - (1 - height) * amplitude);
                //}

                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i+resolution + 1;
                    triangles[triIndex + 2] = i+resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;
                    triIndex += 6;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uvs;
    }

    public Vector3 Normalizee(Vector3 p)
    {
        float x2 = p.x * p.x;
        float y2 = p.y * p.y;
        float z2 = p.z * p.z;
        float x = p.x * Mathf.Sqrt(1 - (y2 + z2) / 2 + (y2 * z2) / 3);
        float y = p.y * Mathf.Sqrt(1 - (z2 + x2) / 2 + (z2 * x2) / 3);
        float z = p.z * Mathf.Sqrt(1 - (x2 + y2) / 2 + (x2 * y2) / 3);
        return new Vector3(x, y, z);
    }
}
